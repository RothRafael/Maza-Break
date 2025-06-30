using System.Collections.Generic;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class LevelManagerScript : Node
{
    private static LevelManagerScript _instance;
    public static LevelManagerScript Instance => _instance;

    [Export] public Array<PackedScene> baseRoomLayers = new Array<PackedScene>();
    [Export] public Array<PackedScene> wallRoomLayers = new Array<PackedScene>();
    [Export] Array<Room> rooms = new Array<Room>();

    private Array<TileMapLayer> CurrentRoomLayers = new Array<TileMapLayer>();

    private string lastDoor;
    private Room lastRoom;
    public Room currentRoom;
    private PlayerController playerController;
    private bool _playerIsInRoom = false;
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
    public void LoadFirstRoom()
    {
        currentRoom = rooms[0];
        LoadRoom(currentRoom, "null");
    }
    private void LoadRoom(Room roomToLoad, string door)
    {
        if (roomToLoad == null) return;


        DisableRoom(currentRoom);
        lastRoom = currentRoom;
        lastDoor = door;
        currentRoom = roomToLoad;

        GD.Print("Loading room: " + roomToLoad.nome);

        int baseLayer = roomToLoad.baseLayer;
        int wallLayer = roomToLoad.wallLayer;
        // Load base room layer
        TileMapLayer baseLayerNode = baseRoomLayers[baseLayer].Instantiate() as TileMapLayer;
        TileMapLayer wallLayerNode = wallRoomLayers[wallLayer].Instantiate() as TileMapLayer;

        CurrentRoomLayers.Add(baseLayerNode);
        CurrentRoomLayers.Add(wallLayerNode);

        GetTree().Root.CallDeferred("add_child", baseLayerNode);
        GetTree().Root.CallDeferred("add_child", wallLayerNode);
        baseLayerNode.Visible = true;
        wallLayerNode.Visible = true;
        baseLayerNode.ProcessMode = ProcessModeEnum.Inherit;
        wallLayerNode.ProcessMode = ProcessModeEnum.Inherit;

        TeleportPlayerToRoom(currentRoom, door);
    }
    public void NextRoom(string nome, PlayerController player)
    {
        if (!PlayerStatus.Instance.canTeleport) return;
        playerController = player;

        switch (nome)
        {
            case "N":
                LoadRoom(rooms[currentRoom.n], "S");
                break;
            case "S":
                LoadRoom(rooms[currentRoom.s], "N");
                break;
            case "L":
                LoadRoom(rooms[currentRoom.l], "O");
                break;
            case "O":
                LoadRoom(rooms[currentRoom.o], "L");
                break;
        }
        if (!EnemiesManager.instance._playerEnterLevel)
        {
            EnemiesManager.instance.PlayerEnteredRoom();
            GD.Print("Player entered room: " + currentRoom.nome);
        }
        else
        {
            GD.Print("Player re-entered room: " + currentRoom.nome);
            EnemiesManager.instance.PlayerExitedRoom();
            EnemiesManager.instance.PlayerEnteredRoom();
        }
    }

    private void DisableRoom(Room room)
    {
        if (room == null)
        {
            return;
        }
        foreach (var layer in CurrentRoomLayers)
        {
            layer.Visible = false;
            layer.CallDeferred("set_process_mode", (int)ProcessModeEnum.Disabled);
            layer.CallDeferred("queue_free");
        }
        CurrentRoomLayers.Clear();
    }
    private void TeleportPlayerToRoom(Room room, string door)
    {
        if (room == null) return;
        PlayerStatus.Instance.canTeleport = false;


        playerController.Position = CurrentRoomLayers[0].GetNode<Node2D>(door).Position;
        EnemiesManager.instance.PlayerEnteredRoom();
        GD.Print("Teleporting player to room: " + room.nome + " at door: " + door);

    }
    public List<Vector2> GetSpawnPoints()
    {
        var spawnPoints = new List<Vector2>();

        if (currentRoom != null && CurrentRoomLayers.Count >= 2)
        {
            var wallLayer = CurrentRoomLayers[1];

            var usedRect = wallLayer.GetUsedRect();

            for (int x = usedRect.Position.X; x < usedRect.Position.X + usedRect.Size.X; x++)
            {
                for (int y = usedRect.Position.Y; y < usedRect.Position.Y + usedRect.Size.Y; y++)
                {
                    var tilePos = new Vector2I(x, y);

                    var wallData = wallLayer.GetCellTileData(tilePos);

                    if (wallData != null && wallData.GetCustomData("CanSpawn").AsBool())
                    {
                        spawnPoints.Add(wallLayer.MapToLocal(tilePos));
                    }
                }
            }
        }

        return spawnPoints;
    }
    public void LevelFinished()
    {
        GD.Print("Level Finished!");
        SpawnChest();
    }
    public void SpawnChest()
    {
        if (currentRoom == null) return;

        string path = "res://Prefabs/Obstacle/Chest.tscn";
        var scene = GD.Load<PackedScene>(path);
        var instance = scene.Instantiate();

    }
    public Room GetCurrentRoom()
    {
        return currentRoom;
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
