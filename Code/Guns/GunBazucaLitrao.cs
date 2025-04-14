using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public partial class GunBazucaLitrao : GunBase
{
    [Export] private AudioStream ShotSounds;
    [Export] private PackedScene _bulletScene;
    List<IStrategy> upgrades = new List<IStrategy>();
    public override bool Shoot(Vector2 direction)
    {
        if (!CanShoot()) return false;
        if (!base.Shoot(direction)) return false;

        
        timeSinceLastShot = 0f;
        return true;
    }
}
