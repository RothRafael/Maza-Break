using Godot;
using System;

public partial class Room : Resource
{
    [Export]
    public string Name { get; set; }

    [Export]
    public Room[] AdjacentRooms { get; set; }

    [Export]
    public Vector2[] AdjacentEntrancesPositions { get; set; }

    [Export]
    public Vector2[] ExitPositions { get; set; }

    [Export]
    public Vector2 Position { get; set; }

    [Export]
    public bool IsVisited { get; set; }
}
