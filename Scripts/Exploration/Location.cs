using Godot;
using Godot.Collections;
using TownResources;
using LocationRooms;


namespace Locations
{
    public static class LocationConstants
    {
        public static Array<string> LocationNames = new() {
            "Nearwood"
        };
    }


    [GlobalClass]
    public partial class Location : Resource
    {
        [Export]
        public string LocationName { get; set; } = string.Empty;

        [Export]
        public string LocationDescription { get; set; } = string.Empty;

        [Export]
        public string Biome { get; set; } = string.Empty;

        [Export]
        public int Difficulty { get; set; } = 1;

        [Export]
        public Array<LocationRoom> RoomPool { get; set; } = new();

        [Export]
        public Array<Resource> Bestiary { get; set; } = new();

        [Export]
        public Array<Resource> TreasurePool { get; set; } = new();

        [Export]
        public Array<ResourceType> ResourcePool { get; set; } = new();

        [Export]
        public float ExplorationProgress { get; set; } = 0f;

        public bool IsExplored => ExplorationProgress >= 1f;

        [Export]
        public Array<Location> ConnectedLocations { get; set; } = new();

        [Export]
        public LocationRoomSkeleton Skeleton { get; set; } = new();
    }
}