using Godot;
using System;
using System.Diagnostics;

[Tool]
public partial class BulletThatMakesOthers : ProjectileBase
{
    [Export] public PackedScene SubBulletScene { get; set; }
    [Export] public PackedScene _impactEffect { get; set; }
    [Export] public int BulletCount { get; set; } = 10;
    [Export] public float Spread { get; set; } = 360f;
    private SoundComponent _soundComponent;

    private BulletSpawnData _bulletSpawnData;
    private BulletFactory _bulletFactory;

    public override void _Ready()
    {
        base._Ready();

        // _soundComponent = GetNode<SoundComponent>("SoundComponent");
    }

    private void _on_body_entered(Node2D body)
    {
        if (body.Name == "Player") return;
        Debug.Print($"Hit {body.Name}");
        base.OnHit(body);

        SetData();
        SpawnSubBullets();
        QueueFree();
        SpawnParticles();
    }

    private void SpawnSubBullets()
    {
        // _soundComponent.PlaySound();
        BulletFactory.CreateShotgunBullets(SubBulletScene, _bulletSpawnData, BulletCount);
    }
    private void SetData()
    {
        _bulletSpawnData = new BulletSpawnData
        {
            Spread = Spread,
            Direction = _velocity,
            BulletSpeed = speed * 1.5f,
            Damage = Damage,
            GlobalPosition = GlobalPosition,
            Tree = GetTree()
        };
    }
    public void SpawnParticles()
    {
        if (_impactEffect == null) return;
        else
        {
            var impactEffect = _impactEffect.Instantiate<Node2D>();
            impactEffect.GlobalPosition = GlobalPosition;
            GetParent().AddChild(impactEffect);
        }
    }
}
