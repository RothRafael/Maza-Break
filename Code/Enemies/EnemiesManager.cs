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
    [Export] private bool _playerEnterLevel = false;
    [Export] string path = "res://Prefabs/Enemies/enemy.tscn";

    // Texture paths
    [Export] private String textureFolderPath = "res://Prefabs/enemieTextures/";
    [Export] private Array<PackedScene> textureScenes = new Array<PackedScene>();
    private double _timeElapsed = 0;
    private List<Enemy> _enemies = new List<Enemy>();

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
        if (!_canSpawn) return;

        if (!_playerEnterLevel)
        {
            CheckPlayerPosition();
            return;
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
        _enemies.Clear(); // Clear existing enemies
        _timeElapsed = 0; // Reset spawn timer
        _canSpawn = true;
        spawnPoints = LevelManagerScript.Instance.GetSpawnPoints();
    }
    public void PlayerExitedRoom()
    {
        GD.Print("Player exited room, stopping enemy spawn...");
        _canSpawn = false;
        _playerEnterLevel = false;
        KillAllEnemies();
    }
    public void SpawnEnemy()
    {
        Room currentRoom = LevelManagerScript.Instance.GetCurrentRoom();
        if(_enemies.Count >= maxEnemies)
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
        // After 100 tries, just return the last picked spawn point regardless of distance
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
        PickRandomTexture(enemyInstance);
        AddChild(enemyInstance);
        ((Node2D)enemyInstance).Position = spawnPoint;
        _enemies.Add(enemyInstance);

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
}
