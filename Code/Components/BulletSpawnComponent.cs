using Godot;
using System;

public partial class BulletSpawnComponent : Node2D
{
    [Export] float _shootSpeed = 1f;
    [Export] private Node2D _gunScene;
    private GunBase _gun;
    private float currentTime = 0f;
    public override void _Ready()
    {
        _gun = _gunScene as GunBase;
        if (_gun != null)
        {
            _gunScene.Position = new Vector2(170, -100);
        }
    }

    public override void _Process(double delta)
    {
        currentTime -= (float)delta;
        if (currentTime <= 0)
        {
            currentTime = _shootSpeed;
            shootGun(Vector2.Right);
        }
    }
    public void shootGun(Vector2 direction)
    {
        if (_gun.Shoot(direction))
        {
            GD.Print("Shooting");
            _gun.Shoot(direction);
        }
    }
}
