using Godot;
using System;

public partial class BossShootState : IEnemyState
{
    float shootAngle = 0f;
    float timer = 0f;
    float tempoNoEstado = 0f;
    float tempoMaximoNoEstado;

    float tempoEspiral = 0.01f;
    int burstShots = 10;
    float burstInterval = 0.015f;
    float burstTimer = 0f;
    int burstShotsLeft = 0;
    bool isBursting = false;

    Random rand = new();

    public void Enter(Enemy enemy)
    {
        shootAngle = 0f;
        timer = 0f;
        tempoNoEstado = 0f;
        tempoMaximoNoEstado = (float)(rand.NextDouble() * 3.0 + 3.0); // entre 3 e 6 segundos
        burstShotsLeft = 0;
        isBursting = false;
    }

    public void Execute(Enemy enemy)
    {
        float delta = (float)enemy.GetProcessDeltaTime();
        tempoNoEstado += delta;

        if (tempoNoEstado >= tempoMaximoNoEstado)
        {
            enemy.CurrentState = new BossChasePlayerState();
            enemy.CurrentState.Enter(enemy);
            return;
        }

        if (isBursting)
        {
            burstTimer += delta;
            if (burstShotsLeft > 0 && burstTimer >= burstInterval)
            {
                burstTimer = 0f;
                TiroNoPlayer(enemy);
                burstShotsLeft--;
            }
            else if (burstShotsLeft <= 0)
            {
                isBursting = false;
            }
            return;
        }

        timer += delta;
        if (timer >= tempoEspiral)
        {
            timer = 0f;

            int attackType = rand.Next(0, 2); // 0: espiral, 1: burst
            if (attackType == 0)
                TiroEspiral(enemy);
            else
                StartBurst();
        }
    }

    public void Exit(Enemy enemy)
    {
        enemy.Velocity = Vector2.Zero;
    }

    public void TiroEspiral(Enemy enemy)
    {
        enemy._pathFindingComponent.SetTarget(PlayerStatus.Instance.playerPosition);
        enemy._shootComponent.Shoot(shootAngle);
        shootAngle += 15f;
    }

    public void StartBurst()
    {
        isBursting = true;
        burstShotsLeft = burstShots;
        burstTimer = 0f;
    }

    public void TiroNoPlayer(Enemy enemy)
    {
        Vector2 playerPos = PlayerStatus.Instance.playerPosition;
        Vector2 dir = (playerPos - enemy.GlobalPosition).Normalized();
        float angle = Mathf.Atan2(dir.Y, dir.X);
        enemy._shootComponent.Shoot(angle);
    }
}


public partial class BossChasePlayerState : IEnemyState
{
    float chaseSpeedMultiplier = 2f;
    float stopDistance = 50f;

    public void Enter(Enemy enemy) { }

    public void Execute(Enemy enemy)
    {
        Vector2 playerPos = PlayerStatus.Instance.playerPosition;
        Vector2 dir = (playerPos - enemy.GlobalPosition).Normalized();
        float dist = enemy.GlobalPosition.DistanceTo(playerPos);

        enemy.Velocity = dir * enemy.Speed * chaseSpeedMultiplier;
        enemy.MoveAndSlide();

        if (dist <= stopDistance)
        {
            enemy.CurrentState = new BossShootState();
            enemy.CurrentState.Enter(enemy);
        }
    }

    public void Exit(Enemy enemy)
    {
        enemy.Velocity = Vector2.Zero;
    }
}
