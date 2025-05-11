using Godot;
using System;

public partial class ShootingState : IEnemyState
{
    float timeSinceLastShot = 0f;
    float fireRate = 0;
    float timeInState = 0;
    public void Enter(Enemy enemy)
    {
        GD.Print("Shooting at player");
        fireRate = enemy.fireRate;
    }

    public void Execute(Enemy enemy)
    {
        Vector2 _playerPos = PlayerStatus.Instance.playerPosition;
        float distance = enemy.GlobalPosition.DistanceTo(_playerPos);

        if (distance > 250f)
        {
            enemy.CurrentState = new SearchState();
            enemy.CurrentState.Enter(enemy);
            return;
        }
        else if (distance < 250 && distance > 100f)
        {
            enemy._pathFindingComponent.SetTarget(_playerPos);

        }

        HandleShoot(enemy);


        timeInState += (float)enemy.GetProcessDeltaTime();

    }

    public void Exit(Enemy enemy)
    {
        throw new NotImplementedException();
    }

    private void HandleShoot(Enemy enemy)
    {
        if (timeSinceLastShot >= fireRate)
        {
            enemy._shootComponent.Shoot();
            timeSinceLastShot = 0f;
        }
        else
        {
            timeSinceLastShot += (float)enemy.GetProcessDeltaTime() + GD.Randf() * 0.1f;
        }
    }

}

