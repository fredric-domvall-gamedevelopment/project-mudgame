using Infrastructure.Models;
using Infrastructure.Services;
using MUD.Art;

namespace MUD.Worlds;
public class Sea
{
    SeaArt seaArt = new SeaArt();
    public void EnterSea(Player player)
    {
        string art = seaArt.ShowGraphic();
        string artType = "Sea";

        EnemyCreator enemyCreator = new EnemyCreator();
        BattleSystem battleSystem = new BattleSystem();

        battleSystem.Battle(enemyCreator.CreateRandomEnemy(player), player, art, artType);
    }
}