using Godot;
using System;

public partial class RepositionState : IEnemyState
{
    public void Enter(Enemy enemy)
    {
        GD.Print("a");
    }

    public void Execute(Enemy enemy)
    {
        var playerPos = PlayerStatus.Instance.playerPosition;
        Vector2 direction = (playerPos - enemy.GlobalPosition).Normalized();
        enemy.Velocity = direction * enemy.Speed;

        if (enemy.GlobalPosition.DistanceTo(playerPos) < enemy.shootingRange)
        {
            // enemy.ChangeState(new ShootingState());
        }
    }

    public void Exit(Enemy enemy)
    {
        throw new NotImplementedException();
    }

}
