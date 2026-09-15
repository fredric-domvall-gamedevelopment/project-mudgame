using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Services;
using MUD.Art;

namespace MUD.Worlds;
public class Mountains
{
    MountainsArt mountainsArt = new MountainsArt();
    public void EnterMountains(Player player)
    {
        string art = mountainsArt.ShowGraphic();
        string artType = "Mountains";

        EnemyCreator enemyCreator = new EnemyCreator();
        BattleSystem battleSystem = new BattleSystem();

        battleSystem.Battle(enemyCreator.CreateRandomEnemy(player), player, art, artType);

        ConsoleHelper.Continue();
    }
}
