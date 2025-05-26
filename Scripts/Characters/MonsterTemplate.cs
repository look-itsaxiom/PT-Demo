using CharacterData;
using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class MonsterTemplate : Resource
{
    [Export]
    public string MonsterName;

    [Export]
    public string Description;

    [Export]
    public int Level = 1;

    [Export]
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

    [Export]
    public Dictionary<Stat.StatKey, double> ScalingMultipliers = new()
    {
        { Stat.StatKey.STR, 1.0 },
        { Stat.StatKey.END, 1.0 },
        { Stat.StatKey.DEF, 1.0 },
        { Stat.StatKey.INT, 1.0 },
        { Stat.StatKey.SPI, 1.0 },
        { Stat.StatKey.MDF, 1.0 },
        { Stat.StatKey.SPD, 1.0 },
        { Stat.StatKey.LCK, 1.0 },
        { Stat.StatKey.ACC, 1.0 }
    };

    [Export]
    public Vector2 StatRandomizationRange = new Vector2(0.5f, 1.5f);

    [Export]
    public string[] Abilities;

    [Export]
    public Array<Attribute> Attributes;

    [Export]
    public string CombatBehavior;

    [Export]
    public string[] ItemDrops;

    [Export]
    public PackedScene MonsterModel;
}