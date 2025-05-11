using Godot;

public interface IDamagable {

    public Faction faction { get; set; }
    void TakeDamage(int damage);
    void TakeDamage(int damage, bool isCritical);

}
    public enum Faction
    {
        Player,
        Enemy,
    }