using Godot;
public interface IPushable
{
    void Push(Vector2 direction, float force);
        bool isPushable {get; set; }
}