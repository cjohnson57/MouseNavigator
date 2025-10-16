using Godot;
using System;

public partial class LanguageSelector : Control
{
    private bool french = false;

    private Button EnglishButton;
    private Button FrenchButton;

    public override void _Ready()
    {
        EnglishButton = GetNode<Button>("EnglishButton");
        FrenchButton = GetNode<Button>("FrenchButton");
    }

    public void OnButtonPressed(Button sender)
    {
        //The default button press logic runs after this function does
        //So... we want to set the button pressing to the opposite of what we actually want
        //We always want button pressed to be true if it's clicked, because if it's already pressed then clicking should do nothing
        bool buttonAlreadyPressed = sender.ButtonPressed;
        sender.ButtonPressed = false;
        if (buttonAlreadyPressed)
        {
            return;
        }
        //Set the other button and the new language setting
        if (sender == EnglishButton)
        {
            french = false;
            FrenchButton.ButtonPressed = false;
        }
        else
        {
            french = true;
            EnglishButton.ButtonPressed = false;
        }
        GetTree().CallGroup("LanguageChangeDetector", "_OnLanguageChange", french);
    }
}
