using Godot;
using System;

public partial class EnemyAnimationComponent : Node
{
    [Export] private AnimationPlayer _textureAnimationPlayer;
    [Export] private AnimationPlayer _hitAnimationPlayer;
    private bool isDying = false;


    public void Initialize(Node2D enemyTexture)
    {
        _textureAnimationPlayer = enemyTexture.GetNode<AnimationPlayer>("AnimationPlayer");
    }   

    public void PlayAnimation(string animationName)
    {
        if (isDying) return;

        if (animationName == "Hit")
        {
            if (_hitAnimationPlayer != null)
            {
                _hitAnimationPlayer.Stop();
                _hitAnimationPlayer.Play(animationName);
            }
            else
            {
                GD.PrintErr("Hit AnimationPlayer is not assigned.");
            }
            return;
        }

        if (_textureAnimationPlayer != null)
        {
            _textureAnimationPlayer.Play(animationName);
            if (animationName == "Die")
            {
                _hitAnimationPlayer.Play("Die");
                isDying = true;
            }
        }
        else
        {
            GD.PrintErr("Texture AnimationPlayer is not initialized.");
        }
    }

}
