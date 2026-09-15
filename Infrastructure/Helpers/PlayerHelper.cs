using Infrastructure.Models;

namespace Infrastructure.Helpers;

public static class PlayerHelper
{
    public static bool PlayerIsDead(Player player)
    {
        if (player.Health <= 0)
            player.IsDead = true;
        
        return player.IsDead;
    }
}
