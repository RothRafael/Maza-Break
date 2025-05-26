using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[GlobalClass]
public partial class LevelManagerScript : Node
{
    private static LevelManagerScript _instance;
    public static LevelManagerScript Instance => _instance;

    List<TileMapLayer> rooms = new List<TileMapLayer>();
    List<Vector2> currentRoomSpawnPoints = new List<Vector2>();
    private readonly int[][] _boxPattern =
    [
        [1, 1],
        [1, 1]
    ];

    private TileMapLayer currentRoom;

    public override void _Ready()
    {
        if (_instance != null && _instance != this)
        {
            QueueFree();
            return;
        }
        _instance = this;

        SearchRooms();
        GenerateSpawnPoints();
    }

    public override void _Process(double delta)
    {

    }

    private void GenerateSpawnPoints()
    {
        foreach (var room in rooms)
        {
            ProcessRoomForSpawnPoints(room);
        }
    }

    public void ProcessRoomForSpawnPoints(TileMapLayer room)
    {
        // Get the used rectangle which includes position and size
        var usedRect = room.GetUsedRect();

        // Iterate over the actual tile positions in the used rect
        for (int x = usedRect.Position.X; x < usedRect.Position.X + usedRect.Size.X; x++)
        {
            for (int y = usedRect.Position.Y; y < usedRect.Position.Y + usedRect.Size.Y; y++)
            {
                Vector2I tilePos = new Vector2I(x, y);
                // Get the tile data, check if it exists and has the custom data
                var data = room.GetCellTileData(tilePos);
                if (data != null && data.GetCustomData("CanSpawn").AsBool())
                {
                    GD.Print("Spawn point found at: " + tilePos);
                    currentRoomSpawnPoints.Add(room.MapToLocal(tilePos));
                    SpawnCrates(room, tilePos);
                }
            }
        }
    }
    private void SpawnCrates(TileMapLayer room, Vector2I tilePos)
    {
        Random random = new Random();
        // Randomly decide whether to spawn a crate
        int rand = random.Next(1, 11); // Random number between 1 and 10
        if (rand > 1) return;
        string path = "res://Prefabs/Obstacle/caixa.tscn";
        var scene = GD.Load<PackedScene>(path);
        var instance = scene.Instantiate();
        room.AddChild(instance);
        ((Node2D)instance).Position = room.MapToLocal(tilePos);
    }

    public void SearchRooms()
    {
        rooms.Clear();
        var tileMapLayers = GetChildren();
        foreach (var layer in tileMapLayers)
        {
            if (layer is TileMapLayer tileMapLayer)
            {
                if (tileMapLayer.Name.ToString().StartsWith("Room"))
                {
                    rooms.Add(tileMapLayer);
                }
            }
        }
        GD.Print("Rooms found: " + rooms.Count);
    }

    public List<Vector2> GetSpawnPoints()
    {
        return currentRoomSpawnPoints;
    }
}

