using Godot;
using System;
using System.Collections.Generic;

public partial class Gacha : Control
{
    public GachaInfo Info { get; set; }
    private bool mouseHovering = false;
    private bool mouseDragging = false;
    private Vector2 draggingOffset = new Vector2();
    private bool reversed = false;

    private ColorRect outline;
    private Label nameLabel;
    private AnimatedSprite2D icon;

    private ShaderMaterial materialInstance;

    public override void _Ready()
    {
        base._Ready();
        outline = GetNode<ColorRect>("Outline");
        nameLabel = GetNode<Label>("NameLabel");
        icon = GetNode<AnimatedSprite2D>("Icon");
        icon.Play(Info.Name);
        nameLabel.Visible = false;
        nameLabel.Text = Info.Name;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (mouseHovering && !mouseDragging)
        {
            MoveLabel();
        }
    }

    public void _OnMouseEntered()
    {
        mouseHovering = true;
        if (!mouseDragging)
        {
            nameLabel.Visible = true;
            MoveLabel();
        }
    }

    public void _OnMouseExited()
    {
        nameLabel.Visible = false;
        mouseHovering = false;
    }

    private void MoveLabel()
    {
        Vector2 mousePos = GetGlobalMousePosition();
        Vector2 size = nameLabel.Size;
        Vector2 labelPos = mousePos - size / 2;
        labelPos.Y -= 30;
        nameLabel.GlobalPosition = labelPos;
    }

    private void _OnGuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent) 
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left) //Left button has done something
            {
                if (mouseEvent.Pressed) //Button down, start dragging
                {
                    mouseDragging = true;
                    draggingOffset = mouseEvent.GlobalPosition - GlobalPosition; //Get the difference between mouse and our pos so we can keep that difference as we move
                    nameLabel.Visible = false; // Also hide the label while we're dragging
                }
                else //Button up
                {
                    mouseDragging = false;
                    if (mouseHovering)
                    {
                        nameLabel.Visible = true; //Show label again
                    }                    
                }
            }
            else if (mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.Pressed)
            {
            }            
        }
        else if (@event is InputEventMouseMotion motionEvent && mouseDragging) //Currently being dragged and mouse is moving
        {
            GlobalPosition = motionEvent.GlobalPosition - draggingOffset;
        }
    }

    public void _OnLanguageChange(bool french)
    {
        nameLabel.Text = french ? Info.NameFrench : Info.Name;
    }
}
