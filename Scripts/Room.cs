using Godot;
using System;

public partial class Room : Control
{
    public RoomInfo Info { get; set; }

    private bool mouseHovering = false;
    private bool mouseDragging = false;
    private Vector2 draggingOffset = new Vector2();
    private bool reversed = false;

    private ColorRect background;
    private ColorRect outline;
    private Label nameLabel;
    private AnimatedSprite2D icon;

    private ShaderMaterial materialInstance;

    public override void _Ready()
    {
        base._Ready();
        background = GetNode<ColorRect>("Background");
        outline = GetNode<ColorRect>("Outline");
        nameLabel = GetNode<Label>("NameLabel");
        icon = GetNode<AnimatedSprite2D>("Icon");
        icon.Play(Info.Name);
        nameLabel.Visible = false;
        nameLabel.Text = Info.Name;
        //Change a bunch of things if this is a double wide room
        if (Info.DoubleWide)
        {
            float outlineSizeDifference = outline.Size.X - background.Size.X;
            background.Size = new Vector2(background.Size.X * 2, background.Size.Y);
            background.Position = new Vector2(background.Position.X * 2, background.Position.Y);
            outline.Size = new Vector2(background.Size.X + outlineSizeDifference, outline.Size.Y);
            outline.Position = new Vector2(background.Position.X - outlineSizeDifference / 2, outline.Position.Y);
        }
        //Set the material for the icon so we can modify the shader
        ShaderMaterial shaderMat = icon.Material as ShaderMaterial;
        materialInstance = shaderMat.Duplicate() as ShaderMaterial;
        icon.Material = materialInstance;
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
                reversed = !reversed;
                if (reversed)
                {
                    background.Color = Colors.Black;
                    outline.Color = Colors.White;
                    materialInstance.SetShaderParameter("makeWhite", true);
                    Scale = new Vector2(Scale.X * -1, Scale.Y);
                    nameLabel.Scale = new Vector2(nameLabel.Scale.X * -1, nameLabel.Scale.Y); //Make sure label is not reversed
                    MoveLabel(); //Prevents the label from flashing to the side for 1 frame
                }
                else
                {
                    background.Color = Colors.White;
                    outline.Color = Colors.Black;
                    materialInstance.SetShaderParameter("makeWhite", false);
                    Scale = new Vector2(Scale.X * -1, Scale.Y);
                    nameLabel.Scale = new Vector2(nameLabel.Scale.X * -1, nameLabel.Scale.Y); //Make sure label is not reversed
                    MoveLabel(); //Prevents the label from flashing to the side for 1 frame
                }
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
