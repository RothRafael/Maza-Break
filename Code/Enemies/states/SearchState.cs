using Godot;
using System;

public partial class SearchState : IEnemyState
{
    float originalSpeed;
    float newSpeed;


    public void Enter(Enemy enemy)
    {
        originalSpeed = enemy.Speed;
        newSpeed = originalSpeed * 2f;
        enemy.Speed = newSpeed;
    }

    public void Execute(Enemy enemy)
    {
        Vector2 _playerPos = PlayerStatus.Instance.playerPosition;
        enemy._pathFindingComponent.SetTarget(_playerPos);

        if (enemy.GlobalPosition.DistanceTo(_playerPos) < 100f)
        {
            Exit(enemy);
            enemy.CurrentState = new ShootingState();
            enemy.CurrentState.Enter(enemy);
        }
    }

    public void Exit(Enemy enemy)
    {
        enemy.Speed = originalSpeed;
        GD.Print(enemy.Speed);
        GD.Print("Exiting Search State");
    }

}
