using Godot;
using System;
using System.Collections.Generic;

public partial class GunRaquete : MeleeBaseSwing
{
    [Export] private float _swingSpeed = 0.5f;
    [Export] private float _swingAngle = 45f; // Angle in degrees
    [Export] private float _swingDistance = 100f; // Distance in pixels

    private AnimationPlayer _animationPlayer;
    private bool _isSwinging = false;

    private List<ProjectileBase> _projectilesToParry = new List<ProjectileBase>();
    public override void _Ready()
    {
        base._Ready();
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override bool Shoot(Vector2 direction)
    {
        if (_isSwinging)
            return false;

        PlayAnim();
        SpawnProjectile();
        return true;
    }

    private void PlayAnim()
    {
        _animationPlayer.Play("Swing");
    }

    public void Parry() {
        foreach (var projectile in _projectilesToParry)
        {
            // Check if the projectile is within the swing area
            projectile.Launch(projectile._velocity * -1, 1f);
        }
        _projectilesToParry.Clear();
    }

    private void _on_area_2d_area_entered(Area2D area)
    {
        if (area is ProjectileBase projectileBase) {
            // Check if the projectile is from an enemy
            if (projectileBase.shooterFaction == Faction.Enemy)
            {
                projectileBase.shooterFaction = Faction.Player;
                _projectilesToParry.Add(projectileBase);
            }
        }
    }
}
