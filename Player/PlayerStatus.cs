using Godot;

public partial class PlayerStatus : Node, IPlayerStatus, IDamagable
{
    [Export] public Faction faction { get; set; } = Faction.Player;
    public static PlayerStatus Instance;

    public override void _Ready()
    {
        Instance = this;
        Vida = MaxVida;
        Energia = MaxEnergia;
        Armor = MaxArmor;
    }

    // Current and max values
    [Export] public int MaxVida { get; set; } = 20;
    [Export] public int MaxEnergia { get; set; } = 100;
    [Export] public int MaxArmor { get; set; } = 5;

    public int Vida { get; set; }
    public int Energia { get; set; }
    public float Armor { get; set; }
    public bool _isDead { get; set; } = false;

    [Export] public float ArmorRestoreRate { get; set; } = 10f;
    [Export] public Vector2 playerPosition = new Vector2(0, 0);
    [Export] public Vector2 lookDirection = new Vector2(0, 0);
    [Export] private PlayerUIComponent playerUIComponent;
    [Export] private float timeSinceLastHit = 0f;
    [Export] private float timeSinceLastShot = 0f;
    [Export] private float armorRestoreDelay = 0f;


    public override void _Process(double delta)
    {
        if(_isDead) {
            ProcessMode = ProcessModeEnum.Disabled;
        }
        ManageArmor(delta);
    }

    private void ManageArmor(double delta)
    {
        timeSinceLastHit += (float)delta;

        if (timeSinceLastHit >= armorRestoreDelay)
        {
            if (Armor < MaxArmor)
            {
                Armor += ArmorRestoreRate * (float)delta;
                // GD.Print($"Armor restored: {Armor}");

                if (Armor > MaxArmor)
                    Armor = MaxArmor;
            }
        }

        // GD.Print($"Armor: {Armor}, Time since last hit: {timeSinceLastHit}");
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        // GD.Print($"Player took {damage} damage. Health: {Vida}, Armor: {Armor}");

        if (Armor > 0)
        {
            Armor -= damage;
            if (Armor < 0)
            {
                // Excess damage goes to health
                Vida += (int)Armor;
                Armor = 0;
            }
        }
        else
        {
            Vida -= damage;
        }

        if (Vida <= 0)
        {
            Vida = 0;
            Die();
        }
        Vida = Mathf.Max(Vida, 0);
        timeSinceLastHit = 0f;
        UpdateUI();
    }

    public bool canShoot(int energyCost)
    {
        if (Energia >= energyCost)
        {
            Energia -= energyCost;
            // GD.Print($"Player shot. Energy: {Energia}");
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
    public int GetVida() => Vida;
    public int GetEnergia() => Energia;
    public int GetArmor() => (int)Armor;

    private void UpdateUI()
    {
        if (playerUIComponent != null)
        {
            playerUIComponent.UpdateUI(Vida, Energia, (int)Armor);
        }
        else
        {
            GD.PrintErr("PlayerUIComponent is not assigned!");
        }
    }

    public void TakeDamage(int damage, bool isCritical)
    {
        // GD.Print($"Player took {damage} damage. Critical: {isCritical}");
        TakeDamage(damage);
    }
    private void Die(){
        GD.Print("Player has died.");
        _isDead = true;
        playerUIComponent.ShowGameOver();

    }
}
