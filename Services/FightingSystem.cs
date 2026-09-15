using MUD.Helpers;
using MUD.Models;

namespace MUD.Services;
public class FightingSystem
{
    PlayerCreator playerCreator = new PlayerCreator();
    public void Battle(Enemy enemy, Player player)
    {
        do
        {
            Console.WriteLine($"Player: {player.Name} | HP: {player.Health} | Attack: {player.Attack} | Defense: {player.Defense} | Level: {player.Level}");
            Console.WriteLine($"Enemy: {enemy.Name} | HP: {enemy.Health}  | Attack: {enemy.Attack} | Defense: {enemy.Defense} | Level: {enemy.Level}");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Run away");

            char choice = Console.ReadKey().KeyChar;

            Console.Clear();

            switch (choice)
            {
                case '1':
                    float playerDamage = Math.Max(player.Attack - enemy.Defense, 0);
                    enemy.Health -= playerDamage;
                    Console.WriteLine($"\nYou attack the {enemy.Name} for {playerDamage} damage!");

                    float enemyDamage = Math.Max(enemy.Attack - player.Defense, 0);
                    player.Health -= enemyDamage;
                    Console.WriteLine($"\nThe {enemy.Name} attacks you for {enemyDamage} damage!");
                    break;

                case '2':
                    Console.WriteLine("\nYou run away from the battle.\n");
                    return;

                default:
                    Console.WriteLine("Invalid choice, please try again.\n");
                    ConsoleHelper.Continue();
                    break;
            }
        } while (enemy.Health > 0 && player.Health > 0);

        if (enemy.Health <= 0)
        {
            Console.WriteLine($"You have defeated the {enemy.Name}!");

            player.CurrentXp += enemy.Reward.Xp * 10;
            player.Gold += enemy.Reward.Gold * 5;

            if(player.CurrentXp >= player.NextLevelXp)
            {
                player.Level++;
                player.CurrentXp -= player.NextLevelXp;
                player.NextLevelXp = player.Level * 100;
                player.SkillPoints += 10;

                Console.WriteLine($"Congratulations! You have leveled up to level {player.Level}!");
                Console.WriteLine($"Youve earned 10 Skillpoints.\n");
                
                ConsoleHelper.Continue();

                playerCreator.SetSkillPoints(player);
            }
        }
        else if (player.Health <= 0)
        {
            Console.WriteLine("You have been defeated!\n");

            player.IsDead = true;
        }
    }
}
