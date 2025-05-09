using Godot;
using System;

namespace LocationRooms
{
    public abstract partial class LocationRoom : Resource
    {
        public abstract RoomType RoomType { get; set; }
        public abstract string RoomName { get; set; }
        public abstract string RoomDescription { get; set; }
        public abstract string RoomImage { get; set; }
        public abstract PackedScene RoomScene { get; set; }
        public abstract int Tier { get; set; }
    }

    public enum RoomType
    {
        MonsterEncounter,
        Treasure,
        ResourceNode,
        Event,
        BossEncounter,
        Rest,
        Shop,
        Quest,
        Special
    }
}
