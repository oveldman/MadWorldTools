namespace MadOffice.Application.Gui;

public class DropdownOption
{
    public string Text { get; set; }
    public int Value { get; set; }
    
    public DropdownOption(string text, int value)
    {
        Text = text;
        Value = value;
    }
}