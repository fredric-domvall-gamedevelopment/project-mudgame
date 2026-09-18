using Infrastructure.Enums;
using Infrastructure.Helpers;
using Infrastructure.Models;


namespace Infrastructure.Services;

public class BattleSystem
{
    public readonly List<string> battleInformation = new List<string>();
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

                if(enemy.Health <= 0)
                {
                    battleInformation.Add($"You have defeated the {enemy.Name}!");
                    battleResult = BattleResult.EnemyDead;

                    return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = battleInformation };
                }

                float enemyDamage = Math.Max(enemy.Attack - player.Defense, 0);
                player.Health -= enemyDamage;
                battleInformation.Add($"The {enemy.Name} attacks you for {enemyDamage} damage!");

                var isPlayerDead = PlayerHelper.IsPlayerDead(player);

                if (isPlayerDead.IsSuccess)
                    return new ResultResponse<BattleResult> { IsSuccess = true, Data = isPlayerDead.Data, Information = isPlayerDead.Information };

                battleInformation.Add("The battle continues...");
                battleResult = BattleResult.Continue;

                return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = battleInformation };

            case BattleAction.UseItem:
                battleInformation.Add("You use an item.");

                enemyDamage = Math.Max(enemy.Attack - player.Defense, 0);
                player.Health -= enemyDamage;
                battleInformation.Add($"The {enemy.Name} attacks you for {enemyDamage} damage!");

                isPlayerDead = PlayerHelper.IsPlayerDead(player);

                if (isPlayerDead.IsSuccess)
                    return new ResultResponse<BattleResult> { IsSuccess = true, Data = isPlayerDead.Data, Information = isPlayerDead.Information };

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

                isPlayerDead = PlayerHelper.IsPlayerDead(player);

                if (isPlayerDead.IsSuccess)
                    return new ResultResponse<BattleResult> { IsSuccess = true, Data = isPlayerDead.Data, Information = isPlayerDead.Information };

                battleResult = BattleResult.Continue;

                return new ResultResponse<BattleResult> { IsSuccess = true, Data = battleResult, Information = battleInformation };

            default:
                throw new ArgumentOutOfRangeException(nameof(battleAction), battleAction, null);
        }
    }
}