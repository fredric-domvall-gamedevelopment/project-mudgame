using MUD.Art;
using MUD.Helpers;
using MUD.Models;
using MUD.Models.Enums;
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