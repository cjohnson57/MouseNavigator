using Godot;
using System;

public partial class ResetButton : Button
{
    [Export]
    public string EnglishText = "";
    [Export]
    public string FrenchText = "";

    public void _OnLanguageChange(bool french)
    {
        Text = french ? FrenchText : EnglishText;
    }

    public void _OnButtonUp()
    {
        GetTree().ReloadCurrentScene();
    }
}
