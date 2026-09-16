using Infrastructure.Models;

namespace Infrastructure.Services;
public class HighscoreCalculator
{
    public Player CalculateHighscore(Player player)
    {
        player.Score = (int) (player.Stats.Strength + player.Stats.Dexterity + player.Stats.Endurance);

        return player;
    }
}
