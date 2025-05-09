using Godot;
using Godot.Collections;
using LocationRooms;

[GlobalClass]
public partial class LocationRoomSkeleton : Resource
{
    [Export]
    public Location AssociatedLocation { get; set; } = null;

    [Export]
    public int DifficultyMin { get; set; } = 1;

    [Export]
    public int DifficultyMax { get; set; } = 10;

    [Export]
    public int RoomCountMin { get; set; } = 5;

    [Export]
    public int RoomCountMax { get; set; } = 10;

    [Export]
    public int RoomCount { get; set; } = 5;

    [Export]
    public Array<RoomType> RoomSequence { get; set; } = new();
}
