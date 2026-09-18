using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Services;
using MUD.Worlds;

namespace MUD;

public class GameManager(ScoreSystem scoreSystem, GameWorld gameWorld, PlayerCreator playerCreator)
{
    Player player = new Player();
    private readonly ScoreSystem _scoreSystem = scoreSystem;
    private readonly GameWorld _gameWorld = gameWorld;

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

    private async Task HighscoreList()
    {
        List<Player> highscoreList = await _scoreSystem.GetHighscoreList();
        Console.WriteLine("-----Highscore List-----\n");

        if (highscoreList.Count == 0)
            Console.WriteLine("No players in the highscore list yet.");
        else
            foreach (var player in highscoreList)
                Console.WriteLine($"Name: {player.Name} || Score: {player.Score}");

        ConsoleHelper.Continue();
    }
}