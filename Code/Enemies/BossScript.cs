using Godot;
using System;

public partial class BossScript : Enemy
{
    private int phase = 1;

    public override void _Ready()
    {
        base._Ready();
        CurrentState = new BossEnterRoomState();
        CurrentState.Enter(this);
    }

    public void EnterPhase(int newPhase)
    {
        phase = newPhase;
        switch (phase)
        {
            case 2:
                // CurrentState = new BossPhaseTwoState();
                break;
            case 3:
                // CurrentState = new EnragedState();
                break;
        }
        CurrentState.Enter(this);
    }

    public override void Die()
    {
        base.Die();
        // Implement boss death logic here, such as spawning loot or triggering an event

    }
    public void OnAnimationFinished()
    {
        GD.Print("Boss defeated! Triggering UI update...");
        PlayerUIComponent.Instance.ShowBossDefeatedMessage();
    }

}