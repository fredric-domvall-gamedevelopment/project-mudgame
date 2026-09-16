using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Services;
using MUD.Worlds;

namespace MUD;
public class GameManager
{
    GameWorld gameWorld = new GameWorld();
    Player player = new Player();
    PlayerCreator playerCreator = new PlayerCreator();
    ScoreSystem scoreSystem = new ScoreSystem();
    public void StartMenu()
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
                StartNewGame();
                break;

            case '2':
                HighscoreList();
                StartMenu();
                break;

            case '3':
                Console.WriteLine("Thank you for playing! Goodbye!");
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Invalid choice, please try again.");
                ConsoleHelper.Continue();

                StartMenu();
                break;       
        }
    }

    private void StartNewGame()
    {
        player = playerCreator.PlayerCreation();
        gameWorld.PlayGame(player);
    }
    private void HighscoreList()
    {
        List<Player> highscoreList = scoreSystem.GetHighscoreList();
        Console.WriteLine("-----Highscore List-----\n");

        if(highscoreList.Count == 0)
            Console.WriteLine("No players in the highscore list yet.");
        else
            foreach (var player in highscoreList)
                Console.WriteLine($"Name: {player.Name} || Score: {player.Score}");

        ConsoleHelper.Continue();
    }
}