using Godot;
using System;

public interface IEnemyState
{
    // Define the methods that all enemy states must implement
    void Enter(Enemy enemy);
    void Exit(Enemy enemy);
    void Execute(Enemy enemy);
}