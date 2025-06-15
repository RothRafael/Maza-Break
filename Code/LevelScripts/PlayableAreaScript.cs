using Godot;
using System;

public partial class PlayableAreaScript : Area2D
{
    public void OnBodyEntered(Node body)
    {
        if (body is PlayerController player)
        {
            GD.Print("Player entered the playable area.");
            // LevelManagerScript.Instance.PlayerEnteredRoom();
        }
    }
}
