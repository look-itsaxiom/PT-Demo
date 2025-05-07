using Godot;
using Godot.Collections;
using CharacterData;

[GlobalClass]
public partial class ClassData : Resource
{
    [Export] public string Name;
    [Export] public string Description;
    [Export] public int tier;
    [Export] public string[] Requirements;
    [Export] public Dictionary<Stat.StatKey, float> Modifiers = new();
}
