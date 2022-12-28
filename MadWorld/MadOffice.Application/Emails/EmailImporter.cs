using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MadOffice.Application.Extensions;
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

    private (WorkbookPart, WorksheetPart) OpenWorkSheet(MemoryStream file)
    {
        var package = System.IO.Packaging.Package.Open(file);
        var document = SpreadsheetDocument.Open(package);
        var sheet = document.WorkbookPart.Workbook.Descendants<Sheet>().First();
        return (document.WorkbookPart ,(WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id));
    }
    
    private List<MadOfficePerson> ParseWorksheet(WorkbookPart workbookPart, WorksheetPart worksheetPart)
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
                var value = GetColumnValue(workbookPart, cell);
                switch (GetColumnName(cell.CellReference.Value))
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
            people.Add(person);
        }
        return people;
    }

    private static string GetColumnValue(WorkbookPart workbookPart, Cell cell)
    {
        if (cell.DataType == null) return string.Empty;
        if (cell.DataType != CellValues.SharedString) return string.Empty;
        if (!int.TryParse(cell.InnerText, out var id)) return string.Empty;
        
        var item = GetSharedStringItemById(workbookPart, id);

        if (item.Text != null)
        {
            return item.Text.Text;
        }
                    
        if (item?.InnerText != null)
        {
            return item.InnerText;
        }
                    
        return item?.InnerXml ?? string.Empty;
    }
    
    private static SharedStringItem GetSharedStringItemById(WorkbookPart workbookPart, int id)
    {
        return workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
    }
    
    private static readonly Regex ColumnNameRegex = new Regex("[A-Za-z]+");
    
    private static string GetColumnName(string cellReference)
    {
        if (ColumnNameRegex.IsMatch(cellReference))
            return ColumnNameRegex.Match(cellReference).Value;

        throw new ArgumentOutOfRangeException(cellReference);
    }
}