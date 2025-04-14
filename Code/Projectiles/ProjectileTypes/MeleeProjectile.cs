using Godot;
using System;

public partial class MeleeProjectile : ProjectileBase
{
    [Export] private PackedScene _ImpactEffect;
    private SoundComponent _soundComponent;
    public override void _Ready()
    {
        base._Ready();
        _soundComponent = GetNode<SoundComponent>("SoundComponent");
    }
    private void _on_body_entered(Node2D body)
    {
        if(body.Name == "Player" || !isCollisionEnabled) return;
        base.OnHit(body);
        OnWallHit();
    }
    public void OnWallHit()
    {
        _soundComponent.PlaySound();
        GD.Print("Melee bullet hit a wall");
        SpawnParticles();
        CameraShakeComponent.Instance.Shake(0.6f, 0.1f);
        QueueFree();
    }
    public void SpawnParticles()
    {
        if (_ImpactEffect != null)
        {
            var impactEffect = _ImpactEffect.Instantiate<Node2D>();
            impactEffect.GlobalPosition = GlobalPosition;
            GetParent().AddChild(impactEffect);
        } else {
            GD.Print("Impact effect not set");
        }
    }
}

