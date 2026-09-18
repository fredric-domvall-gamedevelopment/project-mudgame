using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class PlayerSystem
{
    private readonly List<string> _playerInformation = new List<string>();
    private readonly JsonFileRepository<Player> _jsonFileRepository;
    private readonly string _fileSources;
    CharacterStatsCalculator calculator = new CharacterStatsCalculator();

    public PlayerSystem(JsonFileRepository<Player> jsonFileRepository, string fileSources)
    {
        _jsonFileRepository = jsonFileRepository;
        _fileSources = fileSources;
    }

    public ResultResponse<Player> CreatePlayer(Player player)
    {
        _playerInformation.Clear();

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
            _playerInformation.Add("Player name cannot be empty. Please enter a valid name.");
            return new ResultResponse<Player> { IsSuccess = false, Data = player, Information = _playerInformation };
        }

        return new ResultResponse<Player> { IsSuccess = true, Data = player, Information = _playerInformation };
    }

    public ResultResponse<Player> SetSkillPoints(Player player, char choice)
    {
        _playerInformation.Clear();

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
                _playerInformation.Add("Invalid choice. Please select a valid option.");
                return new ResultResponse<Player> { IsSuccess = false, Data = player, Information = _playerInformation };
        }

    }
}
