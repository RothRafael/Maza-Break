// using System.Diagnostics;
// using Godot;

// public partial class ProjectilePiercing : ProjectileDecorator
// {
//     [Export] public float speed = 100;
//     public float pierceDamage;
//     public int pierceCount;
//     public float pierceIncreaseSpeed;
//     public override void OnHit(CharacterBody2D target)
//     {
//         base.OnHit(target);
//         pierceCount--;
//         if (pierceCount <= 0)
//         {
//             QueueFree();
//         }
//     }
//     public override void Launch(Vector2 direction, float playerSpeed)
//     {
//         Debug.Print("launching piercing");
//         float finalSpeed = speed + playerSpeed;
//         base.Launch(direction, finalSpeed);
//     }
// }

// public partial class ProjectileExplosive : ProjectileDecorator
// {
//     [Export] public float speed = 700;
//     PackedScene explosionScene = (PackedScene)ResourceLoader.Load("res://Prefabs/Effects/explosao.tscn");
//     public override void Launch(Vector2 direction, float playerSpeed)
//     {
//         Debug.Print("launching Explosive");
//         float finalSpeed = speed + playerSpeed;
//         base.Launch(direction, finalSpeed);
//     }
//     public override void OnHit(CharacterBody2D target)
//     {
//         base.OnHit(target);
//         GD.Print("Explosive hit target: " + target.Name);
//         var explosion = explosionScene.Instantiate<Node2D>();
//     }
//     public override void Delete() 
//     {
//         TriggerExplosion();
//         base.Delete();
//     }
//     private void TriggerExplosion() {
//         GD.Print("Triggering explosion");
//         var explosion = explosionScene.Instantiate<Node2D>();
//     }
    
// }

