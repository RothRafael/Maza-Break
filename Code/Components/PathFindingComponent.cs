using Godot;
using System;

public partial class PathFindingComponent : Node
{
    [Export] public NavigationAgent2D _nav;

    public override void _Ready()
    {
        if (_nav == null) GD.PrintErr("NavigationAgent2D not found");
    }

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

        if (_nav.IsNavigationFinished())
        {
            GD.Print("Navigation finished");
            return;
        }
    }
    public bool HasReachedTarget()
    {
        return _nav.IsNavigationFinished();
    }

}
