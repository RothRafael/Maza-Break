using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

public partial class EnemiesManager : Node2D
{
    public static EnemiesManager instance;
    [Export] public float spawnDelay = 0.1f;
    [Export] private int maxEnemies = 10;
    [Export] private int _spawnDistance = 300;
    [Export] private bool _canSpawn = false;
    [Export] public bool _playerEnterLevel = false;
    [Export] string path = "res://Prefabs/Enemies/enemy.tscn";

    // Texture paths
    [Export] private String textureFolderPath = "res://Prefabs/enemieTextures/";
    [Export] private String ChestPath = "res://Prefabs/Obstacles/Chest.tscn";
    [Export] private Array<PackedScene> textureScenes = new Array<PackedScene>();
    private double _timeElapsed = 0;
    private List<Enemy> _enemies = new List<Enemy>();
    private int spawnedEnemiesCount = 0;
    private bool sceneHasChest = false;

    private List<Vector2> spawnPoints = new List<Vector2>();
    public override void _Ready()
    {
        if (instance != null && instance != this)
        {
            QueueFree();
            return;
        }
        instance = this;
    }

    public override void _Process(double delta)
    {
        if (!_canSpawn || !_playerEnterLevel) return;

        if (!sceneHasChest && spawnedEnemiesCount >= maxEnemies)
        {
            SpawnChest();
            sceneHasChest = true;
        }

        _timeElapsed += delta;
        if (_timeElapsed >= spawnDelay)
        {
            _timeElapsed = 0;
            SpawnEnemy();
        }
    }
    public void PlayerEnteredRoom()
    {
        GD.Print("Player entered room, spawning enemies...");
        _timeElapsed = 0; // Reset spawn timer
        _canSpawn = true;
        _playerEnterLevel = true;
        sceneHasChest = false; // Reset chest state
        spawnedEnemiesCount = 0; // Reset enemy count
        spawnPoints = LevelManagerScript.Instance.GetSpawnPoints();
        _enemies.Clear(); // Clear existing enemies
    }
    public void PlayerExitedRoom()
    {
        GD.Print("Player exited room, stopping enemy spawn...");
        _canSpawn = true;
        _playerEnterLevel = true;
        spawnedEnemiesCount = 0;
        sceneHasChest = false;
        KillAllEnemies();
    }
    public void SpawnEnemy()
    {
        Room currentRoom = LevelManagerScript.Instance.GetCurrentRoom();
        if (currentRoom.isBossRoom)
        {
            GD.Print("Cannot spawn enemies in boss room.");
            KillAllEnemies();
            return;
        }

        if (_enemies.Count >= maxEnemies)
        {
            GD.Print("Max enemies reached, not spawning more.");
            return;
        }

        if (spawnPoints.Count > 0)
        {
            GD.Print("Spawning enemy...");
            Vector2 spawnPoint = GetRandomSpawnPoint(spawnPoints);
            var enemyInstance = CreateEnemyInstance(spawnPoint);
            AddEnemyToScene(enemyInstance, spawnPoint);
            spawnedEnemiesCount++;
        }
    }

    private Vector2 GetRandomSpawnPoint(List<Vector2> spawnPoints)
    {
        var playerPosition = PlayerStatus.Instance.playerPosition;
        Random random = new Random();
        Vector2 result = spawnPoints[0];

        for (int i = 0; i < 100; i++)
        {
            int randomIndex = random.Next(0, spawnPoints.Count);
            result = spawnPoints[randomIndex];
            if (result.DistanceTo(playerPosition) >= _spawnDistance)
            {
                return result;
            }
        }
        return result;
    }

    private Enemy CreateEnemyInstance(Vector2 spawnPoint)
    {
        var scene = GD.Load<PackedScene>(path);
        var instance = scene.Instantiate();
        return (Enemy)instance;
    }

    private void AddEnemyToScene(Enemy enemyInstance, Vector2 spawnPoint)
    {
        ((Node2D)enemyInstance).Position = spawnPoint;
        _enemies.Add(enemyInstance);
        PickRandomTexture(enemyInstance);
        AddChild(enemyInstance);

    }
    public void KillAllEnemies()
    {
        GD.Print("Killing all enemies...");
        foreach (var enemy in _enemies)
        {
            if (enemy != null && enemy.IsInsideTree())
            {
                enemy.QueueFree();
            }
        }
        _enemies.Clear();
    }
    private void CheckPlayerPosition()
    {
        var playerPosition = PlayerStatus.Instance.playerPosition;
        GD.Print("Checking player position: " + playerPosition);
        foreach (var spawnPoint in spawnPoints)
        {
            if (playerPosition.DistanceTo(spawnPoint) < 10.0f) // Adjust threshold as needed
            {
                GD.Print(playerPosition);
                _playerEnterLevel = true;
                break;
            }
        }
    }
    private void PickRandomTexture(Enemy enemy)
    {
        var texture = textureScenes.PickRandom();
        var textureInstance = texture.Instantiate() as Sprite2D;
        enemy.setTexture(textureInstance);

        GD.Print("Picked texture: " + textureInstance.Texture.ResourcePath);

    }
    public void EnemieDied(Enemy enemy)
    {
        if (_enemies.Contains(enemy))
        {

        }
    }
    private void SpawnChest()
    {
        GD.Print("Spawning chest...");
        var chestScene = GD.Load<PackedScene>(ChestPath);
        if (chestScene != null)
        {
            var chestInstance = chestScene.Instantiate();
            if (chestInstance is Node2D chestNode)
            {
                // Set the position of the chest to a random spawn point
                chestNode.Position = GetRandomSpawnPoint(spawnPoints);
                AddChild(chestNode);
                GD.Print("Chest spawned successfully.");
            }
            else
            {
                GD.PrintErr("Chest instance is not a Node2D.");
            }
        }
        else
        {
            GD.PrintErr("Failed to load chest scene.");
        }
    }
}