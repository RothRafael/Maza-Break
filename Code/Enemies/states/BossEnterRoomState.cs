using Godot;
using System;

public partial class BossEnterRoomState : IEnemyState
{
    float timeInState = 0f;
    public void Enter(Enemy enemy)
    {
        throw new NotImplementedException();
    }

    public void Execute(Enemy enemy)
    {
        timeInState += (float)enemy.GetProcessDeltaTime();

        // Check if the enemy has been in this state for more than 2 seconds
        if (timeInState > 2f)
        {
            // Transition to the next state, e.g., ShootingState
            enemy.CurrentState = new BossShootState();
            enemy.CurrentState.Enter(enemy);
        }
    }
    public void Exit(Enemy enemy)
    {
        throw new NotImplementedException();
    }
}
