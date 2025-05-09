using Godot;

namespace LocationRooms
{
    [GlobalClass]
    public partial class SpecialRoom : LocationRoom
    {
        public override RoomType RoomType { get; set; } = RoomType.Special;

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
    }
}
