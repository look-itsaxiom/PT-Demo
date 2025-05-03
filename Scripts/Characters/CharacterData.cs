using System;
using Godot;
using System.Collections.Generic;

namespace CharacterData
{
    public partial class Stat : Resource
    {
        public enum StatKey
        {
            STR,
            END,
            DEF,
            INT,
            SPI,
            MDF,
            SPD,
            LCK,
            ACC
        }

        [Export] public StatKey Key;
        [Export] public string StatName;
        [Export] public string KeyName;
        [Export] public int Value = 0;
        [Export] public float Overflow = 0;
        [Export] public string Description;

        public static readonly Dictionary<StatKey, string> StatNamesDictionary = new()
        {
            { StatKey.STR, "Strength" },
            { StatKey.END, "Endurance" },
            { StatKey.DEF, "Defense" },
            { StatKey.INT, "Intelligence" },
            { StatKey.SPI, "Spirit" },
            { StatKey.MDF, "Magic Defense" },
            { StatKey.SPD, "Speed" },
            { StatKey.LCK, "Luck" },
            { StatKey.ACC, "Accuracy" }
        };

        public static readonly Dictionary<StatKey, string> StatKeyNames = new()
        {
            { StatKey.STR, "STR" },
            { StatKey.END, "END" },
            { StatKey.DEF, "DEF" },
            { StatKey.INT, "INT" },
            { StatKey.SPI, "SPI" },
            { StatKey.MDF, "MDF" },
            { StatKey.SPD, "SPD" },
            { StatKey.LCK, "LCK" },
            { StatKey.ACC, "ACC" }
        };

        public static readonly Dictionary<string, StatKey> StatKeyNamesReverse = new()
        {
            { "STR", StatKey.STR },
            { "END", StatKey.END },
            { "DEF", StatKey.DEF },
            { "INT", StatKey.INT },
            { "SPI", StatKey.SPI },
            { "MDF", StatKey.MDF },
            { "SPD", StatKey.SPD },
            { "LCK", StatKey.LCK },
            { "ACC", StatKey.ACC }
        };

        public Stat(StatKey key)
        {
            Key = key;
            StatName = StatNamesDictionary[key];
            KeyName = StatKeyNames[key];
        }
    }

    public partial class GrowthRate : Resource
    {
        [Export] public GrowthRateKey Key;
        [Export] public string GrowthRateName;
        [Export] public string GrowthRateSymbol;
        [Export] public float GrowthRateChance;
        [Export] public string Description;
        [Export] public string MetaDescription;
        public Func<int, int> CalculateGrowthRate;

        public enum GrowthRateKey
        {
            Exceptional = 4,
            Accelerated = 2,
            Gradual = 1,
            Normal = 0,
            Steady = -1,
            Minimal = -2
        }

        public static readonly Dictionary<GrowthRateKey, string> GrowthRateNames = new()
        {
            { GrowthRateKey.Exceptional, "Exceptional" },
            { GrowthRateKey.Accelerated, "Accelerated" },
            { GrowthRateKey.Gradual, "Gradual" },
            { GrowthRateKey.Normal, "Normal" },
            { GrowthRateKey.Steady, "Steady" },
            { GrowthRateKey.Minimal, "Minimal" }
        };

        public static readonly Dictionary<GrowthRateKey, string> GrowthRateSymbols = new()
        {
            { GrowthRateKey.Exceptional, "*" },
            { GrowthRateKey.Accelerated, "++" },
            { GrowthRateKey.Gradual, "+" },
            { GrowthRateKey.Normal, "•" },
            { GrowthRateKey.Steady, "-" },
            { GrowthRateKey.Minimal, "--" }
        };

        public static readonly Dictionary<GrowthRateKey, float> GrowthRateChances = new()
        {
            { GrowthRateKey.Exceptional, 0.05f },
            { GrowthRateKey.Accelerated, 0.1f },
            { GrowthRateKey.Gradual, 0.2f },
            { GrowthRateKey.Normal, 0.4f },
            { GrowthRateKey.Steady, 0.2f },
            { GrowthRateKey.Minimal, 0.05f }
        };

        public static readonly Dictionary<GrowthRateKey, string> GrowthRateDescriptions = new()
        {
            { GrowthRateKey.Exceptional, "Exceptional growth rate, very high chance of improvement." },
            { GrowthRateKey.Accelerated, "Accelerated growth rate, high chance of improvement." },
            { GrowthRateKey.Gradual, "Gradual growth rate, moderate chance of improvement." },
            { GrowthRateKey.Normal, "Normal growth rate, linear rate of improvement." },
            { GrowthRateKey.Steady, "Steady growth rate, low chance of improvement." },
            { GrowthRateKey.Minimal, "Minimal growth rate, very low chance of improvement." }
        };

        public static readonly Dictionary<GrowthRateKey, string> GrowthRateMetaDescriptions = new()
        {
            { GrowthRateKey.Exceptional, "+2 every level" },
            { GrowthRateKey.Accelerated, "+1 every level, bonus +1 every 2 levels" },
            { GrowthRateKey.Gradual, "+1 every level, bonus +1 every 3 levels" },
            { GrowthRateKey.Normal, "+1 every level" },
            { GrowthRateKey.Steady, "+2 every 3 levels" },
            { GrowthRateKey.Minimal, "+1 every 2 levels" }
        };

