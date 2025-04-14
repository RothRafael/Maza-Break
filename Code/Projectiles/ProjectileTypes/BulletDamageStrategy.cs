using Godot;
using System;

public partial class BulletDamageStrategy : Node
{   
    int Damage { get; set; }
    public BulletDamageStrategy(int damage)
    {
        Damage = damage;
    }
}
