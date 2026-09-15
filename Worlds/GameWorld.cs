using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Services;

namespace MUD.Worlds
{
    public class GameWorld
    {
        Player player = new Player();
        PlayerCreator playerCreator = new PlayerCreator();

        public void StartGame()
        {       
            Console.WriteLine("Welcome the magical worlds of MUDs \n");

            player = playerCreator.PlayerCreation();

            PlayGame();
        }

        private void PlayGame()
        {
            if(player.IsDead)
            {
                Console.WriteLine("You are dead, game over!");
                return;
            }
            Console.WriteLine($"Player: {player.Name} | HP: {player.Health}/{player.MaxHealth} | Attack: {player.Attack} | Defense: {player.Defense} | Gold: {player.Gold}\n" +
            $"STR: {player.Stats.Strength} | DEX: {player.Stats.Dexterity} | END: {player.Stats.Endurance} | XP: {player.CurrentXp} / {player.NextLevelXp}\n");

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Go to the Sea");
            Console.WriteLine("2. Go to the Mountains");
            Console.WriteLine("3. Go to the Tavern");

            char choice = Console.ReadKey().KeyChar;

            Console.Clear();

            switch (choice)
            {
                case '1':
                    GoToSea();
                    PlayGame();
                    break;

                case '2':
                    GoToMountains();
                    PlayGame();
                    break;

                case '3':
                    GoToTown();
                    PlayGame();
                    break;

                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    ConsoleHelper.Continue();

                    PlayGame();
                    break;
            }
        }

        private void GoToSea()
        {
            Sea sea = new Sea();
            sea.EnterSea(player);
        }

        private void GoToMountains()
        {
            Mountains mountains = new Mountains();
            mountains.EnterMountains(player);
        }

        private void GoToTown()
        {
            Town town = new Town();
            town.EnterTown(player);
        }

    }
}