using Godot;
using Godot.Collections;
using TownResources;

[GlobalClass]
public partial class Building : Resource
{
	[Export] public Vector2I GridSize;
	[Export] public string BuildingName;
	[Export] public string Description;
	[Export] public string ScenePath;
	[Export] public Array<TownResource> BuildingRequirements;
}
