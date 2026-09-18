using Infrastructure.Configurations;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class PlayerSystem(JsonFileRepository<Player> jsonFileRepository, FileSources fileSources)
{
    private readonly List<string> _playerInformation = new List<string>();
    private readonly List<Player> _savedPlayers = new List<Player>();
    private readonly JsonFileRepository<Player> _jsonFileRepository = jsonFileRepository;
    private readonly FileSources _fileSources = fileSources;
    CharacterStatsCalculator calculator = new CharacterStatsCalculator();

    public ResultResponse<Player> CreatePlayer(Player player)
    {
        _playerInformation.Clear();

        player.PlayerId = Guid.NewGuid();
        player.SkillPoints = 20;
        player.Level = 1;
        player.NextLevelXp = 100;
        player.Stats.Strength = 0f;
        player.Stats.Dexterity = 0f;
        player.Stats.Endurance = 0f;
        player.Attack = 0f;
        player.Defense = 0f;
        player.MaxHealth = 50f;
        player.IsDead = false;

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

        player.ArmorRating = Math.Min(player.Stats.Endurance / 50f, 0.9f);

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

    public async Task<ResultResponse<Player>> SavePlayer(Player player)
    {
        _savedPlayers.Clear();

        var result = await _jsonFileRepository.ReadFromJsonAsync(_fileSources.PlayersFileSource);

        if (result.Data is not null)
            _savedPlayers.AddRange(result.Data);

        Player? savedPlayer = _savedPlayers.FirstOrDefault(p => p.PlayerId == player.PlayerId);

        if (savedPlayer is not null)
            _savedPlayers.Remove(savedPlayer);

        _savedPlayers.Add(player);

        await _jsonFileRepository.WriteToJsonAsync(_fileSources.PlayersFileSource, _savedPlayers);

        return new ResultResponse<Player> { IsSuccess = true, Data = player, Information = _playerInformation };

    }

    public async Task<ResultResponse<Player>> LoadPlayerByPlayerId(Guid playerId, Player player)
    {
        await GetPlayersFromList();

        if (_savedPlayers.FirstOrDefault(p => p.PlayerId == playerId) is Player foundPlayer)
        {
            player.PlayerId = foundPlayer.PlayerId;
            player.Name = foundPlayer.Name;
            player.Health = foundPlayer.Health;
            player.MaxHealth = foundPlayer.MaxHealth;
            player.Attack = foundPlayer.Attack;
            player.Defense = foundPlayer.Defense;
            player.Level = foundPlayer.Level;
            player.CurrentXp = foundPlayer.CurrentXp;
            player.NextLevelXp = foundPlayer.NextLevelXp;
            player.Gold = foundPlayer.Gold;
            player.SkillPoints = foundPlayer.SkillPoints;
            player.Score = foundPlayer.Score;
            player.Stats.Strength = foundPlayer.Stats.Strength;
            player.Stats.Dexterity = foundPlayer.Stats.Dexterity;
            player.Stats.Endurance = foundPlayer.Stats.Endurance;

            return new ResultResponse<Player> { IsSuccess = true, Data = player, Information = new List<string> { "Player loaded successfully." } };
        }
        else
            return new ResultResponse<Player> { IsSuccess = false, Data = player, Information = new List<string> { "Player not found." } };
    }

    public async Task<ResultResponse<List<Player>>> GetPlayersFromList()
    {
        _savedPlayers.Clear();

        var result = await _jsonFileRepository.ReadFromJsonAsync(_fileSources.PlayersFileSource);

        if (result.Data is not null)
            _savedPlayers.AddRange(result.Data);

        return new ResultResponse<List<Player>> { IsSuccess = result.IsSuccess, Data = _savedPlayers, Information = result.Information };
    }
}
