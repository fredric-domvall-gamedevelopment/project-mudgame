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
    public void StartMenu()
    {
        Console.WriteLine("Welcome the magical worlds of MUDs \n");
        Console.WriteLine("1. Start New Game");
        Console.WriteLine("2. Exit");

        char choice = Console.ReadKey().KeyChar;
        Console.Clear();

        switch (choice)
        {
            case '1':
                StartNewGame();
                break;

            case '2':
                ExitGame();
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

    private void ExitGame()
    {
        Console.WriteLine("Thank you for playing! Goodbye!");
        Environment.Exit(0);
    }
}