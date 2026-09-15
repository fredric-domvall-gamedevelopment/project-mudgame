using MUD.Helpers;
using MUD.Models;

namespace MUD.Worlds;
public class Town
{
    public void EnterTown(Player player)
    {
        Console.WriteLine("You have entered the town.");
        Console.WriteLine("only a Tavern remain standing. Maybe can rest here and recover your health. \n");
        
        ConsoleHelper.Continue();

        Tavern(player);
    }

    private void Tavern(Player player)
    {
        Console.WriteLine("----------------THE TAVERN-----------------\n");
        Console.WriteLine("you enter the tavern, it is warm and cozy");
        Console.WriteLine("you meet a bartender, he offers you a room to rest in for 100 Gold");
        Console.WriteLine("do you accept his  offer?  \n  (press y to accept, any other key to decline)");

        char choice = Console.ReadKey().KeyChar;

        Console.Clear();

        if (choice == 'y')
        {
            Console.WriteLine("\nYou accept the offer and rest in the tavern.");
            Console.WriteLine("You recover to full health.");

            if(player.Gold < 100)
            {
                Console.WriteLine("However, you don't have enough Gold to pay for the room.");
                Console.WriteLine("You leave the tavern without resting.");
            }
            else
            {
                Console.WriteLine("You pay 100 Gold for the room and rest in the tavern.");
                player.Health = player.MaxHealth;
                player.Gold -= 100;
            }
        }
        else
        {
            Console.WriteLine("\nYou decline the offer and continue on your way.");
        }

        ConsoleHelper.Continue();
    }

}
