using Godot;

public partial class ExplosionStrategy : ProjectileBase
{
    [Export] private PackedScene explosionScene;
    public override void _Ready()
    {
        base._Ready();
    }
    private void _on_body_entered(Node2D body)
    {
        base.OnHit(body);
    }
    public override void _process(float delta)
    {
        base._process(delta);

        if (lifeTime <= 0)
        {
            CreateExplosion(GlobalPosition);
        }
    }
    public void CreateExplosion(Vector2 position)
    {
        Node2D explosion = (Node2D)explosionScene.Instantiate();

        if(explosion.HasMethod("SetExplosionDamage"))
        {
            explosion.Call("SetExplosionDamage", Damage * 2);
        }

        GetTree().Root.AddChild(explosion);
        explosion.GlobalPosition = position;
        explosion.GlobalRotation = Rotation;
        QueueFree();
    }

}
