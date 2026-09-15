using MUD.Art;
using MUD.Helpers;
using MUD.Models;

namespace MUD.Services;
public class BattleSystem
{
    PlayerCreator playerCreator = new PlayerCreator();
    SeaArt sea = new SeaArt();
    MountainsArt mountains = new MountainsArt();
    public void Battle(Enemy enemy, Player player, string art)
    {
        do
        {
            if(art == "Sea")
                sea.ShowGraphic();
            else if(art == "Mountains")
                mountains.ShowGraphic();

            Console.WriteLine($"Player: {player.Name} | HP: {player.Health} | Attack: {player.Attack} | Defense: {player.Defense} | Level: {player.Level}");
            Console.WriteLine($"Enemy: {enemy.Name} | HP: {enemy.Health}  | Attack: {enemy.Attack} | Defense: {enemy.Defense} | Level: {enemy.Level}\n");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Run away");

            Console.Write("\nYour choice: ");
            char choice = Console.ReadKey().KeyChar;

            switch (choice)
            {
                case '1':
                    float playerDamage = Math.Max(player.Attack - enemy.Defense, 0);
                    enemy.Health -= playerDamage;
                    Console.WriteLine($"\n\nYou attack the {enemy.Name} for {playerDamage} damage!\n");

                    float enemyDamage = Math.Max(enemy.Attack - player.Defense, 0);
                    player.Health -= enemyDamage;
                    Console.WriteLine($"The {enemy.Name} attacks you for {enemyDamage} damage!\n");

                    ConsoleHelper.Continue();
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
            Console.WriteLine($"\nYou have defeated the {enemy.Name}!");

            player.CurrentXp += enemy.Reward.Xp * 10;
            player.Gold += enemy.Reward.Gold * 5;

            if (player.CurrentXp >= player.NextLevelXp)
            {
                player.Level++;
                player.CurrentXp -= player.NextLevelXp;
                player.NextLevelXp = player.Level * 100;
                player.SkillPoints += 10;

                Console.WriteLine($"\nCongratulations! You have leveled up to level {player.Level}!");
                Console.WriteLine($"Youve earned 10 Skillpoints.\n");

                ConsoleHelper.Continue();

                playerCreator.SetSkillPoints(player);

                Console.WriteLine("Player stats upgraded! \n");

                ConsoleHelper.Continue();
            }
        }
        else if (player.Health <= 0)
        {
            Console.WriteLine("You have been defeated!\n");

            player.IsDead = true;
        }
    }
}
