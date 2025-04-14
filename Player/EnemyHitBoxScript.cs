using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyHitBoxScript : Area2D
{
    private List<Enemy> _enemiesPositions = new List<Enemy>();
    private CollisionShape2D _collisionShape;
    private Label _label;
    public override void _Ready() {

        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        _label = GetParent().GetNode<Label>("Label");

        if(_label == null); GD.PrintErr("Label not found in EnemyHitBoxScript");
    }
    private void _on_area_2d_body_entered(Node2D body)
    {
        if (body is Enemy enemy)
        {
            // Check if the enemy is already in the list
            if (!_enemiesPositions.Contains(enemy))
            {
                AddEnemieToText(enemy);
                _enemiesPositions.Add(enemy);
                GD.Print("Enemy hitbox entered: " + enemy.Name);
                // Call the enemy's method to handle the hit
            }
        }
    }
    private void _on_enemy_hit_box_body_exited(Node2D body)
    {
        if (body is Enemy enemy)
        {
            // Remove the enemy from the list when it exits
            if (_enemiesPositions.Contains(enemy))
            {
                RemoveEnemyFromText(enemy);
                _enemiesPositions.Remove(enemy);
                GD.Print("Enemy hitbox exited: " + enemy.Name);
                // Call the enemy's method to handle the exit
            }
        }
    }

    public void SetHitBoxSize(float size) {
        if (_collisionShape.Shape is CircleShape2D circleShape)
        {
            circleShape.Radius = size;
        }
    }

    public Enemy GetNearestEnemy(Vector2 playerPos) {
        Enemy nearestEnemy = null;
        float nearestDistance = float.MaxValue;
    
        foreach (Enemy enemy in _enemiesPositions)
        {
            if(enemy == null || !enemy.IsInsideTree())
                continue;

            float distance = playerPos.DistanceTo(enemy.GlobalPosition);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
    public bool IsListEmpty() {
        return _enemiesPositions.Count == 0;
    }
    public void AddEnemieToText(Enemy enemy) {
        if (!_enemiesPositions.Contains(enemy))
        {
            _enemiesPositions.Add(enemy);
            _label.Text += enemy + "\n";
        }
    }
    public void RemoveEnemyFromText(Enemy enemy) {
        if (_enemiesPositions.Contains(enemy))
        {
            _enemiesPositions.Remove(enemy);
            _label.Text = _label.Text.Replace(enemy.ToString(), "");
        }
    }
}
