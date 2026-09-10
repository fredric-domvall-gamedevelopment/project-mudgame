using MUD.Models.Enums;

namespace MUD.Models;
public class Enemy
{
    public EnemyType Type { get; set; }
    public string Name { get; set; } = String.Empty;
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defence { get; set; }

}
