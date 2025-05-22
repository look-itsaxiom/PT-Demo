using Godot;

[GlobalClass]
public partial class RoomTemplate : Resource
{
    [Export]
    public string TemplateName { get; set; } = "Unnamed Room";

    [Export]
    public RoomType RoomType { get; set; } = RoomType.Combat;

    [Export(PropertyHint.MultilineText)]
    public string Description { get; set; } = "";

    [Export]
    public bool IsUnique { get; set; } = false; // Shop, Shrine, etc.

    [Export]
    public PackedScene RoomScene { get; set; } = null; // Optional
}

public enum RoomType
{
    Combat,
    ResourceNode,
    Event,
    Treasure,
    Shop,
    Shrine,
    Boss,
    Random
}

