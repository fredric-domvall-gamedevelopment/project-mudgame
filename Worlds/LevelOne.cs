using MUD.Models;
using MUD.Services;

namespace MUD.Worlds
{
    public class LevelOne
    {
        CharacterStatsCalculator calculator = new CharacterStatsCalculator();
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
            player.Stats.Strength = 0f;
            player.Stats.Dexterity = 0f;
            player.Stats.Endurance = 0f;
            player.Attack = 0f;
            player.Defense = 0f;
            player.MaxHealth = 50f;
            player.IsDead = false;          

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

            do
            {
                Console.WriteLine("-----Set your Attack & Defence stats-----\n");
                Console.WriteLine($"You have {SkillPoints} points to spend\n");
                Console.WriteLine($"1. Increase Strength  | Current STR: {player.Stats.Strength}");
                Console.WriteLine($"2. Increase Dexterity | Current DEX: {player.Stats.Dexterity}");
                Console.WriteLine($"3. Increase Endurance | Current END: {player.Stats.Endurance}");

                char choice = Console.ReadKey().KeyChar;
                Console.Clear();

                switch (choice)
                {
                    case '1':
                        player.Stats.Strength++;
                        SkillPoints--; 
                        break;
                    case '2':
                        player.Stats.Dexterity++;
                        SkillPoints--;
                        break;
                    case '3':
                        player.Stats.Endurance++;
                        SkillPoints--;
                        break;
                    default:
                        
                        Console.WriteLine("Invalid choice, please try again.\n");
                        break;
                }
                if(SkillPoints == 0)
                {
                    player.Attack = calculator.CalculateCharacterAttack(player);
                    player.Defense = calculator.ClalculateCharacterDefense(player);
                    player.MaxHealth = calculator.CalculateCharacterHealth(player);
                    player.Health = player.MaxHealth;

                    Console.WriteLine($"Character created! \n" +
                        $"Name: [{player.Name}] \n" +
                        $"STR: {player.Stats.Strength} || DEX: {player.Stats.Dexterity} || END: {player.Stats.Endurance}\n" +
                        $"HP: {player.Health}/{player.MaxHealth} || Attack: {player.Attack} || Defense: {player.Defense}");
                    Console.WriteLine("Are you happy with your character? (press n to restart, any other key to continue)");

                    choice = Console.ReadKey().KeyChar;
                    Console.Clear();

                    if (choice == 'N' || choice == 'n')
                        StartGame();
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
            Console.WriteLine($"Player: {player.Name} | HP: {player.Health}/{player.MaxHealth} | Attack: {player.Attack} | Defense: {player.Defense}\n ");
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
                    GoToTavern(player);
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
            sea.EnterSea(player);
        }

        private void GoToMountains()
        {
            Mountains mountains = new Mountains();
            mountains.EnterMountains(player);
        }

        private void GoToTavern(Player player)
        {
            Town town = new Town();
            town.EnterTown(player);
        }

    }
}