using System.Diagnostics;
using Godot;

public partial class ProjectileBase : Area2D
{
    [Export] public int Damage = 0;
    [Export] public float lifeTime = 1f;
    [Export] public float pushForce = 0f;
    [Export] public bool isCrit = false;
    [Export] public Faction shooterFaction;
    // helpers
    private float TimeWithCollisionDisabled = 0f;
    public bool isCollisionEnabled = true;


    protected Vector2 _velocity;
    public float speed = 0;
    public virtual void Launch(Vector2 direction, float speed)
    {
        _velocity = direction.Normalized();
        SetSpeed(speed);
    }

    public virtual void _process(float delta)
    {
        if (!isCollisionEnabled) TimeWithCollisionDisabled += delta;

        if (TimeWithCollisionDisabled >= 0.1 && !isCollisionEnabled) EnableCollision();

        Position += _velocity * speed * delta;

        lifeTime -= (float)delta;
        if (lifeTime <= 0)
        {
            QueueFree();
        }
    }

    public virtual void OnHit(Node2D target)
    {
        if (target == null) return;

        var direction = (PlayerStatus.Instance.playerPosition - target.GlobalPosition).Normalized();


        if (target is IPushable pushable)
        {
            pushable.Push(direction, pushForce);
        }

        if (target is IObstacle obstacle)
        {
            obstacle.DoAction();
        }

        if (target is IDamagable damagable)
        {
            damagable.TakeDamage(Damage, isCrit);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed += speed;
    }
    public void DisableCollision()
    {
        var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        if (collisionShape == null) return;
        isCollisionEnabled = false;
        collisionShape.Disabled = true;
        // Debug.Print("Collision disabled");
    }
    public void EnableCollision()
    {
        var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        if (collisionShape == null) return;
        isCollisionEnabled = true;
        TimeWithCollisionDisabled = 0f;
        collisionShape.Disabled = false;
        // Debug.Print("Collision enabled");
    }
}







