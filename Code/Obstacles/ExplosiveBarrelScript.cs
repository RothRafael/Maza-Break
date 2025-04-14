using Godot;


public partial class ExplosiveBarrelScript : ObstacleBaseScript
{
    [Export] private int explosionDamage = 50;
    [Export] private PackedScene _explosionEffectScene;
    public override void DoAction()
    {
        GD.Print("Explosive barrel action performed");
        CreateExplosion();
        QueueFree(); // Remove the barrel after the action
    }
     public void CreateExplosion()
    {
        Node2D explosion = (Node2D)_explosionEffectScene.Instantiate();

        if(explosion.HasMethod("SetExplosionDamage"))
        {
            explosion.Call("SetExplosionDamage", explosionDamage);
        }
        GetTree().Root.AddChild(explosion);
        explosion.GlobalPosition = GlobalPosition;
        explosion.GlobalRotation = Rotation;
    }
}