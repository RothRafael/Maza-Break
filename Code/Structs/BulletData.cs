using Godot;
using System;

public struct BulletSpawnData
{
    public float Spread;
    public Vector2 Direction;
    public float BulletSpeed;
    public int Damage;
    public float PushForce;
    public int CritChance;
    public Faction SenderFaction;
    public int SenderId;
    public Vector2 GlobalPosition;
    public SceneTree Tree;
    public BulletSpawnData(float spread, Vector2 direction, float bulletSpeed, int damage, float pushForce, int critChance, Faction senderFaction, Vector2 globalPosition, SceneTree tree, int senderId)
    {
        Spread = spread;
        Direction = direction;
        BulletSpeed = bulletSpeed;
        Damage = damage;
        PushForce = pushForce;
        CritChance = critChance;
        SenderFaction = senderFaction;
        GlobalPosition = globalPosition;
        Tree = tree;
        SenderId = senderId;
    }
}