using Godot;

public interface IDamagable {
    void TakeDamage(int damage);
    void TakeDamage(int damage, bool isCritical);

}