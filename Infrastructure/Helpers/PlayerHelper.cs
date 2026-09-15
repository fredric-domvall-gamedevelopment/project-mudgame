using Infrastructure.Models;

namespace Infrastructure.Helpers;

public static class PlayerHelper
{
    public static ResultResponse<Player> IsPlayerDead(Player player)
    {
        string message = "You have been defeated!";

        if (player.Health <= 0)
            player.IsDead = true;
        
        return new ResultResponse<Player> { IsSuccess = true, Data = player, Information = new List<string> { message } };
    }
}
