using Godot;
using System;

public partial class AnimationComponent : Node
{
    [Export] private AnimatedSprite2D _animatedSprite2D;
    [Export] private PlayerController _playerController;
    [Export] private Node2D hands;
    private const string Up = "Up", Down = "Down", Left = "Left", Right = "Right", Idle = "Idle";

    public override void _Ready()
    {
        _playerController = GetParent<PlayerController>();
        if (_animatedSprite2D == null)
        {
            GD.PrintErr("AnimatedSprite2D is not assigned in the inspector.");
        }
    }

    public override void _Process(double delta)
    {
        UpdateAnimation();
    }

    public void UpdateAnimation()
    {
        if (_animatedSprite2D == null || _playerController == null)
        {
            GD.PrintErr("AnimatedSprite2D or PlayerController is null.");
            return;
        }

        if (_playerController.moveX > 0)
        {
            _animatedSprite2D.Play(Right);
            hands.ZIndex = 1;

        }
        else if (_playerController.moveX < 0)
        {
            _animatedSprite2D.Play(Left);
            hands.ZIndex = 1;
        }
        else if (_playerController.moveY > 0)
        {
            _animatedSprite2D.Play(Down);
            hands.ZIndex = 1;
        }
        else if (_playerController.moveY < 0)
        {
            _animatedSprite2D.Play(Up);
            hands.ZIndex = -1;
        } else {
            _animatedSprite2D.Play(Idle);
            hands.ZIndex = 0;
        }
    }
}
