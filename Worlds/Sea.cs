using MUD.Models;
using MUD.Services;

namespace MUD.Worlds;
public class Sea
{
    public void EnterSea(Player player)
    {
        string art = "Sea";

        EnemyCreator enemyCreator = new EnemyCreator();
        BattleSystem battleSystem = new BattleSystem();

        battleSystem.Battle(enemyCreator.CreateRandomEnemy(player), player, art);
    }
}