using Godot;
using Godot.Collections;
using TownResources;

[GlobalClass]
public partial class ResourceNodeDefinition : Resource
{
    [Export]
    public string NodeName { get; set; } = "Unnamed Node";

    [Export]
    public string Description { get; set; } = "";

    [Export]
    public Array<WeightedResource> Contents { get; set; } = new();

    [Export]
    public int InteractionsAllowed { get; set; } = 3;
}
