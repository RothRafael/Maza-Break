using Godot;
using System;

public partial class AnimComponent : Node
{
    [Export]
    private NodePath animationPlayerPath;

    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>(animationPlayerPath);
    }

    public void PlayAnimation(string animationName)
    {
        if (_animationPlayer != null && _animationPlayer.HasAnimation(animationName))
        {
            _animationPlayer.Seek(0, true); // Reset animation to the start
            _animationPlayer.Play(animationName);
        }
        else
        {
            GD.PrintErr($"Animation '{animationName}' not found or AnimationPlayer is null.");
        }
    }
}
