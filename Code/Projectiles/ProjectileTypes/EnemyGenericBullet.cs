using Godot;
using System;

public partial class EnemyGenericBullet : ProjectileBase
{
    [Export] private PackedScene _ImpactEffect;
    public override void _Ready()
    {
        shooterFaction = Faction.Enemy;
        base._Ready();
    }
    private void _on_body_entered(Node2D body)
    {
        if(body is Enemy enemy && shooterFaction == Faction.Enemy) return;

        if(body.Name ==  "Player")
        {
            GD.Print("Player hit");
            PlayerStatus.Instance.TakeDamage(Damage, isCrit);
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
