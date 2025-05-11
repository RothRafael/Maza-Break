using Godot;
using System;

public partial class GunRaquete : MeleeBaseSwing
{
    [Export] private float _swingSpeed = 0.5f;
    [Export] private float _swingAngle = 45f; // Angle in degrees
    [Export] private float _swingDistance = 100f; // Distance in pixels

    private AnimationPlayer _animationPlayer;
    private bool _isSwinging = false;

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
}
