using MUD.Worlds;

namespace MUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "MUD - The Magical World";
            GameWorld levelOne = new GameWorld();
            levelOne.StartGame();
        }
    }
}