using MUD.Models;
using MUD.Worlds;

namespace MUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LevelOne levelOne = new LevelOne();
            levelOne.StartGame();
        }
    }
}