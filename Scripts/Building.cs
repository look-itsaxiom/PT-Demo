using Godot;
using System;

public partial class Building : Node3D
{
	[Export] public Vector2I GridSize;
	[Export] public string BuildingName;
	[Export] public string Description;
	[Export] public string ScenePath;
}
