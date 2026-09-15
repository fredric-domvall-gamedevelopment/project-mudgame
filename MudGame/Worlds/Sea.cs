using Infrastructure.Enums;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Services;
using MUD.Art;

namespace MUD.Worlds;
public class Sea
{
    SeaArt seaArt = new SeaArt();
    BattleAction battleAction = new BattleAction();
    BattleSystem battleSystem = new BattleSystem();

    public void EnterSea(Player player)
    {
        EnemyCreator enemyCreator = new EnemyCreator();

        Enemy enemy = enemyCreator.CreateRandomEnemy(player);

        ToBattle(enemy, player);
    }

    private void ToBattle(Enemy enemy, Player player)
    {
        Console.WriteLine(seaArt.ShowGraphic());
        Console.WriteLine($"Player: {player.Name} | HP: {player.Health} | Attack: {player.Attack} | Defense: {player.Defense} | Level: {player.Level}");
        Console.WriteLine($"Enemy: {enemy.Name} | HP: {enemy.Health}  | Attack: {enemy.Attack} | Defense: {enemy.Defense} | Level: {enemy.Level}\n");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("1. Attack");
        Console.WriteLine("2. Use Item");
        Console.WriteLine("3. Run away");

        char choice = Console.ReadKey(true).KeyChar;

        switch (choice)
        {
            case '1':
                battleAction = BattleAction.Attack;
                break;

            case '2':
                battleAction = BattleAction.UseItem;
                break;

            case '3':
                battleAction = BattleAction.Flee;
                break;

            default:
                Console.WriteLine("Invalid choice. Please try again.");
                ToBattle(enemy, player);
                break;
        }

        var result = battleSystem.Battle(battleAction, enemy, player);

        if (result.IsSuccess)
        {
            if(result.Information != null)
                foreach (var info in result.Information)     
                    Console.WriteLine(info);

            ConsoleHelper.Continue();

            switch (result.Data)
            {
                case BattleResult.Continue:
                    ToBattle(enemy, player);
                    break;

                case BattleResult.EnemyDead:
                    break;

                case BattleResult.PlayerDead:
                    player.IsDead = true;
                    break;
            }
        }
    }
}