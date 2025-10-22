using Godot;
using System;

public partial class Hole : Control
{
    private ColorRect Rectangle;
    private enum State
    {
        Open,
        Closed,
        Exit,
    }
    private State state;

    public bool horizontal = true;

    public override void _Ready()
    {
        base._Ready();
        Rectangle = GetNode<ColorRect>("Rectangle");
        if (!horizontal)
        {
            RotationDegrees = 90;
        }
    }

    private void _OnGuiInput(InputEvent @event)
    {
        //User has clicked on hole
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
        { 
            if (state == State.Open)
            {
                state = State.Closed;
                Rectangle.Modulate = Colors.Red;
            }
            else if (state == State.Exit || state == State.Closed && !horizontal) //Either state is exit, or it's closed but this is not a horizontal hole, so cannot be the exit
            {
                state = State.Open;
                Rectangle.Modulate = Colors.Green;
            }
            else //State is closed and this is a horizontal hole
            {
                state = State.Exit;
                Rectangle.Modulate = Colors.Gold;
            }
        }
    }
}
