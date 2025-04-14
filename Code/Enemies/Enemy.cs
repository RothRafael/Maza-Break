using Godot;
using System.Diagnostics;

public partial class Enemy : CharacterBody2D, IDamagable, IPushable
{
    [Export] public int Health;
    [Export] public int Coins;
    [Export] public bool isPushable {get; set; }

    private AnimComponent _animComponent;
    private CoinComponent _coinComponent;
    private PathFindingComponent _pathFindComponent;
    private HitNumbersComponent _hitNumbersComponent;

    private Vector2 _lastPlayerPos = Vector2.Zero;
    [Export] private float _speed = 100f; // Adjust this value as needed
    public override void _Ready()
    {
        _animComponent = GetNode<AnimComponent>("AnimComponent");
        _coinComponent = GetNode<CoinComponent>("CoinComponent");
        _pathFindComponent = GetNode<PathFindingComponent>("PathFindingComponent"); 
        _hitNumbersComponent = GetNode<HitNumbersComponent>("HitNumbersComponent");
        ThrowErrors();
    }
    public override void _Process(double delta)
{
    var playerPos = PlayerStatus.Instance.playerPosition;
    
    // Only update target if the player moved significantly
    if (_lastPlayerPos.DistanceTo(playerPos) > 5f)
    {
        _pathFindComponent.SetTarget(playerPos);
        _lastPlayerPos = playerPos;
    }

    if (!_pathFindComponent.HasReachedTarget())
    {
        var direction = (_pathFindComponent.GetNextPathPoint() - Position).Normalized();
        Velocity = direction * _speed;
        MoveAndSlide(); // Replace SPEED with your desired speed value
    }
    else
    {
        // Maybe idle or perform some other behavior
    }
}
    public void TakeDamage(int damage, bool isCritical)
    {
        Debug.Print($"Enemy took {damage} damage");
        Health -= damage;
        _animComponent.PlayAnimation("OnReciveDamage");
        _hitNumbersComponent.CreateHitNumber(GlobalPosition, damage, isCritical);
        

        if (Health <= 0)
        {
            PlayerStatus.Instance.SlowTime(0.7f, 0.1f);
            Die();
        }
    }
    public void Push(Vector2 direction, float force)
    {
        if (isPushable)
        {
            Velocity -= direction.Normalized() * force;
            MoveAndSlide();
            GD.Print($"Enemy pushed with force: {force}");
        }
    }
    public void Die()
    {
        _coinComponent.InstantiateCoin(Coins, Position);
        QueueFree();
    }
    public override string ToString()
    {
        return $"Enemy: {Name}, Health: {Health}";
    }




    private void ThrowErrors()
    {
        if (_animComponent == null)
        {
            GD.PrintErr("AnimComponent not found");
        }
        if (_coinComponent == null)
        {
            GD.PrintErr("CoinComponent not found");
        }
        if (_pathFindComponent == null)
        {
            GD.PrintErr("PathFindingComponent not found");
        }
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}