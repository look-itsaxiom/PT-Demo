using Godot;
using Godot.Collections;

namespace LocationRooms
{
    [GlobalClass]
    public partial class MonsterEncounterRoom : LocationRoom
    {
        public override RoomType RoomType { get; set; } = RoomType.MonsterEncounter;

        [Export]
        public override string RoomName { get; set; } = string.Empty;

        [Export]
        public override string RoomDescription { get; set; } = string.Empty;

        [Export]
        public override string RoomImage { get; set; } = string.Empty;

        [Export]
        public override PackedScene RoomScene { get; set; } = null;

        [Export]
        public override int Tier { get; set; } = 1;

        [Export]
        public Array<string> ResidentMonsters { get; set; } = new();
    }
}
