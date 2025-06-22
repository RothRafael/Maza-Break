using Godot;
using System;
using System.Diagnostics;

public partial class GunBolas : GunBase
{
    [Export] private PackedScene _bulletScene;
    [Export] private int _bulletCount = 22;
    public int _spriteIndex = 1;
    private int maxIndex = 9;

    
    private Sprite2D sprite;
    private SoundComponent _soundComponent;
    public override void _Ready()
    {
        base._Ready();
        sprite = GetNode<Sprite2D>("Sprite");
        _soundComponent = GetNode<SoundComponent>("SoundComponent");
    }
    public override bool Shoot(Vector2 direction)
    {
        if (!CanShoot())
            return false;

        if (base.Shoot(direction) == false)
            return false;

        CreateProjectile();

        return true;
    }

    private void CreateProjectile()
    {
        
        timeSinceLastShot = 0f;
        _soundComponent.PlaySound();
        BulletFactory.CreateBullet(_bulletScene, BulletFactory.CreateBulletSpawnData(this), _bulletCount);
        CameraShakeComponent.Instance.Shake(1f, 0.1f);
    }
}
