using MUD.Art;
using MUD.Models;
using MUD.Models.Enums;
using MUD.Services;

namespace MUD.Worlds;
public class Sea
{

    public void EnterSea(Player player)
    {
        SeaArt sea = new SeaArt();
        sea.ShowGraphic();

        EnemyCreator enemyCreator = new EnemyCreator();
        FightingSystem fightingSystem = new FightingSystem();

        fightingSystem.Battle(enemyCreator.CreateRandomEnemy(player), player);

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

}
