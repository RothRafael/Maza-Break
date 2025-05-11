using Godot;
using System;

public partial class PathFindingComponent : Node
{
    [Export] public NavigationAgent2D _nav;
    private Enemy _characterBody2D;

    public override void _Ready()
    {
        _characterBody2D = GetParent().GetParent<Enemy>();
        if (_nav == null) GD.PrintErr("NavigationAgent2D not found");
        if (_characterBody2D == null) GD.PrintErr("CharacterBody2D not found");
    }

    // Faz parte do sistema de pathfinding para o target
    // O target é o ponto que o inimigo vai seguir
    public void SetTarget(Vector2 target)
    {
        _nav.TargetPosition = target;
    }

    public Vector2 GetNextPathPoint()
    {
        return _nav.GetNextPathPosition();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        if (_nav.IsNavigationFinished() || !_characterBody2D.isAlive)
        {
            _characterBody2D.Velocity = Vector2.Zero;
            return;
        }

        bool shouldFlip = _nav.TargetPosition.X < _characterBody2D.GlobalPosition.X;

if (shouldFlip != _characterBody2D.IsFlipped)
{
    _characterBody2D.SetFlip(shouldFlip);
}

        
        Vector2 nextPathPoint = GetNextPathPoint();
        Vector2 direction = (nextPathPoint - _characterBody2D.GlobalPosition).Normalized();
        _characterBody2D.Velocity = direction * _characterBody2D.Speed ; // Adjust speed as needed
        _characterBody2D.MoveAndSlide();
        
        
    }
    public bool HasReachedTarget()
    {
        return _nav.IsNavigationFinished();
    }

}
