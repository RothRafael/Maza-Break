using Godot;
using System;

public partial class ObstacleBaseScript : StaticBody2D, IObstacle
{
    public virtual void DoAction()
    {
        GD.Print("Obstacle action performed");
    }
}


