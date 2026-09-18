using Infrastructure.Enums;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class EnemyCreator
{
    private readonly List<Enemy> _enemies = new List<Enemy>();
    public Enemy CreateEnemy(EnemyType enemyType, Player player)
    {
        Enemy enemy = new Enemy();
        CharacterStatsCalculator characterStatsCalculator = new CharacterStatsCalculator();

        switch (enemyType)
        {
            case EnemyType.Goblin:
                enemy.Name = "Goblin";
                enemy.Level = Random.Shared.Next(1, 4);
                enemy.MaxHealth = 50f;
                enemy.Stats.Strength = Random.Shared.Next(player.Level, player.Level + 2) * enemy.Level;
                enemy.Stats.Dexterity = Random.Shared.Next(player.Level, player.Level + 2) * enemy.Level;
                enemy.Stats.Endurance = Random.Shared.Next(player.Level, player.Level + 2) * enemy.Level;
                enemy.Attack = characterStatsCalculator.CalculateCharacterAttack(enemy);
                enemy.Defense = characterStatsCalculator.ClalculateCharacterDefense(enemy);
                enemy.MaxHealth = characterStatsCalculator.CalculateCharacterHealth(enemy);
                enemy.Health = enemy.MaxHealth;
                enemy.Reward.Xp = Random.Shared.Next(1, 10) * enemy.Level;
                enemy.Reward.Gold = Random.Shared.Next(1, 10) * enemy.Level;
                enemy.IsDead = false;
                break;

            case EnemyType.Orc:
                enemy.Name = "Orc";
                enemy.Level = Random.Shared.Next(player.Level, player.Level + 3);
                enemy.MaxHealth = 60f;
                enemy.Stats.Strength = Random.Shared.Next(player.Level, player.Level + 2) * enemy.Level;
                enemy.Stats.Dexterity = Random.Shared.Next(player.Level, player.Level + 2) * enemy.Level;
                enemy.Stats.Endurance = Random.Shared.Next(player.Level, player.Level + 2) * enemy.Level;
                enemy.Attack = characterStatsCalculator.CalculateCharacterAttack(enemy);
                enemy.Defense = characterStatsCalculator.ClalculateCharacterDefense(enemy);
                enemy.MaxHealth = characterStatsCalculator.CalculateCharacterHealth(enemy);
                enemy.Health = enemy.MaxHealth;
                enemy.Reward.Xp = Random.Shared.Next(1, 10) * enemy.Level;
                enemy.Reward.Gold = Random.Shared.Next(1, 10) * enemy.Level;
                enemy.IsDead = false;
                break;

            case EnemyType.Troll:
                enemy.Name = "Troll";
                enemy.Level = Random.Shared.Next(player.Level, player.Level + 4);
                enemy.MaxHealth = 70f;
                enemy.Stats.Strength = Random.Shared.Next(player.Level, player.Level + 2) * enemy.Level;
                enemy.Stats.Dexterity = Random.Shared.Next(player.Level, player.Level + 2) * enemy.Level;
                enemy.Stats.Endurance = Random.Shared.Next(player.Level, player.Level + 2) * enemy.Level;
                enemy.Attack = characterStatsCalculator.CalculateCharacterAttack(enemy);
                enemy.Defense = characterStatsCalculator.ClalculateCharacterDefense(enemy);
                enemy.MaxHealth = characterStatsCalculator.CalculateCharacterHealth(enemy);
                enemy.Health = enemy.MaxHealth;
                enemy.Reward.Xp = Random.Shared.Next(1, 10) * enemy.Level;
                enemy.Reward.Gold = Random.Shared.Next(1, 10) * enemy.Level;
                enemy.IsDead = false;
                break;

            default:
                throw new ArgumentException("Invalid enemy type");
        }

        return enemy;
    }

    public Enemy CreateRandomEnemy(Player player)
    {
        Random random = new Random();

        EnemyType enemytype = (EnemyType)random.Next(0, 3);

        return CreateEnemy(enemytype, player);
    }
}
