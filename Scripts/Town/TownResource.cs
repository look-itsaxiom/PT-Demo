using Godot;
using System;

[GlobalClass]
public partial class TownResource : Resource
{

    public enum ResourceType
    {
        Wood,
        Stone,
        Food,
        Gold
    }

    [Export]
    public ResourceType ResourceKey { get; set; }

    [Export]
    public int Amount { get; set; } = 0;
}
