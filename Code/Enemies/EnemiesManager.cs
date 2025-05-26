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
    [Export] string path = "res://Prefabs/Enemies/enemy.tscn";

    // Texture paths
    [Export] private String textureFolderPath = "res://Prefabs/enemieTextures/";
    // Temp
    [Export] private Array<PackedScene> textureScenes = new Array<PackedScene>();


    private double _timeElapsed = 0;
    private List<Enemy> _enemies = new List<Enemy>();
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

        _timeElapsed += delta;
        if (_timeElapsed >= spawnDelay)
        {
            _timeElapsed = 0;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        GD.Print("Spawning enemy...");
        var spawnPoints = LevelManagerScript.Instance.GetSpawnPoints();
        if (spawnPoints.Count > 0)
        {
            Vector2 spawnPoint = GetRandomSpawnPoint(spawnPoints);
            var enemyInstance = CreateEnemyInstance(spawnPoint);
            AddEnemyToScene(enemyInstance, spawnPoint);
        }
    }

    private Vector2 GetRandomSpawnPoint(List<Vector2> spawnPoints)
    {
        var playerPosition = PlayerStatus.Instance.playerPosition;

        Random random = new Random();
        int randomIndex = random.Next(0, spawnPoints.Count);
        var result = spawnPoints[randomIndex];

        if (result.DistanceTo(playerPosition) < _spawnDistance)
        {
            // If the spawn point is too close to the player, try again
            return GetRandomSpawnPoint(spawnPoints);
        }
        else
        {
            return result;
        }


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
    private void PickRandomTexture(Enemy enemy)
    {
        var texture = textureScenes.PickRandom();
        var textureInstance = texture.Instantiate() as Sprite2D;
        enemy.setTexture(textureInstance);
    }
}
