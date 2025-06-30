using Godot;
using System;
using System.Security.Cryptography;

public partial class ShootComponentScript : Node2D
{
    [Export] public GunBase _gun;
    Vector2 _direction;

    public override void _Ready()
    {
        _gun = GetNode<GunBase>("Gun");
    }
    public override void _Process(double delta)
    {

    }
    public void Shoot()
    {
        var _playerPos = PlayerStatus.Instance.playerPosition;
        var _enemyPos = GetParent().GetParent<Enemy>().GlobalPosition;

        _direction = (_playerPos - _enemyPos).Normalized();
        Rotation = _direction.Angle();
        if (_gun != null)
        {
            _gun.Shoot(_direction);
        }
        else
        {
            GD.PrintErr("Gun not found");
        }
    }
    public void Shoot(float angle)
    {
        var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).Normalized();
        if (_gun != null)
        {
            _gun.Shoot(direction);
        }
        else
        {
            GD.PrintErr("Gun not found");
        }
    }
}