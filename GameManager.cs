using MUD.Worlds;

namespace MUD;
public class GameManager
{
    public void StartMenu()
    {
        GameWorld levelOne = new GameWorld();
        levelOne.StartGame();
    }
}
