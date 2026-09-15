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
                battleInformation.Add($"\n\nYou attack the {enemy.Name} for {playerDamage} damage!\n");

                if(enemy.Health <= 0)
                {
                    battleInformation.Add($"\nYou have defeated the {enemy.Name}!");
                    battleResult = BattleResult.EnemyDead;
                    return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = battleInformation };
                }

                float enemyDamage = Math.Max(enemy.Attack - player.Defense, 0);
                player.Health -= enemyDamage;
                battleInformation.Add($"\nThe {enemy.Name} attacks you for {enemyDamage} damage!\n");

                if(player.Health <= 0)
                {
                    battleInformation.Add("\nYou have been defeated!\n");
                    battleResult = BattleResult.PlayerDead;
                    return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = battleInformation };
                }

                battleInformation.Add("\nThe battle continues...\n");
                battleResult = BattleResult.Continue;

                return new ResultResponse<BattleResult> { IsSuccess = true, Information = battleInformation };

            case BattleAction.UseItem:
                battleInformation.Add("\nYou use an item.\n");
                return new ResultResponse<BattleResult> { IsSuccess = true, Information = battleInformation };

            case BattleAction.Flee:
                battleInformation.Add("\nYou run away from the battle.\n");
                return new ResultResponse<BattleResult> { IsSuccess = true, Information = battleInformation };

            default:
                throw new ArgumentOutOfRangeException(nameof(battleAction), battleAction, null);

        }

        //if (enemy.Health <= 0)
        //{
        //    battleInformation.Add($"\nYou have defeated the {enemy.Name}!");
        //    battleInformation.Add($"\nYou gained {enemy.Reward.Xp * 10} XP and {enemy.Reward.Gold * 5} Gold!\n");

        //    player.CurrentXp += enemy.Reward.Xp * 10;
        //    player.Gold += enemy.Reward.Gold * 5;

        //    if (player.CurrentXp >= player.NextLevelXp)
        //    {
        //        player.Level++;
        //        player.CurrentXp -= player.NextLevelXp;
        //        player.NextLevelXp = player.Level * 100;
        //        player.SkillPoints += 10;

        //        battleInformation.Add($"\nCongratulations! You have leveled up to level {player.Level}!");
        //        battleInformation.Add($"\nYouve earned 10 Skillpoints.\n");

        //        playerCreator.SetSkillPoints(player);

        //        Console.WriteLine("Player stats upgraded! \n");

        //        ConsoleHelper.Continue();
        //    }

        //    return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = battleInformation };
        //}
        //else if (player.Health <= 0)
        //{
        //    Console.WriteLine("You have been defeated!\n");

        //    player.IsDead = true;
        //}
    }
}