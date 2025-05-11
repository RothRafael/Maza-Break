using Godot;
using System.Diagnostics;

public partial class Enemy : CharacterBody2D, IDamagable, IPushable
{
    [Export] public Faction faction { get; set; } = Faction.Enemy;
    [Export] public int Health = 100;
    [Export] public float Speed = 100f;
    [Export] public float fireRate = 0.2f;
    [Export] public bool isPushable { get; set; } = true;
    [Export] public float shootingRange = 10f;
    public IEnemyState CurrentState { get; set; }
    public bool isAlive = true;
    [Export] public PackedScene playerTexture;

    // Components
    [Export] public EnemyAnimationComponent _enemyAnimationComponent;
    [Export] public PathFindingComponent _pathFindingComponent;
    [Export] public ShootComponentScript _shootComponent;

    private Sprite2D sprite;
    public bool IsFlipped { get; set; } = false;

    public override void _Ready()
    {
        InstantiateComponents();
    }
    public override void _PhysicsProcess(double delta)
    {
        if (isAlive)
        {
            // MoveAndSlide();
            CurrentState?.Execute(this);
        }
    }

    public void Push(Vector2 direction, float force)
    {
        if (isPushable && isAlive)
        {
            // Apply a force in the direction of the push
            Vector2 pushDirection = direction.Normalized();
            Velocity -= pushDirection * force;
            MoveAndSlide();
        }
    }


    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Die();
            return;
        }
        _enemyAnimationComponent.PlayAnimation("Hit");
    }
    public void TakeDamage(int damage, bool isCritical)
    {
        TakeDamage(damage);
    }
    private void Die()
    {
        _enemyAnimationComponent.PlayAnimation("Die");
        isAlive = false;
    }
    private void OnEnemyTimerTimeout()
    {
        QueueFree();
    }
    private void InstantiateComponents()
    {
        sprite = playerTexture.Instantiate() as Sprite2D;
        AddChild(sprite);
        sprite.Position = new Vector2(0, 0);
        sprite.Scale = new Vector2(1, 1);

        _enemyAnimationComponent.Initialize(sprite);

        CurrentState = new SearchState();
        CurrentState.Enter(this);
    }
    public void SetFlip(bool flip)
    {
        IsFlipped = flip;
        sprite.FlipH = flip;
    }
    public void Shoot()
    {
        // Implementar a lógica de tiro aqui
        _shootComponent.Shoot();
    }
}