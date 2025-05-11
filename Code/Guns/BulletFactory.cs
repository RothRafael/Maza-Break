using Godot;
using System;
using System.Diagnostics;

public abstract partial class BulletFactory : Node
{
    public BulletFactory instance;
    public BulletFactory()
    {
        instance = this;
    }
    public static void CreateBullet(PackedScene BulletScene, BulletSpawnData gunBase)
    {
        CreateBullets(BulletScene, gunBase, 1);
    }
    public static void CreateBullet(PackedScene BulletScene, BulletSpawnData gunBase, Vector2 offset)
    {
        gunBase.GlobalPosition += offset;
        CreateBullets(BulletScene, gunBase, 1);
    }
    public static void CreateBullet(PackedScene BulletScene, BulletSpawnData gunBase, int bulletCount)
    {
        CreateBullets(BulletScene, gunBase, bulletCount);
    }
    private static void CreateBullets(PackedScene BulletScene, BulletSpawnData gunBase, int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            ProjectileBase spawnedBullet = BulletScene.Instantiate<ProjectileBase>();

            // Calculate the spread angle
            float spreadAngle = (float)GD.RandRange(-gunBase.Spread, gunBase.Spread);
            Vector2 spreadDirection = gunBase.Direction.Rotated(Mathf.DegToRad(spreadAngle));

            // Set the bullet's properties
            spawnedBullet.Launch(spreadDirection, gunBase.BulletSpeed);

            spawnedBullet.Damage = GetCritDamage(gunBase.Damage, gunBase.CritChance);
            spawnedBullet.isCrit = IsCrit(spawnedBullet.Damage, gunBase.Damage);
        
            spawnedBullet.pushForce = gunBase.PushForce;

            gunBase.Tree.Root.CallDeferred("add_child", spawnedBullet);
            spawnedBullet.GlobalPosition = gunBase.GlobalPosition;
            spawnedBullet.Rotation = spreadDirection.Angle();
            spawnedBullet.DisableCollision();
        }
    }
    public static void CreateMeleeAttack(PackedScene BulletScene, BulletSpawnData gunBase, float rotation)
    {
        ProjectileBase spawnedBullet = BulletScene.Instantiate<ProjectileBase>();
        spawnedBullet.Damage = GetCritDamage(gunBase.Damage, gunBase.CritChance);
        spawnedBullet.isCrit = IsCrit(spawnedBullet.Damage, gunBase.Damage);
        spawnedBullet.pushForce = gunBase.PushForce;
        spawnedBullet.GlobalPosition = gunBase.GlobalPosition;
        spawnedBullet.Rotation = rotation;
        spawnedBullet.DisableCollision();
        gunBase.Tree.Root.CallDeferred("add_child", spawnedBullet);
    }
     
    private static int GetCritDamage(int damage, int critChance)
    {
        if (critChance > 0)
        {
            Random random = new Random();
            int critRoll = random.Next(0, 100);
            if (critRoll < critChance)
            {
                // Debug.Print($"Critical hit! Damage: {damage * 2}");
                return damage * 2;
            }
        }
        return damage;
    }
    
    private static bool IsCrit(int damage, int baseDamage)
    {
        return damage > baseDamage;
    }
    public static BulletSpawnData CreateBulletSpawnData(GunBase gunBase)
    {
        BulletSpawnData spawnData = new BulletSpawnData
        {
            Spread = gunBase.Spread,
            Direction = gunBase.direction,
            BulletSpeed = gunBase.BulletSpeed,
            Damage = gunBase.Damage,
            GlobalPosition = gunBase.GlobalPosition,
            Tree = gunBase.GetTree(),
            PushForce = gunBase.pushForce,
            CritChance = gunBase.CritChance

        };
        return spawnData;
    }

}

