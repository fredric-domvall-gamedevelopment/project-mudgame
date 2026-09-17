using Infrastructure.Enums;
using Infrastructure.Models;
using System.Reflection.Metadata.Ecma335;

namespace Infrastructure.Helpers;

public static class PlayerHelper
{
    public static ResultResponse<BattleResult> IsPlayerDead(Player player)
    {
        if (player.Health <= 0)
        {
            player.IsDead = true;

            BattleResult battleResult = BattleResult.PlayerDead;

            string message = "You have been defeated!";

            return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = new List<string> { message } };
        }
        
        return new ResultResponse<BattleResult> { IsSuccess = false };
    }
}
