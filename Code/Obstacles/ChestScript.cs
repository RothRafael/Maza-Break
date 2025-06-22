using Godot;
using System;

public partial class ChestScript : ObstacleBaseScript
{
    [Export] private string GunPrefabFolder = "res://Prefabs/Guns/";

    public override void DoAction()
    {
        DefaultCoinSpawner coinSpawner = new DefaultCoinSpawner(GetTree().Root, GD.Load<PackedScene>("res://Prefabs/Coins/Coin.tscn"));
        int coinCount = GD.RandRange(1, 5);
        for (int i = 0; i < coinCount; i++)
        {
            coinSpawner.SpawnAtCloseToPosition(Position, 10);
        }
        QueueFree();
    }

    public class DefaultCoinSpawner : CoinSpawner
    {
        public DefaultCoinSpawner(Node root, PackedScene coinScene) : base(root, coinScene) { }
    }

    public static class TscnPicker
    {
        public static Godot.Collections.Array GetPackedScenes(string folderPath)
        {
            var dir = DirAccess.Open(folderPath);
            var scenes = new Godot.Collections.Array();

            if (dir == null)
            {
                GD.PrintErr($"Could not open directory: {folderPath}");
                return scenes;
            }

            dir.ListDirBegin();
            string fileName = dir.GetNext();
            while (fileName != "")
            {
                if (!dir.CurrentIsDir() && fileName.EndsWith(".tscn"))
                {
                    var scenePath = $"{folderPath}/{fileName}";
                    var packedScene = GD.Load<PackedScene>(scenePath);
                    if (packedScene != null)
                    {
                        scenes.Add(packedScene);
                    }
                }
                fileName = dir.GetNext();
            }
            dir.ListDirEnd();

            return scenes;
        }
    }

    public abstract class CoinSpawner
    {
        protected Node Root;
        protected PackedScene CoinScene;

        public CoinSpawner(Node root, PackedScene coinScene)
        {
            Root = root;
            CoinScene = coinScene;
        }

        public virtual void SpawnCoin()
        {
            SpawnRandom();
        }

        public void SpawnRandom()
        {
            if (CoinScene == null)
            {
                GD.PrintErr("Coin scene is not set.");
                return;
            }

            var spawnPoints = LevelManagerScript.Instance.GetSpawnPoints();
            if (spawnPoints == null || spawnPoints.Count == 0)
            {
                GD.PrintErr("No spawn points available for coins.");
                return;
            }

            var randomIndex = GD.RandRange(0, spawnPoints.Count - 1);
            var coinInstance = CoinScene.Instantiate() as Node2D;
            if (coinInstance != null)
            {
                coinInstance.Position = spawnPoints[randomIndex];
                Root.AddChild(coinInstance);
            }
            else
            {
                GD.PrintErr("Coin instance is not a Node2D.");
            }
        }

        public void SpawnAtCloseToPosition(Vector2 targetPosition, float areaRadius)
        {
            if (CoinScene == null)
            {
                GD.PrintErr("Coin scene is not set.");
                return;
            }

            var spawnPoints = LevelManagerScript.Instance.GetSpawnPoints();
            if (spawnPoints == null || spawnPoints.Count == 0)
            {
                GD.PrintErr("No spawn points available for coins.");
                return;
            }

            // Filter spawn points within areaRadius of targetPosition
            var closePoints = new Godot.Collections.Array<Vector2>();
            foreach (var point in spawnPoints)
            {
                if (point.DistanceTo(targetPosition) <= areaRadius)
                {
                    closePoints.Add(point);
                }
            }

            if (closePoints.Count == 0)
            {
                GD.PrintErr("No spawn points close to the target position.");
                return;
            }

            var randomIndex = GD.RandRange(0, closePoints.Count - 1);
            var coinInstance = CoinScene.Instantiate() as Node2D;
            if (coinInstance != null)
            {
                coinInstance.Position = closePoints[randomIndex];
                Root.AddChild(coinInstance);
            }
            else
            {
                GD.PrintErr("Coin instance is not a Node2D.");
            }
        }
    }
}