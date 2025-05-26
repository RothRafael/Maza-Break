public class EnemyData
{
    public Faction faction { get; set; }
    public int health { get; set; }
    public float speed { get; set; }
    public float fireRate { get; set; }
    public int coinDropAmmount { get; set; }
    public bool isPushable { get; set; }
    public float shootingRange { get; set; }

    public EnemyData(Faction faction, int health, float speed, float fireRate, int coinDropAmmount, bool isPushable, float shootingRange)
    {
        this.faction = faction;
        this.health = health;
        this.speed = speed;
        this.fireRate = fireRate;
        this.coinDropAmmount = coinDropAmmount;
        this.isPushable = isPushable;
        this.shootingRange = shootingRange;
    }


}
public class EnemyInitializer()
{
    public void InitializeSelf(Enemy enemy, EnemyData enemyData)
    {
        enemy.faction = enemyData.faction;
        enemy.Health = enemyData.health;
        enemy.Speed = enemyData.speed;
        enemy.fireRate = enemyData.fireRate;
        enemy.coinDropAmmount = enemyData.coinDropAmmount;
        enemy.isPushable = enemyData.isPushable;
        enemy.shootingRange = enemyData.shootingRange;
    }
}