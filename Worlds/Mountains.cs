using MUD.Art;
using MUD.Helpers;
using MUD.Models;
using MUD.Services;

namespace MUD.Worlds;
public class Mountains
{
    public void EnterMountains(Player player)
    {
        MountainsArt mountains = new MountainsArt();
        mountains.ShowGraphic();

        EnemyCreator enemyCreator = new EnemyCreator();
        FightingSystem fightingSystem = new FightingSystem();

        fightingSystem.Battle(enemyCreator.CreateRandomEnemy(player), player);

        ConsoleHelper.Continue();
    }
}
