using Godot;
using System;
using System.IO;

public partial class HitNumberFactory : Node
{
    private static HitNumberFactory _instance;
    public static HitNumberFactory Instance => _instance;

    [Export] public PackedScene HitNumberScene = ResourceLoader.Load<PackedScene>("res://Prefabs/Components/enemy_hit_numbers.tscn");

    public override void _Ready()
    {
        if (_instance != null)
        {
            GD.PrintErr("HitNumberFactory instance already exists!");
            QueueFree();
            return;
        }
        _instance = this;
    }

    public Node2D CreateHitNumber(Vector2 position, int damdage, bool isCritial)
    {
        if (HitNumberScene == null)
        {
            GD.PrintErr("HitNumberScene is not assigned!");
            return null;
        }

        var hitNumberInstance = HitNumberScene.Instantiate<Node2D>();
        if (hitNumberInstance != null)
        {
            GetTree().Root.AddChild(hitNumberInstance); // acho q isso vai dar problema
            hitNumberInstance.GlobalPosition = position;
            hitNumberInstance.GetNode<Label>("HitNumberLabel").Text = damdage.ToString(); // Set the hit number text
        }
        return hitNumberInstance;
    }
}
