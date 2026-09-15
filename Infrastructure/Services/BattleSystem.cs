using Infrastructure.Enums;
using Infrastructure.Helpers;
using Infrastructure.Models;
using System.Runtime.CompilerServices;

namespace Infrastructure.Services;

public class BattleSystem
{
    private readonly List<string> battleInformation = new List<string>();
    BattleResult battleResult = new BattleResult();
    public ResultResponse<BattleResult> Battle(BattleAction battleAction, Enemy enemy, Player player)
    {
        battleInformation.Clear();

        switch (battleAction)
        {
            case BattleAction.Attack:
                float playerDamage = Math.Max(player.Attack - enemy.Defense, 0);
                enemy.Health -= playerDamage;
                battleInformation.Add($"You attack the {enemy.Name} for {playerDamage} damage!");

                if (enemy.Health <= 0)
                {
                    battleInformation.Add($"You have defeated the {enemy.Name}!");
                    battleResult = BattleResult.EnemyDead;

                    BattleRewards(enemy, player);

                    return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = battleInformation };
                }

                float enemyDamage = Math.Max(enemy.Attack - player.Defense, 0);
                player.Health -= enemyDamage;
                battleInformation.Add($"The {enemy.Name} attacks you for {enemyDamage} damage!");

                if (player.Health <= 0)
                {
                    battleInformation.Add("You have been defeated!");
                    battleResult = BattleResult.PlayerDead;
                    player.IsDead = true;

                    return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = battleInformation };
                }

                battleInformation.Add("The battle continues...");
                battleResult = BattleResult.Continue;

                return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = battleInformation };

            case BattleAction.UseItem:
                battleInformation.Add("You use an item.");

                enemyDamage = Math.Max(enemy.Attack - player.Defense, 0);
                player.Health -= enemyDamage;
                battleInformation.Add($"The {enemy.Name} attacks you for {enemyDamage} damage!");
                battleResult = BattleResult.Continue;

                return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = battleInformation };

            case BattleAction.Flee:
                int escapeAttempt = new Random().Next(1, 4);

                if (escapeAttempt == 1)
                {
                    battleInformation.Add("You successfully escaped from the battle.");
                    battleResult = BattleResult.Flee;

                    return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = battleInformation };
                }

                battleInformation.Add("You failed to escape from the battle.");

                enemyDamage = Math.Max(enemy.Attack - player.Defense, 0);
                player.Health -= enemyDamage;
                battleInformation.Add($"The {enemy.Name} attacks you for {enemyDamage} damage!");
                battleResult = BattleResult.Continue;

                return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = battleInformation };

            default:
                throw new ArgumentOutOfRangeException(nameof(battleAction), battleAction, null);
        }
    }

    private void BattleRewards(Enemy enemy, Player player)
    {
        battleInformation.Add($"\nYou gained {enemy.Reward.Xp * 10} XP and {enemy.Reward.Gold * 5} Gold!\n");
        player.CurrentXp += enemy.Reward.Xp * 10;
        player.Gold += enemy.Reward.Gold * 5;
        if (player.CurrentXp >= player.NextLevelXp)
        {
            player.Level++;
            player.CurrentXp -= player.NextLevelXp;
            player.NextLevelXp = player.Level * 100;
            player.SkillPoints += 10;
            battleInformation.Add($"\nCongratulations! You have leveled up to level {player.Level}!");
            battleInformation.Add($"\nYouve earned 10 Skillpoints.\n");
            //playerCreator.SetSkillPoints(player);
            Console.WriteLine("Player stats upgraded! \n");
            ConsoleHelper.Continue();
        }
    }
}