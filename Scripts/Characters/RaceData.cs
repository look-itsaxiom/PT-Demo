using CharacterData;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class RaceData : Resource
{
    [Export] public string Name;
    [Export] public string Description;
    [Export] public Dictionary<Stat.StatKey, Vector2I> BaseStats = new();
}