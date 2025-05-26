using Godot;
using System;

public partial class MainMenuScript : Node2D
{
    public override void _Ready()
    {
        // Called when the node is added to the scene for the first time.
        // Initialization code here.
    }

    public override void _Process(double delta)
    {
        // Called every frame. 'delta' is the elapsed time since the previous frame.
        // Update code here.
    }

    public void OnPlayButtonPressed()
    {
        GD.Print("Play button pressed");
        GetTree().ChangeSceneToFile("res://cenas/MainScene.tscn");
    }

    public void OnExitButtonPressed()
    {
        GetTree().Quit();
    }
}
