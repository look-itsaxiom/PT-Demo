using Godot;
using System.Collections.Generic;
using Characters;
using System.Linq;

public partial class CharacterSystem : Node
{
    public static Dictionary<string, RaceData> raceDefs;
    public static Dictionary<string, ClassData> classDefs;
    public static Dictionary<GrowthRate.GrowthRateKey, GrowthRate> growthRates = new();

    public override void _Ready()
    {
        raceDefs = DataRegistry.Instance.Races;
        classDefs = DataRegistry.Instance.Classes;
        growthRates = DataRegistry.Instance.GrowthRates;
    }

    public static CharacterData GenerateRandomCharacter()
    {
        var raceKeys = raceDefs.Keys.ToList();
        var randomRaceKey = raceKeys[GD.RandRange(0, raceKeys.Count - 1)];
        var race = raceDefs[randomRaceKey];

        var character = new CharacterData
        {
            CharacterName = "Jerry",
            Race = race,
            ClassName = classDefs["Adventurer"]
        };

        foreach (var stat in character.BaseStats.Keys)
        {
            var min = race.BaseStats[stat].Min;
            var max = race.BaseStats[stat].Max;
            character.BaseStats[stat].Value = GD.RandRange(min, max);
        }

        foreach (var stat in character.BaseStats.Keys)
        {
            character.GrowthRates[stat] = RollGrowthRate();
        }

        return character;
    }

    public static GrowthRate RollGrowthRate()
    {
        float roll = (float)GD.Randf();
        float cumulativeProbability = 0f;

        foreach (var growthRate in growthRates.Values)
        {
            cumulativeProbability += growthRate.GrowthRateChance;
            if (roll < cumulativeProbability)
            {
                return growthRate;
            }
        }

        // Fallback in case of rounding errors
        return growthRates[GrowthRate.GrowthRateKey.Minimal];
    }

}

