using MUD.Models;

namespace MUD.Services;

public class CharacterStatsCalculator
{
    public float CalculateCharacterAttack(Character character)
    {
        character.Attack = character.Stats.Strength;
        return character.Attack;
    }
}
