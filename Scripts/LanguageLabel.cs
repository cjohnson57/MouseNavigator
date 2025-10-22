using Godot;
using System;

public partial class LanguageLabel : Label
{
    [Export]
    public string EnglishText = "";
    [Export]
    public string FrenchText = "";

    public void _OnLanguageChange(bool french)
    {
        Text = french ? FrenchText : EnglishText;
    }
}
