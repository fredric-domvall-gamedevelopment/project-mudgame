using Infrastructure.Helpers;
using Infrastructure.Models;

namespace MUD;
public class PlayerManager(PlayerCreator playerCreator)
{
    public void PlayerMenu(Player player)
    {
        Console.WriteLine("Player Menu");
        Console.WriteLine("1. Show Player Stats");
        Console.WriteLine("2. Back to Game");

        char choice = Console.ReadKey().KeyChar;
        Console.Clear();

        switch (choice)
        {
            case '1':
                Console.WriteLine(
                    "--- Player Stats ---    {0,-10} {1,-15} {2,-10} {3,-10} {4,-10} {5}\n",
                    "Name:", player.Name,
                    "Level:", player.Level,
                    "Score:", player.Score);

                Console.WriteLine(
                    "{0,-12} {1,-12} {2,-12} {3,-12}",
                    "Health:", $"{player.Health} / {player.MaxHealth}",
                    "Strength:", player.Stats.Strength);

                Console.WriteLine(
                    "{0,-12} {1,-12} {2,-12} {3,-12}",
                    "Attack:", player.Attack,
                    "Dexterity:", player.Stats.Dexterity);

                Console.WriteLine(
                    "{0,-12} {1,-12} {2,-12} {3,-12}\n",
                    "Defense:", player.Defense,
                    "Endurance:", player.Stats.Endurance);

                if (player.SkillPoints > 0)
                {
                    Console.WriteLine($"You have {player.SkillPoints} skill points to spend. Spend them now?\n" +
                        $"Press Y to spend, any other key to skip.");
                    choice = Console.ReadKey(true).KeyChar;

                    if (choice == 'Y' || choice == 'y')
                        playerCreator.SetSkillPoints(player);
                    else
                        ConsoleHelper.Continue();
                }

                PlayerMenu(player);
                break;

            case '2':
                return;

            default:
                Console.WriteLine("Invalid choice, please try again.");
                ConsoleHelper.Continue();

                PlayerMenu(player);
                break;

        }
    }
}
