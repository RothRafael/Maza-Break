using Godot;
using System;
using System.Collections.Generic;

public partial class GunRaquete : MeleeBaseSwing
{

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
        if (_isSwinging || !CanShoot() || timeSinceLastShot < fireRate)
            return false;

        base.Shoot(direction);
        PlayAnim();
        Parry(direction);
        timeSinceLastShot = 0f;
        return true;
    }

    private void PlayAnim()
    {
        _animationPlayer.Play("Swing");
    }

    public void Parry(Vector2 direction)
    {
        if (_projectilesToParry.Count == 0) return;

        foreach (var projectile in _projectilesToParry)
        {
            // Check if the projectile is within the swing area
            projectile.shooterFaction = Faction.Player;
            projectile.Launch(direction, 1f);
        }
        // PlayerStatus.Instance.SlowTime(0.5f, 0.2f);
        _projectilesToParry.Clear();
    }

    private void _on_area_enter(Area2D area)
    {
        if (area is ProjectileBase projectile)
        {
            if (projectile.shooterFaction == Faction.Enemy)
            {
                _projectilesToParry.Add(projectile);
            }
        }
    }
    private void _on_area_exited(Area2D area)
    {
        if (area is ProjectileBase projectile)
        {
            _projectilesToParry.Remove(projectile);
        }
    }
    
}
