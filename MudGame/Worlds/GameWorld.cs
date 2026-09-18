using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Services;

namespace MUD.Worlds
{
    public class GameWorld(ScoreSystem scoreSystem, PlayerManager playerManager, Sea sea, Mountains mountains)
    {
        private readonly ScoreSystem _scoreSystem = scoreSystem;

        Town town = new Town();

        public async Task PlayGame(Player player)
        {
            _scoreSystem.CalculateHighscore(player);

            if (player.IsDead)
            {
                Console.WriteLine("You are dead, game over!");

                var result = await _scoreSystem.AddPlayerToHighscoreList(player);
                return;
            }

            Console.WriteLine($"Player: {player.Name} | HP: {player.Health}/{player.MaxHealth} | Attack: {player.Attack} | Defense: {player.Defense} | Gold: {player.Gold}\n" +
            $"STR: {player.Stats.Strength} | DEX: {player.Stats.Dexterity} | END: {player.Stats.Endurance} | XP: {player.CurrentXp} / {player.NextLevelXp}\n");

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Go to the Sea");
            Console.WriteLine("2. Go to the Mountains");
            Console.WriteLine("3. Go to the Tavern\n");
            Console.WriteLine("M. Open Player Menu");

            char choice = Console.ReadKey().KeyChar;

            Console.Clear();

            switch (choice)
            {
                case '1':
                    sea.EnterSea(player);
                    await PlayGame(player);
                    break;

                case '2':
                    mountains.EnterMountains(player);
                    await PlayGame(player);
                    break;

                case '3':
                    town.EnterTown(player);
                    await PlayGame(player);
                    break;

                case 'M' or 'm':
                    playerManager.PlayerMenu(player);
                    await PlayGame(player);
                    break;

                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    ConsoleHelper.Continue();

                    await PlayGame(player);
                    break;
            }
        }
    }
}