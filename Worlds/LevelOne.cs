using MUD.Art;
using MUD.Models;

namespace MUD.Worlds
{
    public class LevelOne
    {
        Player player = new Player();


        public void StartGame()
        {
            Console.WriteLine("Welcome the magical worlds of MUDs \n");

            CreatePlayer();

            PlayGame();
        }

        private void CreatePlayer()
        {
            int SkillPoints = 20;
            player.Attack = 0f;
            player.Defense = 0f;

            Console.WriteLine("-----Create your character-----\n");
            Console.WriteLine("What is your name?");

            player.Name = Console.ReadLine()!;
            if(String.IsNullOrEmpty(player.Name))
            {
                while (String.IsNullOrEmpty(player.Name))
                {
                    Console.WriteLine("Please enter a valid name");
                    player.Name = Console.ReadLine()!;
                }
            }
            Console.Clear();

            player.MaxHealth = 100f;
            player.Health = player.MaxHealth;
            player.IsDead = false;

            do
            {
                Console.WriteLine("-----Set your Attack & Defence stats-----\n");
                Console.WriteLine($"You have {SkillPoints} points to spend\n");
                Console.WriteLine($"1. Increase Attack  | Current Attack: {player.Attack}");
                Console.WriteLine($"2. Increase Defence | Current Defence: {player.Defense}");

                char choice = Console.ReadKey().KeyChar;
                Console.Clear();

                switch (choice)
                {
                    case '1':
                        player.Attack++;
                        SkillPoints--; 
                        break;
                    case '2':
                        player.Defense++;
                        SkillPoints--;
                        break;
                    default:
                        
                        Console.WriteLine("Invalid choice, please try again.\n");
                        break;
                }
            } while (SkillPoints > 0);

        }

        private void PlayGame()
        {
            if(player.IsDead)
            {
                Console.WriteLine("You are dead, game over!");
                return;
            }
            Console.WriteLine($"Player: {player.Name} | HP: {player.Health}");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Go to the Sea");
            Console.WriteLine("2. Go to the Mountains");
            Console.WriteLine("3. Go to the Tavern");

            Char choice = Console.ReadKey().KeyChar;
            Console.Clear();

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
            Enemy enemy = CreateEnemy(new Enemy());
            Sea sea = new Sea();
            sea.ShowGraphic();
            Battle(CreateEnemy(new Enemy()), player);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void GoToMountains()
        {
            Mountains mountain = new Mountains();
            mountain.ShowGraphic();
            Battle(CreateEnemy(new Enemy()), player);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void GoToTavern()
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

        private Enemy CreateEnemy(Enemy enemy)
        {
             
            enemy = new Enemy
            {
                Type = Models.Enums.EnemyType.Goblin,
                Name = "Goblin",
                Health = 50f,
                Attack = 10f,
                Defense = 5f
            };

            return enemy;
        }

        private void Battle(Enemy enemy, Player player)
        {
            do
            {
                Console.WriteLine($"Player: {player.Name} | HP: {player.Health}");
                Console.WriteLine($"Enemy: {enemy.Name} | HP: {enemy.Health}");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Run away");

                char choice = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (choice)
                {
                    case '1':
                        enemy.Health -= player.Attack - enemy.Defense;
                        Console.WriteLine($"You attack the {enemy.Name} for {player.Attack - enemy.Defense} damage!");
                        player.Health -= enemy.Attack - player.Defense;
                        Console.WriteLine($"The {enemy.Name} attacks you for {enemy.Attack - player.Defense} damage!");
                        break;
                    case '2':
                        Console.WriteLine("You run away from the battle.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            } while (enemy.Health > 0 && player.Health > 0);

            if(enemy.Health <= 0)
            {
                Console.WriteLine($"You have defeated the {enemy.Name}!");
            }
            else if (player.Health <= 0)
            {
                Console.WriteLine("You have been defeated!");
                player.IsDead = true;
            }
        }
    }
}