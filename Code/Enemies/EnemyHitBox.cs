// using Godot;

// public partial class CollisionBody2D : Area2D
// {
//     [Signal]
//     public delegate void DamagedEventHandler(int damage);

//     public int DamageAmount { get; set; } = 10;

//     public override void _Ready()
//     {
//         Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
//     }

//     private void OnBodyEntered(Node body)
//     {
//         if (body is CharacterBody2D character && character.HasMethod("TakeDamage"))
//         {
//             character.Call("TakeDamage", DamageAmount);
//             EmitSignal(nameof(DamagedEventHandler), DamageAmount);
//         }
//     }
    
// }
