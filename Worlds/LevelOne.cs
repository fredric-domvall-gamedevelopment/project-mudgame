using MUD.Art;
using MUD.Models;

namespace MUD.Worlds
{
    public class LevelOne
    {
        Player player = new Player();


        public void StartGame()
        {
            Console.WriteLine("Welcome the magical worlds of MUDs");
            Console.WriteLine("Who are you?");

            while(String.IsNullOrEmpty(player.Name)) {
                player.Name = Console.ReadLine()!;
            }

            Console.WriteLine("Oh, your name is " + player.Name);
            Console.WriteLine("Not what I would have chosen, but it will do I suppose...");
            player.Health = 100;
            Console.WriteLine("You have " + player.Health + " HP");
            player.IsDead = false;

            if (player.IsDead)
                Console.WriteLine("You are however, dead?");
            else
                Console.WriteLine("You are alive? Good!");

            PlayGame();
        }

        private void CreatePlayer()
        {

        }

        private void PlayGame()
        {
            Console.WriteLine($"Player: {player.Name} | HP: {player.Health}");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Go to the Sea");
            Console.WriteLine("2. Go to the Forest");
            Console.WriteLine("3. Go to the Tavern");

            Char choice = Console.ReadKey().KeyChar;

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
                    GoToTavern();
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
            sea.ShowGraphic();
            Console.WriteLine("you meet a seamonster, you get scared and run away. you loose 10 Hp");
            player.Health -= 10;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void GoToMountains()
        {
            Mountains mountain = new Mountains();
            mountain.ShowGraphic();
            Console.WriteLine("you meet a goblin, you get scared and run away. you loose 10 Hp");
            player.Health -= 10;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void GoToTavern()
        {
            Console.WriteLine("----------------THE TAVERN-----------------");
            Console.WriteLine("you enter the tavern, it is warm and cozy");
            Console.WriteLine("you meet a bartender, he offers you a room to rest in");
            Console.WriteLine("do you accept his kind offer?  \n  (press y to accept, any other key to decline)");
            char choice = Console.ReadKey().KeyChar;
            if (choice == 'y')
            {
                Console.WriteLine("You accept the offer and rest in the tavern.");
                Console.WriteLine("You recover to full health.");
                player.Health = 100;
            }
            else
            {
                Console.WriteLine("You decline the offer and continue on your way.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void CreateEnemy()
        {
            Enemy enemy = new Enemy();
            enemy = new Enemy
            {
                Type = Models.Enums.EnemyType.Goblin,
                Name = "Goblin",
                Health = 50,
                Attack = 10,
                Defence = 5
            };

        }
    }
}