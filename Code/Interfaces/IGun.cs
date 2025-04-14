using Godot; 

public interface IGun {
    public bool Shoot(Vector2 direction);
    public bool isAutomatic { get; set; }
}