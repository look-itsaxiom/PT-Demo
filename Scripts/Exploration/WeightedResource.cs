using Godot;
using System;
using TownResources;

[GlobalClass]
public partial class WeightedResource : Resource
{
    [Export]
    public ResourceType ResourceType { get; set; }
    [Export]
    public float Weight { get; set; }
}
