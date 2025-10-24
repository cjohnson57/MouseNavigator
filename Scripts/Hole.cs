using Godot;
using System;

public partial class Hole : Control
{
    [Export]
    public HoleState defaultState = HoleState.Open;

    private ColorRect Rectangle;

    public enum HoleState
    {
        Open,
        Closed,
        Exit,
    }
    private HoleState state;

    public bool horizontal = true;

    public ConnectionLine connection = null;
    public int connectionPoint = 0;

    public override void _Ready()
    {
        base._Ready();
        Rectangle = GetNode<ColorRect>("Rectangle");
        if (!horizontal)
        {
            RotationDegrees = 90;
            //Vertical holes are closed by default
            SwitchState(HoleState.Closed);
        }
        else
        {
            SwitchState(defaultState);
        }
    }

    public void SwitchState(HoleState _state)
    {
        state = _state;
        switch(state)
        {
            case HoleState.Open:
                Rectangle.Modulate = Colors.Green;
                break;
            case HoleState.Closed:
                Rectangle.Modulate = Colors.Red;
                break;
            case HoleState.Exit:
                Rectangle.Modulate = Colors.Gold;
                break;
        }
    }

    private void _OnGuiInput(InputEvent @event)
    {
        //User has clicked on hole
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.Pressed) //Right click, cycle colors
            {
                if (state == HoleState.Open)
                {
                    SwitchState(HoleState.Closed);
                }
                else if (state == HoleState.Exit || state == HoleState.Closed && !horizontal) //Either state is exit, or it's closed but this is not a horizontal hole, so cannot be the exit
                {
                    SwitchState(HoleState.Open);
                }
                else //State is closed and this is a horizontal hole
                {
                    SwitchState(HoleState.Exit);
                }
            }
            else if (mouseEvent.ButtonIndex == MouseButton.Left) //Left click, line drawing
            {
                DiscardConnection();
                if (mouseEvent.Pressed)
                {
                    GetTree().CallGroup("HoleListener", "_OnHolePressed", this);
                }
                else //Released
                {
                    GetTree().CallGroup("HoleListener", "_OnHoleReleased");
                }
            }
        }
    }

    public void DiscardConnection()
    {
        if (connection != null) //If there is currently a line connected to this hole, then delete it
        {
            connection.QueueFree();
            connection.FreeHoles();
        }
    }

    private void _OnMouseEntered()
    {
        GetTree().CallGroup("HoleListener", "_OnHoleHovered", this);
    }

    private void _OnMouseExited()
    {
        GetTree().CallGroup("HoleListener", "_OnHoleExited");
    }
}
