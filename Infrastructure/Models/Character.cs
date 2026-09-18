namespace Infrastructure.Models;

public class Character
{
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Attack { get; set; }
    public float Defense { get; set; }
    public bool IsDead { get; set; }
    public CharacterStats Stats { get; set; } = new CharacterStats();
}
