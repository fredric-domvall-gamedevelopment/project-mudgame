using MUD.Models;
using MUD.Models.Enums;

namespace MUD.Services;

public class EnemyCreator
{
    public Enemy CreateEnemy(EnemyType enemyType)
    {
        Enemy enemy = new Enemy();
        switch(enemyType)
        {
            case EnemyType.Goblin:
                enemy.Name = "Goblin";
                enemy.MaxHealth = 50f;
                enemy.Health = enemy.MaxHealth;
                enemy.Attack = 10f;
                enemy.Defense = 5f;
                enemy.IsDead = false;
                break;
            case EnemyType.Orc:
                enemy.Name = "Orc";
                enemy.MaxHealth = 80f;
                enemy.Health = enemy.MaxHealth;
                enemy.Attack = 15f;
                enemy.Defense = 5f;
                enemy.IsDead = false;
                break;
            case EnemyType.Troll:
                enemy.Name = "Troll";
                enemy.MaxHealth = 80f;
                enemy.Health = enemy.MaxHealth;
                enemy.Attack = 20f;
                enemy.Defense = 8f;
                enemy.IsDead = false;
                break;
            default:
                throw new ArgumentException("Invalid enemy type");
        }
        return enemy;
    }
}
