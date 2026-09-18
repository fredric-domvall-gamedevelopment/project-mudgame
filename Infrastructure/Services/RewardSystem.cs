using Infrastructure.Services;
using Infrastructure.Models;

namespace Infrastructure.Services;
public class RewardSystem()
{

    public ResultResponse<Reward> BattleRewards(Enemy enemy, Player player, List<string> rewardInformation)
    {
        rewardInformation.Add($"\nYou gained {enemy.Reward.Xp * 10} XP and {enemy.Reward.Gold * 5} Gold!\n");

        player.CurrentXp += enemy.Reward.Xp * 10;
        player.Gold += enemy.Reward.Gold * 5;

        while (player.CurrentXp >= player.NextLevelXp)
        {
            player.Level++;
            player.CurrentXp -= player.NextLevelXp;
            player.NextLevelXp = player.Level * 100;
            player.SkillPoints += 10;

            rewardInformation.Add($"\nCongratulations! You have leveled up to level {player.Level}!");
            rewardInformation.Add($"\nYouve earned {player.SkillPoints} Skillpoints.\n");

            return new ResultResponse<Reward> { IsSuccess = true, Information = rewardInformation };

        }

        return new ResultResponse<Reward> { IsSuccess = true, Information = rewardInformation };

    }
}
