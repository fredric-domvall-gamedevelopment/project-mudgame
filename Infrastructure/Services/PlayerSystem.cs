using Infrastructure.Helpers;
using Infrastructure.Models;

namespace Infrastructure.Services;
public class PlayerSystem
{
    CharacterStatsCalculator calculator = new CharacterStatsCalculator();
    char choice;
    public Player CreatePlayer(Player player)
    {
        player.SkillPoints = 20;
        player.Level = 1;
        player.NextLevelXp = 100;
        player.Stats.Strength = 0f;
        player.Stats.Dexterity = 0f;
        player.Stats.Endurance = 0f;
        player.Attack = 0f;
        player.Defense = 0f;
        player.MaxHealth = 50f;

        do
        {
            Console.WriteLine("-----Create your character-----\n");
            Console.WriteLine("What is your name?");

            player.Name = Console.ReadLine()!;

            if (string.IsNullOrEmpty(player.Name))
            {
                Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                ConsoleHelper.Continue();
            }

        } while (string.IsNullOrEmpty(player.Name));

        Console.Clear();

        SetSkillPoints(player);

        return player;
    }

    public void SetSkillPoints(Player player)
    {
        do
        {
            Console.WriteLine("-----Set your character stats-----\n");
            Console.WriteLine($"You have {player.SkillPoints} points to spend\n");
            Console.WriteLine($"1. Increase Strength  | Current STR: {player.Stats.Strength}");
            Console.WriteLine($"2. Increase Dexterity | Current DEX: {player.Stats.Dexterity}");
            Console.WriteLine($"3. Increase Endurance | Current END: {player.Stats.Endurance}");

            choice = Console.ReadKey().KeyChar;
            Console.Clear();

            switch (choice)
            {
                case '1':
                    player.Stats.Strength++;
                    player.SkillPoints--;
                    break;

                case '2':
                    player.Stats.Dexterity++;
                    player.SkillPoints--;
                    break;

                case '3':
                    player.Stats.Endurance++;
                    player.SkillPoints--;
                    break;

                default:
                    Console.WriteLine("Invalid choice, please try again.\n");
                    ConsoleHelper.Continue();
                    break;
            }
            if (player.SkillPoints == 0)
            {
                player.Attack = calculator.CalculateCharacterAttack(player);
                player.Defense = calculator.ClalculateCharacterDefense(player);
                player.MaxHealth = calculator.CalculateCharacterHealth(player);
                player.Health = player.MaxHealth;
            }
        } while (player.SkillPoints > 0);
    }
}
