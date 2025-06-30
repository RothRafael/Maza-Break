using Godot;

public partial class Enemy : CharacterBody2D, IDamagable, IPushable
{
    [Export] public Faction faction { get; set; } = Faction.Enemy;
    [Export] public int Health = 100;
    [Export] public float Speed = 100f;
    [Export] public float fireRate = 0.2f;
    [Export] public int coinDropAmmount = 1;
    [Export] public bool isPushable { get; set; } = true;
    [Export] public float shootingRange = 10f;
    public IEnemyState CurrentState { get; set; }
    public bool isAlive = true;
    // Components
    [Export] public EnemyAnimationComponent _enemyAnimationComponent;
    [Export] public PathFindingComponent _pathFindingComponent;
    [Export] public ShootComponentScript _shootComponent;
    [Export] public CoinComponent _coinComponent;
    [Export] public HitNumbersComponent _hitNumbersComponent;

    [Export] private Sprite2D sprite;
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

    public virtual void Push(Vector2 direction, float force)
    {
        if (isPushable && isAlive)
        {
            // Apply a force in the direction of the push
            Vector2 pushDirection = direction.Normalized();
            Velocity -= pushDirection * force;
            MoveAndSlide();
        }
    }

    public virtual void TakeDamage(int damage)
    {
        
    }
    public virtual void TakeDamage(int damage, bool isCritical)
    {
        if(!isAlive) return;
            
        Health -= damage;

        if (Health <= 0)
        {
            Die();
            return;
        }
        // _hitNumbersComponent.CreateHitNumber(GlobalPosition, damage, isCritical);
        _enemyAnimationComponent.PlayAnimation("Hit");
    }
    public virtual void Die()
    {
        _enemyAnimationComponent.PlayAnimation("Die");
        _coinComponent.SpawnCoins(coinDropAmmount, GlobalPosition);
        isAlive = false;
        EnemiesManager.instance.EnemieDied(this);
    }
    public virtual void OnEnemyTimerTimeout()
    {
        QueueFree();
    }
    public virtual void InstantiateComponents()
    {
        CurrentState = new SearchState();
        CurrentState.Enter(this);
    }
    public virtual void setTexture(Sprite2D texture)
    {
        sprite = texture;
        AddChild(sprite);
        sprite.Position = new Vector2(0, 0);
        sprite.Scale = new Vector2(1, 1);

        _enemyAnimationComponent.Initialize(sprite);

    }
    public virtual void SetFlip(bool flip)
    {
        IsFlipped = flip;
        sprite.FlipH = flip;
    }
    public virtual void Shoot()
    {
        // Implementar a lógica de tiro aqui
        _shootComponent.Shoot();
    }
    public virtual void InitializeEnemy(EnemyData enemyData)
    {
        var enemyInitializer = new EnemyInitializer();
        enemyInitializer.InitializeSelf(this, enemyData);
    }

}
