using Godot;
using System;

public partial class PlayerUI : Control
{
    [Export] private Label _vidaText;
    [Export] private Label _energiaText;
    [Export] private Label _armorText;
    private PlayerStatus _playerStatus;
    public override void _Ready()
    {
        _vidaText = GetNode<Label>("VidaLabel");
        _energiaText = GetNode<Label>("ManaLabel");
        _armorText = GetNode<Label>("ArmorLabel");
        _playerStatus = PlayerStatus.Instance;
        
        UpdateUI(_playerStatus.GetVida(), _playerStatus.GetEnergia(), _playerStatus.GetArmor());
    }
    public void UpdateUI(int vida, int mana, int armor)
    {
        _vidaText.Text = $"Vida: {vida}";
        _energiaText.Text = $"Mana: {mana}";
        _armorText.Text = $"Armor: {armor}";
    }
    public void UpdateVida(int vida)
    {
        _vidaText.Text = $"Vida: {vida}";
    }
    public void UpdateEnergia(int mana)
    {
        _energiaText.Text = $"Mana: {mana}";
    }
    public void UpdateArmor(int armor)
    {
        _armorText.Text = $"Armor: {armor}";
    }
}
