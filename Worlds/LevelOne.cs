using MUD.Art;

namespace MUD.Worlds
{
    public class LevelOne
    {
        public string Name;
        public int Health;
        public bool Dead;

        public void StartGame()
        {
            Console.WriteLine("Welcome the magical worlds of MUDs");
            Console.WriteLine("Who are you?");
            Name = Console.ReadLine();
            Console.WriteLine("Oh, your name is " + Name);
            Console.WriteLine("Not what I would have chosen, but it will do I suppose...");
            Health = 100;
            Console.WriteLine("You have " + Health + " HP");
            Dead = false;

            if(Dead)
                Console.WriteLine("You are however, dead?");
            else
                Console.WriteLine("You are alive? Good!");
        }
    }
}