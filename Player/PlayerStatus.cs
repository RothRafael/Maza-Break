using Godot;

public partial class PlayerStatus : Node, IPlayerStatus
{
    public static PlayerStatus Instance;
    public override void _Ready()
    {
        Instance = this;
    }

    // Implementing IPlayerStatus properties
    [Export] public int Vida { get; set; } = 20;
    [Export] public int Energia { get; set; } = 100;
    [Export] public int Armor { get; set; } = 0;
    [Export] public float ArmorRestoreRate { get; set; } = 0.1f;
    [Export] private float timeSinceLastHit = 0f;
    [Export] private float timeSinceLastShot = 0f;
    [Export] public Vector2 playerPosition = new Vector2(0, 0);
    [Export] private PlayerUIComponent playerUIComponent;

    public override void _Process(double delta)
    {
        ManageArmor(delta);
    }
    private void ManageArmor(double delta)
    {
        // 
    }
    public void TakeDamage(int damage)
    {
        if (Armor > 0)
        {
            Armor -= damage;
        }
        else
        {
            Vida -= damage;
        }
        GD.Print($"Player took {damage} damage. Health: {Vida}, Armor: {Armor}");
        timeSinceLastHit = 0f;
        UpdateUI();
    }
    public bool canShoot(int energyCost)
    {
        if (Energia >= energyCost)
        {
            Energia -= energyCost;
            GD.Print($"Player shot. Energy: {Energia}");
            UpdateUI();
            return true;
        }
        UpdateUI();
        return false;
    }
    public async void SlowTime(float scale, float duration)
    {
        Engine.TimeScale = scale;
        await ToSignal(GetTree().CreateTimer(duration * (1 / scale)), "timeout");
        Engine.TimeScale = 1.0f;
    }
    // Getters
    public int GetVida()
    {
        return Vida;
    }
    public int GetEnergia()
    {
        return Energia;
    }
    public int GetArmor()
    {
        return Armor;
    }
    void UpdateUI() {
        if (playerUIComponent != null)
        {
            playerUIComponent.UpdateUI(Vida, Energia, Armor);
        }
        else
        {
            GD.PrintErr("PlayerUIComponent is not assigned!");
        }
    }

    public void TakeDamage(int damage, bool isCritical)
    {
        throw new System.NotImplementedException();
    }
}