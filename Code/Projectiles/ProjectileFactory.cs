using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class ProjectileFactory : Node
{
    // PackedScene _baseProjectileScene = ResourceLoader.Load<PackedScene>("res://Prefabs/Projectiles/Bullet.tscn");

    // public Area2D CreateProjectile(Vector2 position, Vector2 direction, float rotation, List<Type> decorators, float playerSpeed, int damage) {
    //     // Create the base projectile instance
    //     var baseProjectile = _baseProjectileScene.Instantiate<Area2D>();
    //     baseProjectile.GlobalPosition = position;
    //     baseProjectile.Set("Damage", damage); // Set the damage property
    //     baseProjectile.GlobalRotation = rotation; // Set the rotation of the projectile

    //     // Apply decorators
    //     IProjectile projectile = (IProjectile)baseProjectile;
    //     foreach (var decoratorType in decorators)
    //     {
    //         var decorator = (ProjectileDecorator)Activator.CreateInstance(decoratorType);
    //         decorator.SetProjectile(projectile);
    //         projectile = decorator;
    //     }

    //     // Launch the projectile
    //     projectile.Launch(direction, playerSpeed); // Example speed
    //     return baseProjectile;
    // }

}
