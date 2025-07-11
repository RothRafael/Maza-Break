using Godot;
using System;

public partial class PlayerGenericBullet : ProjectileBase
{
    [Export] private PackedScene _ImpactEffect;
    public override void _Ready()
    {
        base._Ready();
    }
    private void _on_body_entered(Node2D body)
    {
        if (body.Name == "Player")
        {
            return;
        }
        base.OnHit(body);
        OnWallHit();
    }
    public void OnWallHit()
    {
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
        }
        else
        {
            GD.Print("Impact effect not set");
        }
    }
}
