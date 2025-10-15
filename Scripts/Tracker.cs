using Godot;
using System;
using static Rooms;

public partial class Tracker : Node2D
{
    [Export]
    public Vector2 spawnStart = new Vector2(1530, 80);

    [Export]
    public float spawnXStep = 260;

    [Export]
    public float spawnYStep = 140;    

    private Rooms rooms = new Rooms();

    public override void _Ready()
    {
        base._Ready();
        //Load room scene and spawn each room
        PackedScene roomScene = GD.Load<PackedScene>("res://Scenes/Room.tscn");
        Vector2 spawnPosition = spawnStart;
        int count = 0;
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
}
