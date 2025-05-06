using Godot;
using System;

namespace TownResources
{
    public enum ResourceType
    {
        Wood,
        Stone,
        Food,
        Gold
    }

    [GlobalClass]
    public partial class TownResource : Resource
    {
        [Export]
        public ResourceType ResourceKey { get; set; }

        [Export]
        public int Amount { get; set; } = 0;
    }
}

