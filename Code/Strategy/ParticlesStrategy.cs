using Godot;
using System;

public partial class ParticlesStrategy : IStrategy
{
    
    public void ApplyUpgrade(ProjectileBase projectile)
    {
        throw new NotImplementedException();
    }

}


public partial class SpeedStrategy : IStrategy
{
    [Export] public float speed = 1000f;
    public void ApplyUpgrade(ProjectileBase projectile)
    {
        projectile.SetSpeed(speed);
    }

}
