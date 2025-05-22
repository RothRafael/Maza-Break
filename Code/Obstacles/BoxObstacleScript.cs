using Godot;
using System;

public partial class BoxObstacleScript : ObstacleBaseScript
{
    [Export] PackedScene boxDestroyedEffectScene;
    [Export] private EffectComponentScript effectComponent;
    public override void DoAction()
    {
        base.DoAction();
        effectComponent.spawnEffect(
            boxDestroyedEffectScene,
            GlobalPosition
        );
        QueueFree();
    }

}
