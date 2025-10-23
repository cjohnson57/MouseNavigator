using Godot;
using System;

public partial class Tracker : Node2D
{
    [Export]
    public Vector2 spawnStart = new Vector2(1530, 80);

    [Export]
    public float spawnXStep = 260;

    [Export]
    public float spawnYStep = 140;    

    private Rooms rooms = new Rooms();

    //Handling hole lines
    private Hole hoveredHole = null;
    private Hole activeHole = null;
    private ConnectionLine activeLine = null;
    Shader lineShader;

    public override void _Ready()
    {
        base._Ready();
        //Load room scene and spawn each room
        PackedScene roomScene = GD.Load<PackedScene>("res://Scenes/Room.tscn");
        Vector2 spawnPosition = spawnStart;
        int count = 0;
        lineShader = GD.Load<Shader>("res://Shaders/DynamicLine.gdshader");
        //Add each room
        foreach (RoomInfo info in rooms.RoomList)
        {
            Room instance = roomScene.Instantiate<Room>();
            instance.GlobalPosition = spawnPosition;
            spawnPosition.Y += spawnYStep;
            count++;
            if (count == 7) //After the 7th one spawned, move to the next column
            {
                spawnPosition.X += spawnXStep;
                spawnPosition.Y = spawnStart.Y;
            }
            instance.Info = info;
            GetTree().CurrentScene.AddChild(instance);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        //Handle keeping line active
        if (activeHole != null && activeLine != null)
        {
            activeLine.SetPointPosition(1, GetGlobalMousePosition());
        }
    }

    public void _OnHolePressed(Hole hole)
    {
        activeHole = hole;
        //Instantiate line
        activeLine = new ConnectionLine();
        activeLine.Width = 3;
        activeLine.DefaultColor = Colors.Black;
        activeLine.ZIndex = 1;
        activeLine.AddPoint(hole.GlobalPosition);
        activeLine.AddPoint(GetGlobalMousePosition());
        //Add shader to line
        ShaderMaterial mat = new ShaderMaterial();
        mat.Shader = lineShader;
        activeLine.Material = mat;
        GetTree().Root.AddChild(activeLine);
    }

    public void _OnHoleReleased()
    {
        if (activeHole != null && activeLine != null)
        {
            if (hoveredHole != null)
            {
                //Snap the line to position on this new hole
                activeLine.SetPointPosition(1, hoveredHole.GlobalPosition);
                activeLine.hole1 = activeHole;
                activeLine.hole2 = hoveredHole;
                //Set as a connection to both holes
                activeHole.connection = activeLine;
                activeHole.connectionPoint = 0;
                hoveredHole.connection = activeLine;
                hoveredHole.connectionPoint = 1;
            }
            else
            {
                activeLine.QueueFree(); //Remove line
            }
            //Remove active state
            activeHole = null;
            activeLine = null;
        }
    }

    public void _OnHoleHovered(Hole hole)
    {
        hoveredHole = hole;
    }

    public void _OnHoleExited()
    {
        hoveredHole = null;
    }
}
