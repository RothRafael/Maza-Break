using Godot;
using System;

public partial class CoinComponent : Node
{
    [Export] private PackedScene _coinScene;
    public void InstantiateCoin(int ammount, Vector2 position)
    {
        for (int i = 0; i < ammount; i++)
        {
            RigidBody2D coinInstance = (RigidBody2D)_coinScene.Instantiate();
            coinInstance.GlobalPosition = position;


            // Em vez de AddChild direto, usamos call_deferred:
            GetTree().Root.CallDeferred("add_child", coinInstance);
        }
    }
}
