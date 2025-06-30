using Godot;
using System;

public partial class GameOverSceneScript : Node
{
    public override void _Ready()
    {
        // Connect the button signal to the method
    }

    private void OnRetryPressed()
    {
        // Load the main scene
        GD.Print("Play button pressed");
        GetTree().ChangeSceneToFile("res://cenas/MainScene.tscn");
    }
    private void OnExitButtonPressed()
    {
        // Exit the game
        GD.Print("Exiting the game...");
        GetTree().Quit();
    }
}
