using Godot;
using System;

namespace TownResources
{
    public enum ResourceType
    {
        Terratite,
        Aquatite,
        Ventite,
        Ignitite,
        Lumia,
        Tenebria,
        Urum,
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

