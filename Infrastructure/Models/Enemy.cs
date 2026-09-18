using Infrastructure.Enums;

namespace Infrastructure.Models;

public class Enemy : Character
{
    public EnemyType Type { get; set; }
    public EnemyRank Rank { get; set; }
    public EnemySpawnArea SpawnArea { get; set; }
    public Reward Reward { get; set; } = new Reward();

}