using Godot;
using System;

public partial class BolaSinucaProjectile : ProjectileBase
{
    [Export] private PackedScene _ImpactEffect;
    private Sprite2D _sprite;
    private SoundComponent _soundComponent;
    public override void _Ready()
    {
        base._Ready();
        _sprite = GetNode<Sprite2D>("Sprite"); if (_sprite == null) GD.PrintErr("Sprite not found on BolaSinucaProjectile");
        // _soundComponent = GetNode<SoundComponent>("SoundComponent"); if (_soundComponent == null) GD.PrintErr("SoundComponent not found on BolaSinucaProjectile");
        SetSpriteIndex();
    }


    public void SetSpriteIndex()
    {
        _sprite.Frame = (int)(GD.Randi() % 10);
    }
    private void _on_body_entered(Node2D body)
    {
        if (body.Name == "Player") return;
        base.OnHit(body);
        OnWallHit();

    }
    public void OnWallHit()
    {
        GD.Print("Enemy bullet hit a wall");
        SpawnParticles();
        QueueFree(); // Remove the bullet on wall hit
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
