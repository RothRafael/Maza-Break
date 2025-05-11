using Godot;
using System;

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
        var _playerPos = PlayerStatus.Instance.playerPosition;
        var _enemyPos = GetParent().GetParent<Enemy>().GlobalPosition;

        _direction = (_playerPos - _enemyPos).Normalized();
        Rotation = _direction.Angle();
    }
    public void Shoot()
    {
        if (_gun != null)
        {
            _gun.Shoot(_direction);
        }
        else
        {
            GD.PrintErr("Gun not found");
        }
    }
}
