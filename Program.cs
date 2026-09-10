using MUD.Models;
using MUD.Worlds;

namespace MUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "MUD - The Magical World";
            LevelOne levelOne = new LevelOne();
            levelOne.StartGame();
        }
    }
}