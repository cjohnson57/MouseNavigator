using Godot;
using System;

public partial class LanguageButton : Button
{
    public void _OnButtonDown()
    {
        GetParent().Call("OnButtonPressed", this);
    }
}
