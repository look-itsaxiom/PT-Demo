using Godot;
using System.Collections.Generic;

public partial class DataRegistry : Node
{
	public Building guildHall = new()
	{
		GridSize = new Vector2I(12, 10),
		BuildingName = "Guild Hall",
		Description = "A place for adventurers to gather and form parties.",
		ScenePath = "res://Scenes/Buildings/GuildHall.tscn",
	};

	public Dictionary<string, Building> buildingTemplates;

	public DataRegistry()
	{
		buildingTemplates = new Dictionary<string, Building>
		{
			{ "GuildHall", guildHall }
		};
	}
}
