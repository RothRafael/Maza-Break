using Godot;
using System;

public partial class GameOverSceneScript : Node
{
    public override void _Ready()
    {
        // Connect the button signal to the method
        var restartButton = GetNode<Button>("RestartButton");
        restartButton.Pressed += OnRestartButtonPressed;
    }

    private void OnRestartButtonPressed()
    {
        // Load the main scene
        GD.Print("Restarting the game...");
        GetTree().ChangeSceneToFile("res://cenas/cena_teste.tscn");
    }
    private void OnExitButtonPressed()
    {
        // Exit the game
        GD.Print("Exiting the game...");
        GetTree().Quit();
    }
}
