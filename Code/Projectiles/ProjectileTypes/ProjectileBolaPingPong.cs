using Godot;
using System;

public partial class ProjectileBolaPingPong : ProjectileBase
{
    [Export] PackedScene impactParticles;
    [Export] private float speedAfterHit = 10f;
    [Export] private float speedLimit = 500f;

    private void _on_body_entered(Node2D body)
    {
        if (body.Name == "Player") return;

        if (speed > speedLimit)
        {
            speedAfterHit = 0f;
        }

        base.OnHit(body);

        base.Launch(-1 * _velocity, speedAfterHit);
    }
    public override void Launch(Vector2 direction, float speed)
    {
        DisableCollision();
        float newspeed = 0;

        if (this.speed >= speedLimit)
        {
            base.Launch(direction, 0);
        }
        else
        {
            base.Launch(direction, speed);
        }
    }
}
