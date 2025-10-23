using Godot;
using System;

public partial class ConnectionLine : Line2D
{
    public Hole hole1 = null;
    public Hole hole2 = null;

    public void FreeHoles()
    {
        hole1.connection = null;
        hole2.connection = null;
    }
}
