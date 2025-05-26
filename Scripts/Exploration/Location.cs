using Godot;
using Godot.Collections;
using TownResources;

[GlobalClass]
public partial class Location : Resource
{
    [Export]
    public string LocationName { get; set; } = "Unnamed";

    [Export]
    public string Description { get; set; } = "";

    [Export]
    public LocationLayoutDefinition LayoutDefinition { get; set; }

    [Export]
    public Array<MonsterTemplate> MonsterPool { get; set; } = new();

    [Export]
    public Array<WeightedResource> ResourcePool { get; set; } = new();

    [Export]
    public Array<PackedScene> ItemPool { get; set; } = new();

    [Export]
    public Array<RoomTemplate> UniqueRoomTemplates { get; set; } = new();
}
