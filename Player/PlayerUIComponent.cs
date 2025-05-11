using Godot;
using System;

public partial class PlayerUIComponent : Node
{
    [Export] public NodePath PlayerUiPath; // Drag PlayerUI in the Inspector
    [Export] private PackedScene GameOverScenePath; // Drag GameOverScene in the Inspector

    private Label _healthLabel;
    private Label _energiaLabel;
    private Label _armorLabel;

    private PlayerStatus _playerStatus;

    public override void _Ready()
    {
        _playerStatus = PlayerStatus.Instance;
        var uiNode = GetNode<CanvasLayer>(PlayerUiPath);
        var vboxContainer = uiNode.GetNode<Panel>("Panel").GetNode<VBoxContainer>("VBoxContainer");
        _healthLabel = vboxContainer.GetNode<Label>("VidaLabel");
        _energiaLabel = vboxContainer.GetNode<Label>("EnergiaLabel");
        _armorLabel = vboxContainer.GetNode<Label>("ArmorLabel");
        InitializeVariables();
        ThrowErrors();

        UpdateUI(_playerStatus.GetVida(), _playerStatus.GetEnergia(), _playerStatus.GetArmor());
    }

    public void UpdateUI(int health, int ammo, int armor)
    {
        _healthLabel.Text = $"Vida: {health}";
        _energiaLabel.Text = $"Energia: {ammo}";
        _armorLabel.Text = $"Armor: {armor}";
    }
    private void InitializeVariables()
    {

    }
    public void ShowGameOver()
    {
        
        var gameOverScene = GameOverScenePath.Instantiate();
        AddChild(gameOverScene);

    }
    private void ThrowErrors()
    {
        if (_healthLabel == null)
        {
            GD.PrintErr("Health label not found!");
        }
        if (_energiaLabel == null)
        {
            GD.PrintErr("Energy label not found!");
        }
    }


}
