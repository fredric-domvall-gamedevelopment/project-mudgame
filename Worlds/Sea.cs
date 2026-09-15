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
        FightingSystem fightingSystem = new FightingSystem();

        fightingSystem.Battle(enemyCreator.CreateRandomEnemy(player), player, art);
    }
}