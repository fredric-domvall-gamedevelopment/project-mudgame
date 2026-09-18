using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Services;

namespace MUD;

public class PlayerCreator
{
    private readonly PlayerSystem playerSystem;

    public PlayerCreator(PlayerSystem playerSystem)
    {
        this.playerSystem = playerSystem;
    }

    public Player PlayerCreation(Player player)
    {
        SetPlayerName(player);

        SetSkillPoints(player);

        CreatePlayerConfirmation(player);

        return player;
    }
    public void SetPlayerName(Player player)
    {
        Console.WriteLine("-----Create your character-----\n");
        Console.WriteLine("What is your name?");

        player.Name = Console.ReadLine()!;
        var result = playerSystem.CreatePlayer(player);

        if (!result.IsSuccess && result.Information != null)
            foreach (var info in result.Information)
                Console.WriteLine(info);

        ConsoleHelper.Continue();

        if (string.IsNullOrEmpty(result.Data!.Name) && string.IsNullOrWhiteSpace(result.Data.Name))
            SetPlayerName(player);



    }
    public void SetSkillPoints(Player player)
    {
        Console.Clear();
        Console.WriteLine("-----Set your character stats-----\n");
        Console.WriteLine($"You have {player.SkillPoints} points to spend\n");
        Console.WriteLine($"1. Increase Strength  | Current STR: {player.Stats.Strength}");
        Console.WriteLine($"2. Increase Dexterity | Current DEX: {player.Stats.Dexterity}");
        Console.WriteLine($"3. Increase Endurance | Current END: {player.Stats.Endurance}");

        var result = playerSystem.SetSkillPoints(player, Console.ReadKey(true).KeyChar);

        if (result.Information != null)
            foreach (var info in result.Information)
                Console.WriteLine(info);

        if (!result.IsSuccess)
            ConsoleHelper.Continue();

        if (player.SkillPoints > 0)
            SetSkillPoints(player);
    }
    private Player CreatePlayerConfirmation(Player player)
    {
        Console.Clear();
        Console.WriteLine($"Your character {player.Name} has been created with the following stats:\n" +
            "\n" +
                $"Strength: {player.Stats.Strength}\n" +
                $"Dexterity: {player.Stats.Dexterity}\n" +
                $"Endurance: {player.Stats.Endurance}\n" +
                $"Attack: {player.Attack}\n" +
                $"Defense: {player.Defense}\n" +
                $"Health: {player.Health}/{player.MaxHealth}\n");

        Console.WriteLine("\nDo you want to keep this character? (Press Y to keep, any other key to restart): ");

        char choice = Console.ReadKey(true).KeyChar;
        if (choice == 'Y' || choice == 'y')
        {
            Console.WriteLine($"\nCharacter {player.Name} has been created successfully!");
            ConsoleHelper.Continue();

            return player;
        }

        Console.WriteLine("\nRestarting character creation...");
        ConsoleHelper.Continue();

        player = new Player();
        return PlayerCreation(player);

    }
}
