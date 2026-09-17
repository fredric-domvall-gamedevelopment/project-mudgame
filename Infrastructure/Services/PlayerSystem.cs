using Infrastructure.Models;

namespace Infrastructure.Services;

public class PlayerSystem
{
    private readonly List<string> playerInformation = new List<string>();
    CharacterStatsCalculator calculator = new CharacterStatsCalculator();

    public ResultResponse<Player> CreatePlayer(Player player)
    {
        playerInformation.Clear();

        player.SkillPoints = 20;
        player.Level = 1;
        player.NextLevelXp = 100;
        player.Stats.Strength = 0f;
        player.Stats.Dexterity = 0f;
        player.Stats.Endurance = 0f;
        player.Attack = 0f;
        player.Defense = 0f;
        player.MaxHealth = 50f;

        if (string.IsNullOrEmpty(player.Name) || string.IsNullOrWhiteSpace(player.Name))
        {
            playerInformation.Add("Player name cannot be empty. Please enter a valid name.");
            return new ResultResponse<Player> { IsSuccess = false, Data = player, Information = playerInformation };
        }

        return new ResultResponse<Player> { IsSuccess = true, Data = player, Information = playerInformation };
    }

    public ResultResponse<Player> SetSkillPoints(Player player, char choice)
    {
        playerInformation.Clear();

        player.Attack = calculator.CalculateCharacterAttack(player);
        player.Defense = calculator.ClalculateCharacterDefense(player);
        player.MaxHealth = calculator.CalculateCharacterHealth(player);
        player.Health = player.MaxHealth;

        switch (choice)
        {
            case '1':
                player.Stats.Strength++;
                player.SkillPoints--;
                return new ResultResponse<Player> { IsSuccess = true, Data = player, Information = null };

            case '2':
                player.Stats.Dexterity++;
                player.SkillPoints--;
                return new ResultResponse<Player> { IsSuccess = true, Data = player, Information = null };

            case '3':
                player.Stats.Endurance++;
                player.SkillPoints--;
                return new ResultResponse<Player> { IsSuccess = true, Data = player, Information = null };

            default:
                playerInformation.Add("Invalid choice. Please select a valid option.");
                return new ResultResponse<Player> { IsSuccess = false, Data = player, Information = playerInformation };
        }

    }
}
