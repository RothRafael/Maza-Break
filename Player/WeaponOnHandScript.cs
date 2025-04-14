using System;
using Godot;
using Godot.Collections;

public partial class WeaponOnHandScript : Node2D
{
    public GunBase currentGun;

    [Export] private Array<GunBase> currentEquipable = new();
    private int gunIndex = 0;

    public override void _Ready()
    {
        // Collect all GunBase children
        foreach (Node child in GetChildren())
        {
            if (child is GunBase gun)
            {
                currentEquipable.Add(gun);
            }
        }

        // Equip first gun
        if (currentEquipable.Count > 0)
        {
            currentGun = currentEquipable[0];
            EnableGun(currentGun);
        }

        // Disable all others
        for (int i = 1; i < currentEquipable.Count; i++)
        {
            DisableGun(currentEquipable[i]);
        }
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("next_gun"))
        {
            EquipNextGun();
        }
    }

    private void EquipNextGun()
    {
        if (currentEquipable.Count == 0) return;

        // Disable current gun
        DisableGun(currentGun);

        // Move to next index
        gunIndex = (gunIndex + 1) % currentEquipable.Count;
        currentGun = currentEquipable[gunIndex];

        // Position and rotate the gun
        currentGun.GlobalPosition = GlobalPosition;
        currentGun.GlobalRotation = GlobalRotation;

        // Enable the new gun
        EnableGun(currentGun);

        GD.Print($"Equipped: {currentGun.Name}");
    }

    public void ShootGun(Vector2 direction)
    {
        if (currentGun != null)
        {
            currentGun.Shoot(direction);
        }
    }

    public GunBase CurrentGun()
    {
        if (currentGun != null)
        {
            return currentGun;
        }
        else
        {
            GD.Print("No gun equipped.");
            return null;
        }
    }

    private void EnableGun(Node2D gun)
    {
        gun.Visible = true;
        gun.SetProcess(true);
        gun.SetPhysicsProcess(true);
    }

    private void DisableGun(Node2D gun)
    {
        gun.Visible = false;
        gun.SetProcess(false);
        gun.SetPhysicsProcess(false);
    }
}
