using MUD.Worlds;

namespace MUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //WriteLine writes to the console
            Console.WriteLine("Welcome the magical worlds of MUDs");
            Console.WriteLine("Who are you?");

            //ReadLine wait for input from the user
            //string name declares a string value and sets it to be equal to the input
            string name = Console.ReadLine();

            //You can insert other values than strings into strings in many ways, this is one.
            Console.WriteLine("Oh, your name is " + name + "?");
            Console.WriteLine("Not what I would have chosen, but it will do I suppose...");

            //int health declares a int value and sets it to be equal to 100
            int health = 100;
            Console.WriteLine("You have " + health + " HP");

            //bool dead declares a boolean value and sets it to be equal to false
            bool dead = false;

            //float playerSpeed declares a float value and sets it to be equal to 1.5, the f is a specific float declaration
            //to make it different from doubles (5.5)
            float playerSpeed = 1.5f;
        }
    }
}