using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[GlobalClass]
public partial class LevelManagerScript : Node
{
    List<TileMapLayer> rooms = new List<TileMapLayer>();

    private TileMapLayer currentRoom;
    public override void _Ready()
    {
        // enemySpawner = GetNode<EnemySpawner>("EnemySpawner");
        SearchRooms();
        GetSpawnPoints();
    }
    public override void _Process(double delta)
    {

    }
    public void GetSpawnPoints()
    {
        foreach (var room in rooms)
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
                    Random random = new Random();
                    int randomNumber = random.Next(1, 101); // Generates a random number between 1 and 100
                    if (data != null && data.GetCustomData("CanSpawn").AsBool() && randomNumber > 90)
                    {
                        GD.Print("Spawn point found at: " + tilePos);
                        string path = "res://Prefabs/Obstacle/caixa.tscn";
                        var scene = GD.Load<PackedScene>(path);
                        var instance = scene.Instantiate();
                        room.AddChild(instance);
                        // Use MapToLocal to get the correct position relative to the TileMap
                        ((Node2D)instance).Position = room.MapToLocal(tilePos);
                    }
                }
            }
        }
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

}

