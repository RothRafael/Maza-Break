using Godot;
using System;

public partial class HitNumbersComponent : Node
{
    [Export] public PackedScene HitNumberScene;
    [Export] public Color HitCriticalColor;

    public override void _Ready()
    {
        if (HitNumberScene == null)
        {
            GD.PrintErr("HitNumberScene is not assigned!");
        }
    }

    public Node2D CreateHitNumber(Vector2 position, int damage, bool isCritical)
    {
        if (HitNumberScene == null)
        {
            GD.PrintErr("HitNumberScene is not assigned!");
            return null;
        }

        var hitNumberInstance = HitNumberScene.Instantiate<Node2D>();
        if (hitNumberInstance != null)
        {
            GetParent().AddChild(hitNumberInstance);
            hitNumberInstance.Position = position;
            hitNumberInstance.GetNode<Label>("Label").Text = damage.ToString(); // Set the hit number text
            hitNumberInstance.GetNode<Label>("Label").LabelSettings.FontColor = Colors.White; // Change color for critical hits

        }

        if(isCritical)
        {
            hitNumberInstance.GetNode<Label>("Label").LabelSettings.FontColor = HitCriticalColor; // Change color for critical hits
        }
        return hitNumberInstance;
    }
}
