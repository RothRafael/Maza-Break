using Godot;
using System;

public partial class EffectComponentScript : Node
{
    public void spawnEffect(PackedScene effectScene, Vector2 position)
    {
        if (effectScene == null)
        {
            GD.PrintErr("Effect scene is null.");
            return;
        }

        Node effectInstance = effectScene.Instantiate();
        if (effectInstance is Node2D node2DEffect)
        {
            node2DEffect.Position = position;
            GetTree().Root.AddChild(node2DEffect);
        }
    }
}
