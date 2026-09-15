using MUD.Models;
using MUD.Services;

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

            Char choice = Console.ReadKey().KeyChar;
            Console.Clear();

            switch (choice)
            {
                case '1':
                    Console.WriteLine("You chose to go to the Sea");
                    GoToSea();
                    PlayGame();
                    break;

                case '2':
                    Console.WriteLine("You chose to go to the Mountains");
                    GoToMountains();
                    PlayGame();
                    break;

                case '3':
                    Console.WriteLine("You chose to go to the Tavern");
                    GoToTown();
                    PlayGame();
                    break;

                default:
                    Console.WriteLine("Invalid choice, please try again.");
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