        public static readonly System.Collections.Generic.Dictionary<GrowthRateKey, Func<int, int>> GrowthRateCalculators = new()
        {
            { GrowthRateKey.Exceptional, CalculateExceptionalGrowth },
            { GrowthRateKey.Accelerated, CalculateAcceleratedGrowth },
            { GrowthRateKey.Gradual, CalculateGradualGrowth },
            { GrowthRateKey.Normal, CalculateNormalGrowth },
            { GrowthRateKey.Steady, CalculateSteadyGrowth },
            { GrowthRateKey.Minimal, CalculateMinimalGrowth }
        };

        public static int CalculateExceptionalGrowth(int level) => 2;
        public static int CalculateAcceleratedGrowth(int level) => level % 2 == 0 ? 2 : 1;
        public static int CalculateGradualGrowth(int level) => level % 3 == 0 ? 2 : 1;
        public static int CalculateNormalGrowth(int level) => 1;
        public static int CalculateSteadyGrowth(int level) => level % 3 == 0 ? 2 : 0;
        public static int CalculateMinimalGrowth(int level) => level % 2 == 0 ? 1 : 0;
    }

    [GlobalClass]
    public partial class Character : Resource
    {
        [Export] public string CharacterName;
        public Guid CharacterId = Guid.NewGuid();
        [Export] public RaceData Race;
        [Export] public ClassData Class;
        public Dictionary<Stat.StatKey, Stat> BaseStats = new() {
            { Stat.StatKey.STR, new Stat(Stat.StatKey.STR) },
            { Stat.StatKey.END, new Stat(Stat.StatKey.END) },
            { Stat.StatKey.DEF, new Stat(Stat.StatKey.DEF) },
            { Stat.StatKey.INT, new Stat(Stat.StatKey.INT) },
            { Stat.StatKey.SPI, new Stat(Stat.StatKey.SPI) },
            { Stat.StatKey.MDF, new Stat(Stat.StatKey.MDF) },
            { Stat.StatKey.SPD, new Stat(Stat.StatKey.SPD) },
            { Stat.StatKey.LCK, new Stat(Stat.StatKey.LCK) },
            { Stat.StatKey.ACC, new Stat(Stat.StatKey.ACC) }
        };
        public Dictionary<Stat.StatKey, GrowthRate> GrowthRates = new() {
            { Stat.StatKey.STR, new GrowthRate() },
            { Stat.StatKey.END, new GrowthRate() },
            { Stat.StatKey.DEF, new GrowthRate() },
            { Stat.StatKey.INT, new GrowthRate() },
            { Stat.StatKey.SPI, new GrowthRate() },
            { Stat.StatKey.MDF, new GrowthRate() },
            { Stat.StatKey.SPD, new GrowthRate() },
            { Stat.StatKey.LCK, new GrowthRate() },
            { Stat.StatKey.ACC, new GrowthRate() }
        };
        public Dictionary<Stat.StatKey, Stat> CurrentStats = new() {
            { Stat.StatKey.STR, new Stat(Stat.StatKey.STR) },
            { Stat.StatKey.END, new Stat(Stat.StatKey.END) },
            { Stat.StatKey.DEF, new Stat(Stat.StatKey.DEF) },
            { Stat.StatKey.INT, new Stat(Stat.StatKey.INT) },
            { Stat.StatKey.SPI, new Stat(Stat.StatKey.SPI) },
            { Stat.StatKey.MDF, new Stat(Stat.StatKey.MDF) },
            { Stat.StatKey.SPD, new Stat(Stat.StatKey.SPD) },
            { Stat.StatKey.LCK, new Stat(Stat.StatKey.LCK) },
            { Stat.StatKey.ACC, new Stat(Stat.StatKey.ACC) }
        };
        public List<Dictionary<Stat.StatKey, Stat>> StatHistory = new();
        [Export] public int Level = 1;
        [Export] public int Experience = 0;
        [Export] public string[] Abilities = new string[0];
        [Export] public string[] Affinities = new string[0];
        public PackedScene CharacterModel;

        public void CalculateCurrentStats()
        {
            if (CurrentStats.Count == 0)
            {
                foreach (var stat in BaseStats.Keys)
                {
                    CurrentStats[stat] = new Stat(stat);
                }
            }
            StatHistory.Add(CurrentStats);
            foreach (var stat in CurrentStats.Keys)
            {
                var baseStat = BaseStats[stat];
                var currentStat = CurrentStats[stat];
                var growthRate = GrowthRates[stat];
                var classMod = Class.Modifiers.TryGetValue(stat, out var classModifier) ? classModifier : 0;
                float statGrowth = growthRate.CalculateGrowthRate(Level);
                statGrowth += currentStat.Overflow;
                statGrowth *= classMod;
                float newOverflow = statGrowth % 1;

                var newStat = new Stat(stat)
                {
                    Value = Mathf.FloorToInt(baseStat.Value + statGrowth),
                    Overflow = newOverflow
                };

                CurrentStats[stat] = newStat;
            }
        }
    }
}
