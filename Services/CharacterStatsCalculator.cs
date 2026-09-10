using MUD.Models;

namespace MUD.Services;

public class CharacterStatsCalculator
{
    public float CalculateCharacterAttack(Character character)
    {
        character.Attack = character.Stats.Strength;
        return character.Attack;
    }

    public float ClalculateCharacterDefense(Character character)
    {
        character.Defense = character.Stats.Dexterity;
        return character.Defense;
    }
}
