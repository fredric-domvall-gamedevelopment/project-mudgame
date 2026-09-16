using Infrastructure.Configurations;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;
public class ScoreSystem
{
    private readonly List<Player> _playerHighscore = new List<Player>();
    private readonly JsonFileRepository<Player> _jsonFileRepository;
    private readonly string _fileSources;

    public ScoreSystem(JsonFileRepository<Player> jsonFileRepository, FileSources fileSources)
    {
        _jsonFileRepository = jsonFileRepository;
        _fileSources = fileSources.HighscoreFileSource;
    }
    public void CalculateHighscore(Player player)
    {
        player.Score = (int)(player.Stats.Strength + player.Stats.Dexterity + player.Stats.Endurance);
    }

    public async Task<Player>AddPlayerToHighscoreList(Player player)
    {
        CalculateHighscore(player);

        _playerHighscore.Add(player);
        await _jsonFileRepository.WriteToJsonAsync(_fileSources, _playerHighscore);
        return player;
    }

    public List<Player> GetHighscoreList()
    {
        return _playerHighscore;
    }
}
