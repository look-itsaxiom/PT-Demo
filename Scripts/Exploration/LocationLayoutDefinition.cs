using Godot;
using Godot.Collections;

[GlobalClass]
public partial class LocationLayoutDefinition : Resource
{
    [Export]
    public int RoomCountMin { get; set; } = 5;

    [Export]
    public int RoomCountMax { get; set; } = 8;

    [Export]
    public RoomTemplate FirstRoomOverride { get; set; } = null;

    [Export]
    public RoomTemplate LastRoomOverride { get; set; } = null;


    [Export]
    public Array<RoomType> RoomSequence { get; set; } = new(); // Optional fixed order
}
