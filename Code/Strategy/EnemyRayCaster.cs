// EnemyRaycaster.cs
using Godot;
using Godot.Collections;

public class EnemyRaycaster
{
    private Node2D _owner;
    private PhysicsDirectSpaceState2D _space;

    public EnemyRaycaster(Node2D owner)
    {
        _owner = owner;
        _space = owner.GetWorld2D().DirectSpaceState;
    }

    public Dictionary CastRay(Vector2 direction, float distance, uint collisionMask = 1)
    {
        Vector2 origin = _owner.GlobalPosition;
        Vector2 target = origin + direction.Normalized() * distance;

        var query = new PhysicsRayQueryParameters2D
        {
            From = origin,
            To = target,
            CollisionMask = collisionMask
        };

        return _space.IntersectRay(query);
    }
}
