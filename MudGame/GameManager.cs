using Infrastructure.Configurations;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Services;
using MUD.Worlds;

namespace MUD;
public class GameManager
{
    Player player = new Player();
    PlayerCreator playerCreator = new PlayerCreator();
    private readonly ScoreSystem _scoreSystem;
    private readonly GameWorld _gameWorld;
    
    public GameManager(ScoreSystem scoreSystem, GameWorld gameWorld)
    {
        _scoreSystem = scoreSystem;
        _gameWorld = gameWorld;
    }
    public async Task StartMenu()
    {
        Console.WriteLine("Welcome the magical worlds of MUDs \n");
        Console.WriteLine("1. Start New Game");
        Console.WriteLine("2. Show Highscore List");
        Console.WriteLine("3. Exit");

        char choice = Console.ReadKey().KeyChar;
        Console.Clear();

        switch (choice)
        {
            case '1':
                player = playerCreator.PlayerCreation(player);
                await _gameWorld.PlayGame(player);

                await StartMenu();
                break;

            case '2':
                await HighscoreList();
                await StartMenu();
                break;

            case '3':
                Console.WriteLine("Thank you for playing! Goodbye!");
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Invalid choice, please try again.");
                ConsoleHelper.Continue();

                await StartMenu();
                break;
        }
    }

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

                if(player.SkillPoints > 0)
                {
                    Console.WriteLine($"You have {player.SkillPoints} skill points to spend. Spend them now?\n" +
                        $"Press Y to spend, any other key to skip.");
                    choice = Console.ReadKey(true).KeyChar;

                    if(choice == 'Y' || choice == 'y')
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

    private async Task HighscoreList()
    {
        List<Player> highscoreList = await _scoreSystem.GetHighscoreList();
        Console.WriteLine("-----Highscore List-----\n");

        if(highscoreList.Count == 0)
            Console.WriteLine("No players in the highscore list yet.");
        else
            foreach (var player in highscoreList)
                Console.WriteLine($"Name: {player.Name} || Score: {player.Score}");

        ConsoleHelper.Continue();
    }
}