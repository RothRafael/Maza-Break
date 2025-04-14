using Godot;
using System;

public partial class PlayerCollectComponent : Node
{
    int n = 0;
    private void _on_area_2d_body_entered(Node2D body)
    {
        if (body is ICollectable collectable)
        {
            collectable.Collect();
            GD.Print("Collected: " + n);
            n++;
        }
    }
}
