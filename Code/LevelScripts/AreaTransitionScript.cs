using Godot;
using System;

public partial class AreaTransitionScript : Area2D
{
    private void _on_body_entered(Node2D body)
    {
        if (body is PlayerController player)
        {
            // Assuming you have a method to handle the transition
            HandleTransition(player);
        }
    }
    private void HandleTransition(PlayerController player)
    {
        GD.Print("Levando o jogador para a próxima área");
        player.Position = new Vector2(100, 100);
    }
}
