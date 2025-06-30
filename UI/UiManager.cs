using Godot;
using System;

public partial class UiManager : Node
{
    [Export] private PackedScene _uiScene;
    private static UiManager _instance;
    public static UiManager Instance => _instance;

    [Export] private PackedScene _bossDefeatedUiScene;

    public override void _Ready()
    {
        base._Ready();
        if (_instance == null)
        {
            _instance = this;
            GD.Print("UiManager instance created");
        }
        else
        {
            GD.Print("UiManager instance already exists");
            QueueFree();
        }
    }

    public void ShowUi()
    {
        var ui = _uiScene.Instantiate();
        AddChild(ui);
    }
    public void ShowBossDefeatedMessage()
    {
        GD.Print("Boss defeated! Displaying message...");
        var tree = GetTree();
        foreach (Node child in tree.Root.GetChildren())
        {
            child.QueueFree();
        }
        var bossDefeatedUi = _bossDefeatedUiScene.Instantiate();
        tree.Root.AddChild(bossDefeatedUi);
    }
    }

