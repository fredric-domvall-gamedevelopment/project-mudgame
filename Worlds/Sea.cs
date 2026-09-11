using MUD.Art;
using MUD.Models;
using MUD.Models.Enums;
using MUD.Services;

namespace MUD.Worlds;
public class Sea
{

    public void EnterSea(Player player)
    {
        SeaArt sea = new SeaArt();
        sea.ShowGraphic();

        EnemyCreator enemyCreator = new EnemyCreator();
        Battle(enemyCreator.CreateRandomEnemy(new Enemy()), player);

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    private void Battle(Enemy enemy, Player player)
    {
        do
        {
            Console.WriteLine($"Player: {player.Name} | HP: {player.Health}");
            Console.WriteLine($"Enemy: {enemy.Name} | HP: {enemy.Health}");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Run away");

            char choice = Console.ReadKey().KeyChar;
            Console.Clear();
            switch (choice)
            {
                case '1':
                    enemy.Health -= player.Attack - enemy.Defense;
                    Console.WriteLine($"You attack the {enemy.Name} for {player.Attack - enemy.Defense} damage!");
                    player.Health -= enemy.Attack - player.Defense;
                    Console.WriteLine($"The {enemy.Name} attacks you for {enemy.Attack - player.Defense} damage!");
                    break;
                case '2':
                    Console.WriteLine("You run away from the battle.");
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        } while (enemy.Health > 0 && player.Health > 0);

        if (enemy.Health <= 0)
        {
            Console.WriteLine($"You have defeated the {enemy.Name}!");
        }
        else if (player.Health <= 0)
        {
            Console.WriteLine("You have been defeated!");
            player.IsDead = true;
        }
    }
}
