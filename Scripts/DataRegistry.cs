using Godot;
using System.Collections.Generic;

public partial class DataRegistry : Node
{
	[Export] public static DataRegistry Instance;

	[Export] public string BuildingFolderPath = "res://Data/Buildings";

	public Dictionary<string, Building> buildingTemplates = new();

	public override void _Ready()
	{
		Instance = this;
		RegisterBuildings();
	}

	private void RegisterBuildings()
	{
		foreach (string path in DirAccess.GetFilesAt(BuildingFolderPath))
		{
			if (!path.EndsWith(".tres"))
				continue;

			var fullPath = $"{BuildingFolderPath}/{path}";
			var resource = ResourceLoader.Load<Building>(fullPath);
			if (resource != null)
			{
				buildingTemplates[resource.BuildingName] = resource;
				GD.Print($"Loaded building: {resource.BuildingName} from {fullPath}");
			}
		}
	}
}
