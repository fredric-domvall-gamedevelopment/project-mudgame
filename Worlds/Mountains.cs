using MUD.Helpers;
using MUD.Models;
using MUD.Services;

namespace MUD.Worlds;
public class Mountains
{
    public void EnterMountains(Player player)
    {
        string art = "Mountains";

        EnemyCreator enemyCreator = new EnemyCreator();
        BattleSystem battleSystem = new BattleSystem();

        battleSystem.Battle(enemyCreator.CreateRandomEnemy(player), player, art);

        ConsoleHelper.Continue();
    }
}
