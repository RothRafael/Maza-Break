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

    [Export] public Array<TileMapLayer> baseRoomLayers = new Array<TileMapLayer>();
    [Export] public Array<TileMapLayer> wallRoomLayers = new Array<TileMapLayer>();
    [Export] Array<Room> rooms = new Array<Room>();

    // current room
    public Room currentRoom;
    public override void _Ready()
    {
        if (_instance != null && _instance != this)
        {
            QueueFree();
            return;
        }
        _instance = this;

        LoadFirstRoom();
        // SearchRooms();
        // GenerateSpawnPoints();
        // DisableRooms();
    }

    public override void _Process(double delta)
    {
        HandleInput();
    }
    private void HandleInput()
    {
        if (Input.IsActionJustPressed("next_room"))
        {
            // NextRoom();
        }
    }
    private void LoadFirstRoom()
    {
        currentRoom = rooms.FirstOrDefault();
        LoadRoom();
    }
    private void LoadRoom()
    {
        if (currentRoom == null) return;

        int baseLayer = currentRoom.baseLayer;
        int wallLayer = currentRoom.wallLayer;
        // Load base room layer
        TileMapLayer baseLayerNode = baseRoomLayers[baseLayer];
        TileMapLayer wallLayerNode = wallRoomLayers[wallLayer];

        GetTree().Root.AddChild(baseLayerNode);
        GetTree().Root.AddChild(wallLayerNode);
        baseLayerNode.Visible = true;
        wallLayerNode.Visible = true;
        baseLayerNode.ProcessMode = ProcessModeEnum.Inherit;
        wallLayerNode.ProcessMode = ProcessModeEnum.Inherit;
    }

    //     private void GenerateSpawnPoints()
    //     {
    //         foreach (var room in rooms)
    //         {
    //             ProcessRoomForSpawnPoints(room);
    //         }
    //     }

    //     public void ProcessRoomForSpawnPoints(TileMapLayer room)
    //     {
    //         // Get the used rectangle which includes position and size
    //         var usedRect = room.GetUsedRect();

    //         // Iterate over the actual tile positions in the used rect
    //         for (int x = usedRect.Position.X; x < usedRect.Position.X + usedRect.Size.X; x++)
    //         {
    //             for (int y = usedRect.Position.Y; y < usedRect.Position.Y + usedRect.Size.Y; y++)
    //             {
    //                 Vector2I tilePos = new Vector2I(x, y);
    //                 // Get the tile data, check if it exists and has the custom data
    //                 var data = room.GetCellTileData(tilePos);
    //                 if (data != null && data.GetCustomData("CanSpawn").AsBool())
    //                 {
    //                     // GD.Print("Spawn point found at: " + tilePos);
    //                     currentRoomSpawnPoints.Add(room.MapToLocal(tilePos));
    //                     SpawnCrates(room, tilePos);
    //                 }
    //             }
    //         }
    //     }
    //     private void SpawnCrates(TileMapLayer room, Vector2I tilePos)
    //     {
    //         Random random = new Random();
    //         // Randomly decide whether to spawn a crate
    //         int rand = random.Next(1, 11); // Random number between 1 and 10
    //         if (rand > 1) return;
    //         string path = "res://Prefabs/Obstacle/caixa.tscn";
    //         var scene = GD.Load<PackedScene>(path);
    //         var instance = scene.Instantiate();
    //         room.AddChild(instance);
    //         ((Node2D)instance).Position = room.MapToLocal(tilePos);
    //     }

    //     public void SearchRooms()
    //     {
    //         rooms.Clear();
    //         var tileMapLayers = GetChildren();
    //         foreach (var layer in tileMapLayers)
    //         {
    //             if (layer is TileMapLayer tileMapLayer)
    //             {
    //                 if (tileMapLayer.Name.ToString().StartsWith("Room"))
    //                 {
    //                     rooms.Add(tileMapLayer);
    //                 }
    //             }
    //         }
    //         GD.Print("Rooms found: " + rooms.Count);
    //     }
    //     private void DisableRooms()
    //     {
    //         foreach (var room in rooms)
    //         {
    //             room.ProcessMode = ProcessModeEnum.Disabled;
    //             room.Visible = false;
    //         }
    //         if (rooms.Count > 0)
    //         {
    //             currentRoom = rooms.First();
    //             currentRoom.Visible = true;
    //             currentRoom.ProcessMode = ProcessModeEnum.Inherit;
    //             currentRoomSpawnPoints.Clear();
    //             ProcessRoomForSpawnPoints(currentRoom);
    //         }
    //     }
    //     public void NextRoom()
    //     {
    //         if (currentRoom != null)
    //         {
    //             currentRoom.Visible = false;
    //             currentRoom.ProcessMode = ProcessModeEnum.Disabled;
    //         }
    //         int currentIndex = rooms.IndexOf(currentRoom);
    //         int nextIndex = (currentIndex + 1) % rooms.Count; // Loops back to the first room if at the last room
    //         currentRoom = rooms[nextIndex];
    //         currentRoom.Visible = true;
    //         currentRoom.ProcessMode = ProcessModeEnum.Inherit;
    //         currentRoomSpawnPoints.Clear();
    //         ProcessRoomForSpawnPoints(currentRoom);
    //     }

    //     public List<Vector2> GetSpawnPoints()
    //     {
    //         return currentRoomSpawnPoints;
    //     }
    // }

}