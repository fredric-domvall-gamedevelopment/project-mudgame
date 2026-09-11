using MUD.Models;

namespace MUD.Worlds;
public class Town
{
    public void EnterTown(Player player)
    {
        Console.WriteLine("You have entered the town.");
        Console.WriteLine("only a Tavern remain standing. Maybe can rest here and recover your health. \n");
        Tavern(player);
    }

    private void Tavern(Player player)
    {
        Console.WriteLine("----------------THE TAVERN-----------------\n");
        Console.WriteLine("you enter the tavern, it is warm and cozy");
        Console.WriteLine("you meet a bartender, he offers you a room to rest in");
        Console.WriteLine("do you accept his kind offer?  \n  (press y to accept, any other key to decline)");

        char choice = Console.ReadKey().KeyChar;
        Console.Clear();
        if (choice == 'y')
        {
            Console.WriteLine("You accept the offer and rest in the tavern.");
            Console.WriteLine("You recover to full health.");
            player.Health = player.MaxHealth;
        }
        else
        {
            Console.WriteLine("You decline the offer and continue on your way.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

}
