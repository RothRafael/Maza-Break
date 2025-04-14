using Godot;
using System;

public partial class GunImpactScript : Node2D
{
    [Export] private SoundComponent _soundComponent;
    public override void _Ready()
    {
        _soundComponent = GetNode<SoundComponent>("SoundComponent");
        if (_soundComponent != null)
        {
            _soundComponent.PlaySound();
        }
    }
}
