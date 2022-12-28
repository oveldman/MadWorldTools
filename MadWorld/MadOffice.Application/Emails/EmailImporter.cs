using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MadOffice.Application.Extensions;
using MadOffice.Domain.Emails.Exceptions;
using MadOffice.Domain.Emails.Interfaces;
using MadOffice.Domain.Emails.Models;
using MadOfficePerson = MadOffice.Domain.Emails.Models.Person;

namespace MadOffice.Application.Emails;

public class EmailImporter : IEmailImporter
{
    private readonly IEmailReader _emailReader;
    
    public EmailImporter(IEmailReader emailReader)
    {
        _emailReader = emailReader;
    }
    
    public bool Import(MemoryStream file, EmailType setEmailType)
    {
        var (workbookPart, worksheet) = OpenWorkSheet(file);
        var persons = ParseWorksheet(workbookPart, worksheet);
        persons.AddIds();
        persons.Set(setEmailType);
        return _emailReader.Save(persons);
    }

    private static (WorkbookPart, WorksheetPart) OpenWorkSheet(MemoryStream file)
    {
        var package = System.IO.Packaging.Package.Open(file);
        var document = SpreadsheetDocument.Open(package);
        var workDocument = document.WorkbookPart;
        if (workDocument == null)
        {
            throw new EmailImportException("WorkbookPart not found in this excel file");
        }

        var sheet = workDocument.Workbook.Descendants<Sheet>().First();
        if (!sheet.Id?.HasValue ?? true)
        {
            throw new EmailImportException("First sheet has no Id in this excel file");
        }
        
        
        return (workDocument, (WorksheetPart)workDocument.GetPartById(sheet.Id.Value!));
    }
    
    private static List<MadOfficePerson> ParseWorksheet(WorkbookPart workbookPart, WorksheetPart worksheetPart)
    {
        var worksheet = worksheetPart.Worksheet;
        var rows = worksheet.Descendants<Row>();
        var people = new List<MadOfficePerson>();
        foreach (var row in rows)
        {
            var cells = row.Descendants<Cell>();
            var person = new MadOfficePerson();
            foreach (var cell in cells)
            {
                if (cell.CellReference?.HasValue ?? false)
                {
                    var value = GetColumnValue(workbookPart, cell);
                    switch (GetColumnName(cell.CellReference.Value!))
                    {
                        case "A":
                            person.Email = value;
                            break;
                        case "B":
                            person.FirstName = value;
                            break;
                        case "C":
                            person.LastName = value;
                            break;
                    }
                }
            }
            people.Add(person);
        }
        return people;
    }

    private static string GetColumnValue(WorkbookPart workbookPart, CellType cell)
    {
        if (cell.DataType == null) return string.Empty;
        if (cell.DataType != CellValues.SharedString) return string.Empty;
        if (!int.TryParse(cell.InnerText, out var id)) return string.Empty;
        
        var item = GetSharedStringItemById(workbookPart, id);

        return item.Text != null ? item.Text.Text : item.InnerXml;
    }
    
    private static SharedStringItem GetSharedStringItemById(WorkbookPart workbookPart, int id)
    {
        if (workbookPart.SharedStringTablePart == null)
        {
            throw new EmailImportException("This workbookPart has no SharedStringTablePart");
        }
        
        return workbookPart.SharedStringTablePart!.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
    }
    
    private static readonly Regex ColumnNameRegex = new("[A-Za-z]+", RegexOptions.None,TimeSpan.FromSeconds(1));
    
    private static string GetColumnName(string cellReference)
    {
        if (ColumnNameRegex.IsMatch(cellReference))
            return ColumnNameRegex.Match(cellReference).Value;

        throw new ArgumentOutOfRangeException(cellReference);
    }
}