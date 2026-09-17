using Infrastructure.Helpers;
using Infrastructure.Models;

namespace Infrastructure.Services;
public class PlayerCreator
{
    PlayerSystem playerSystem = new PlayerSystem();
    Player player = new Player();

    public Player PlayerCreation()
    {
        do
        {    
            player = playerSystem.CreatePlayer(player);

            Console.WriteLine($"Name: {player.Name} \n" +
            $"STR: {player.Stats.Strength} || DEX: {player.Stats.Dexterity} || END: {player.Stats.Endurance}\n" +
            $"HP: {player.Health}/{player.MaxHealth} || Attack: {player.Attack} || Defense: {player.Defense}\n");
            Console.WriteLine("Are you happy with your character? (press n to restart, any other key to continue)");

            choice = Console.ReadKey().KeyChar;
            Console.Clear();

        } while (choice == 'N' || choice == 'n');

        return player;
    }
}
