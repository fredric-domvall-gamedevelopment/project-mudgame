using Infrastructure.Configurations;
using Infrastructure.Models;
using Infrastructure.Repositories;
using System.Runtime.CompilerServices;

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

    public async Task<ResultResponse<Player>> SavePlayer(Player player)
    {
        _savedPlayers.Clear();

        var result = await _jsonFileRepository.ReadFromJsonAsync(_fileSources.PlayersFileSource);

        if (result.Data != null)
            _savedPlayers.AddRange(result.Data);

        if (_savedPlayers.FirstOrDefault(p => p.PlayerId == player.PlayerId) is not null)
        {
            _savedPlayers.RemoveAll(p => p.PlayerId == player.PlayerId);
            _savedPlayers.Add(player);
        }
        else
            _savedPlayers.Add(player);

        result = await _jsonFileRepository.WriteToJsonAsync(_fileSources.PlayersFileSource, _savedPlayers);
        if(!result.IsSuccess && result.Information is not null)
        {
            _playerInformation.AddRange(result.Information);
            return new ResultResponse<Player> { IsSuccess = false, Data = player, Information = _playerInformation };
        }

        _playerInformation.Add("Player saved successfully.");

        return new ResultResponse<Player> { IsSuccess = true, Data = player, Information = _playerInformation };

    }

    public async Task<ResultResponse<Player>> LoadPlayerByPlayerId(Guid playerId)
    {
         Player player = new Player();

         await GetPlayersFromList();
        
        if(_savedPlayers.FirstOrDefault(p => p.PlayerId == playerId) is Player foundPlayer)
        {
            player = foundPlayer;
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
