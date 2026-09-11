using MUD.Models;
using MUD.Models.Enums;
using MUD.Services;

namespace MUD.Services;

public class EnemyCreator
{
    public Enemy CreateEnemy(EnemyType enemyType)
    {
        Enemy enemy = new Enemy();
        CharacterStatsCalculator characterStatsCalculator = new CharacterStatsCalculator();

        switch(enemyType)
        {
            case EnemyType.Goblin:
                enemy.Name = "Goblin";
                enemy.MaxHealth = 50f;
                enemy.Stats.Strength = 7f;
                enemy.Stats.Dexterity = 5f;
                enemy.Stats.Endurance = 3f;
                enemy.Attack = characterStatsCalculator.CalculateCharacterAttack(enemy);
                enemy.Defense = characterStatsCalculator.ClalculateCharacterDefense(enemy);
                enemy.MaxHealth = characterStatsCalculator.CalculateCharacterHealth(enemy);
                enemy.Health = enemy.MaxHealth;
                enemy.IsDead = false;
                break;
            case EnemyType.Orc:
                enemy.Name = "Orc";
                enemy.MaxHealth = 60f;
                enemy.Stats.Strength = 8f;
                enemy.Stats.Dexterity = 6f;
                enemy.Stats.Endurance = 4f;
                enemy.Attack = characterStatsCalculator.CalculateCharacterAttack(enemy);
                enemy.Defense = characterStatsCalculator.ClalculateCharacterDefense(enemy);
                enemy.MaxHealth = characterStatsCalculator.CalculateCharacterHealth(enemy);
                enemy.Health = enemy.MaxHealth;
                enemy.IsDead = false;
                break;
            case EnemyType.Troll:
                enemy.Name = "Troll";
                enemy.MaxHealth = 70f;
                enemy.Stats.Strength = 9f;
                enemy.Stats.Dexterity = 7f;
                enemy.Stats.Endurance = 5f;
                enemy.Attack = characterStatsCalculator.CalculateCharacterAttack(enemy);
                enemy.Defense = characterStatsCalculator.ClalculateCharacterDefense(enemy);
                enemy.MaxHealth = characterStatsCalculator.CalculateCharacterHealth(enemy);
                enemy.Health = enemy.MaxHealth;
                enemy.IsDead = false;
                break;
            default:
                throw new ArgumentException("Invalid enemy type");
        }

        return enemy;
    }

    public Enemy CreateRandomEnemy()
    {
        Random random = new Random();
        EnemyType enemytype = (EnemyType)random.Next(0, 3);

        return CreateEnemy(enemytype);
    }
}
