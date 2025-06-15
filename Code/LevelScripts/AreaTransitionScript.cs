using Godot;
using System;

public partial class AreaTransitionScript : Node2D
{
	private void _on_body_entered(Node2D body)
	{
		if (body is PlayerController player)
		{
			// Assuming you have a method to handle the transition
			HandleTransition(player);
		}
	}
	private void _on_area_exited(Node2D area)
	{
		if (area is PlayerController player)
		{
			
		}
	}
	private void HandleTransition(PlayerController player)
	{
		var nome = this.Name.ToString();
		LevelManagerScript.Instance.NextRoom(nome, player);
	}
}
