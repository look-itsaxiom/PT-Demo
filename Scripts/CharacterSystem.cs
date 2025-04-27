using Godot;
using System.Collections.Generic;
using CharacterData;
using System.Linq;
using System;

public partial class CharacterSystem : Node
{
    public static Dictionary<int, PackedScene> CharacterModelDictionary = new Dictionary<int, PackedScene> {
        { 1, GD.Load<PackedScene>("res://Scenes/rogue.tscn") },
        { 2, GD.Load<PackedScene>("res://Scenes/barbarian.tscn") },
        { 3, GD.Load<PackedScene>("res://Scenes/wizard.tscn") },
        { 4, GD.Load<PackedScene>("res://Scenes/knight.tscn") },
    };
    public static Character GenerateRandomCharacter()
    {
        var raceKeys = DataRegistry.Instance.Races.Keys.ToList();
        var randomRaceKey = raceKeys[GD.RandRange(0, raceKeys.Count - 1)];
        var race = DataRegistry.Instance.Races[randomRaceKey];

        var character = new Character
        {
            CharacterName = "Jerry",
            Race = race,
            Class = DataRegistry.Instance.Classes["Adventurer"],

        };

        foreach (Stat.StatKey stat in Enum.GetValues(typeof(Stat.StatKey)))
        {
            var min = race.BaseStats[stat].X;
            var max = race.BaseStats[stat].Y;
            character.BaseStats[stat].Value = GD.RandRange(min, max);
        }

        foreach (var stat in character.BaseStats.Keys)
        {
            character.GrowthRates[stat] = RollGrowthRate();
        }

        // Apply growth rate modifiers to calc current stats
        GD.Print(character.CurrentStats);
        character.CalculateCurrentStats();
        GD.Print(character.CurrentStats);

        character.CharacterModel = CharacterModelDictionary[GD.RandRange(1, CharacterModelDictionary.Count)];

        return character;
    }

    public static GrowthRate RollGrowthRate()
    {
        float roll = (float)GD.Randf();
        float cumulativeProbability = 0f;

        foreach (var growthRate in DataRegistry.Instance.GrowthRates.Values)
        {
            cumulativeProbability += growthRate.GrowthRateChance;
            if (roll < cumulativeProbability)
            {
                return growthRate;
            }
        }

        // Fallback in case of rounding errors
        return DataRegistry.Instance.GrowthRates[GrowthRate.GrowthRateKey.Minimal];
    }

}

