using Godot;
using System;

public partial class MeleeBaseSwing: GunBase
{
    [Export] public PackedScene _swingScene;
    public float _globalRotation;

    public override bool Shoot(Vector2 direction)
    {
        _globalRotation = direction.Angle();
        SpawnProjectile();
        return true;
    }
    public void SpawnProjectile() {
        BulletFactory.CreateMeleeAttack(_swingScene, BulletFactory.CreateBulletSpawnData(this), GlobalRotation);
    }
}
