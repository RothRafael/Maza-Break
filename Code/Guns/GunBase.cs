using System.Collections.Generic;
using System.Diagnostics;
using Godot;

public abstract partial class GunBase : Node2D, IGun
{
    [Export] public bool isAutomatic  {get; set; } = false;
    [Export] public float fireRate = 0.5f;
    [Export] public int Damage = 1;
    [Export] public int EnergyCost = 1;
    [Export] public int Spread = 0;
    [Export] public int BulletSpeed = 700;
    [Export] public int CritChance = 0;
    [Export] public float pushForce = 0f;
    [Export] public bool isMelee = false;
    public Vector2 direction;
    public float timeSinceLastShot = 0f;
    public Node2D muzzle;
    public ProjectileFactory projectileFactory;
    public bool canShoot = true;
    public override void _Ready()
    {
        muzzle = GetNode<Node2D>("Muzzle");
        projectileFactory = GetNode<ProjectileFactory>("ProjectileFactory");
    }

    public virtual bool Shoot(Vector2 direction) {
        if(PlayerStatus.Instance.canShoot(EnergyCost) && CanShoot())
        {
            this.direction = direction.Normalized();
            return true;
        }
        return false;
    }

    public override void _Process(double delta)
    {
        timeSinceLastShot += (float)delta;
    }

    protected bool CanShoot()
    {
        return timeSinceLastShot >= fireRate;
    }   
}
