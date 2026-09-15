using Infrastructure.Models;

namespace Infrastructure.Services;

public class CharacterStatsCalculator
{
    public float CalculateCharacterAttack(Character character)
    {
        character.Attack = character.Stats.Strength * 2;
        return character.Attack;
    }

    public float ClalculateCharacterDefense(Character character)
    {
        character.Defense = character.Stats.Dexterity * 2;
        return character.Defense;
    }

    public float CalculateCharacterHealth(Character character)
    {
        character.MaxHealth += character.Stats.Endurance * 10;
        return character.MaxHealth;
    }
}
