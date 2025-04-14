using Godot;
using System;

public partial class MeleeScript : GunBase
{
    [Export] private PackedScene _bulletScene;
    [Export] private Node2D _muzzle;
    [Export] private Vector2 offset;

    private SoundComponent _soundComponent;
    public override void _Ready()
    {
        base._Ready();
        _muzzle = GetNode<Node2D>("Muzzle");
        _soundComponent = GetNode<SoundComponent>("SoundComponent");
    }
    public override bool Shoot(Vector2 direction)
    {
        if (!CanShoot())
            return false;
        if (base.Shoot(direction) == false)
            return false;

        StartSwing();
        return true;
    }
    private void StartSwing()
    {
        PlayAnim();
        CreateProjectile();
        _soundComponent.PlaySound();
    }
    public void CreateProjectile()
    {
        timeSinceLastShot = 0f;
        BulletFactory.CreateBullet(_bulletScene, BulletFactory.CreateBulletSpawnData(this), offset);
        CameraShakeComponent.Instance.Shake(1f, 0.1f);
    }
    private void PlayAnim()
    {
        AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer?.Play("atack");
    }
}
