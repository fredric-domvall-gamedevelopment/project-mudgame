using Infrastructure.Configurations;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Repositories;
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
                player = playerCreator.PlayerCreation();
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
                Console.WriteLine("Nothing to show yet, will be added soon");
                ConsoleHelper.Continue();

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