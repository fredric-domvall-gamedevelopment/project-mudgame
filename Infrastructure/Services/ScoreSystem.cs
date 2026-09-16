using Infrastructure.Models;

namespace Infrastructure.Services;
public class ScoreSystem
{
    private readonly List<Player> _playerHighscore = new List<Player>();

    public void CalculateHighscore(Player player)
    {
        player.Score = (int)(player.Stats.Strength + player.Stats.Dexterity + player.Stats.Endurance);
    }

    public void AddPlayerToHighscoreList(Player player)
    {
        CalculateHighscore(player);

        _playerHighscore.Add(player);
    }

    public List<Player> GetHighscoreList()
    {
        return _playerHighscore;
    }
}
