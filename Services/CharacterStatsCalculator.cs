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

    public float CalculateCharacterHealth(Character character)
    {
        character.MaxHealth += character.Stats.Endurance * 5;
        return character.MaxHealth;
    }
}
