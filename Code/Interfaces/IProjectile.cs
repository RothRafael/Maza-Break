using Godot;
public interface IProjectile
{
    void Launch(Vector2 direction, float speed);
    void OnHit(CharacterBody2D target);
    void SetSpeed(float speed);
    int Damage { get; set; }
}
public interface IStrategy {
    public void ApplyUpgrade(ProjectileBase projectile);
}