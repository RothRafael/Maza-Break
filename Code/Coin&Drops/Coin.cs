using Godot;
using System;
using System.Collections.Generic;

public partial class Coin : RigidBody2D, ICollectable
{
    [Export] public int Amount;
    [Export] public int CoinType;
    [Export] public float Speed = 100f;
    [Export] public float _idleTime = 0.5f;
    [Export] public float _gravityTime = 0.5f;
    private List<Sprite2D> sprite2Ds = new List<Sprite2D>();
    private SoundComponent _soundComponent;
    private bool Collected = false;

    public override void _Ready()
    {
        GravityScale = 0; // Desativa gravidade para top-down
        LinearVelocity = SpawnRandomDirection() * Speed;
        // Adiciona sprites à lista
        sprite2Ds.Add(GetNode<Sprite2D>("Gold"));
        sprite2Ds.Add(GetNode<Sprite2D>("Silver"));
        _soundComponent = GetNode<SoundComponent>("SoundComponent");
        
        Random random = new Random();
        CoinType = random.Next(0, 2);
        ActivateSprite(CoinType);

        // random idle time
        _idleTime = (float)GD.RandRange(0.5f, 1.2f);
    }

    public override void _Process(double delta)
    {
        if (_idleTime > 0)
        {
            _idleTime -= (float)delta;
            return;
        }
        if(Collected) {
            Speed = 0;
            if(!_soundComponent.isPlaying()) {
                QueueFree();
            }
        }

        Vector2 direction = (PlayerStatus.Instance.playerPosition - GlobalPosition).Normalized();
        LinearVelocity = direction * Speed;
        Speed += 10;
    }

    private Vector2 SpawnRandomDirection()
    {
        Random random = new Random();
        return new Vector2((float)random.NextDouble(),(float)(random.NextDouble() * 2 - 1)).Normalized()  * 0.2f;
    }

    public void Collect()
    {
        _soundComponent.PlaySoundIncreasingPitch();
        Collected = true;
    }
    private void ActivateSprite(int index)
    {
        for (int i = 0; i < sprite2Ds.Count; i++)
        {
            sprite2Ds[i].Visible = i == index;
        }
    }
}
