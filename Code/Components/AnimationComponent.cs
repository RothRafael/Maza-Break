using Godot;

public partial class AnimationComponent : Node
{
    [Export] private AnimationPlayer _animationPlayer2D;
    [Export] private PlayerController _playerController;
    [Export] private Sprite2D _sprite2D;
    [Export] private GpuParticles2D _footstepsParticles;
    private const string Idle = "idle", Run = "run";

    public override void _Ready()
    {
        _playerController = GetParent<PlayerController>();
        if (_animationPlayer2D == null)
        {
            GD.PrintErr("AnimationPlayer2D is not assigned in the inspector.");
        }
        if (_sprite2D == null)
        {
            GD.PrintErr("Sprite2D is not assigned in the inspector.");
        }
    }

    public override void _Process(double delta)
    {
        UpdateAnimation();
        UpdateParticles();
    }
    private void UpdateParticles()
    {
        if (_playerController.moveX == 0 && _playerController.moveY == 0)
        {
            // Stop particles when not moving
            if (_footstepsParticles.Emitting == true)
            {
                _footstepsParticles.Emitting = false;
            }
        }
        else
        {
            // Start particles when moving
            _footstepsParticles.Emitting = true;
        }
    }

    public void UpdateAnimation()
    {
        if (_animationPlayer2D == null || _playerController == null || _sprite2D == null)
        {
            GD.PrintErr("AnimationPlayer2D, PlayerController, or Sprite2D is null.");
            return;
        }

        if (_playerController.moveX != 0 || _playerController.moveY != 0)
        {
            _animationPlayer2D.Play(Run);

            // Flip the sprite based on movement direction
            if (_playerController.moveX < 0)
            {
                _sprite2D.FlipH = true;
            }
            else if (_playerController.moveX > 0)
            {
                _sprite2D.FlipH = false;
            }
        }
        else
        {
            _animationPlayer2D.Play(Idle);
        }
    }
}
