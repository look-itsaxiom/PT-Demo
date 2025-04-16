using Godot;
using System.Collections.Generic;

public partial class DataRegistry : Node
{
	[Export] public static DataRegistry Instance;

	public Building guildHall = new()
	{
		GridSize = new Vector2I(12, 8),
		BuildingName = "Guild Hall",
		Description = "A place for adventurers to gather and form parties.",
		ScenePath = "res://Scenes/Buildings/GuildHall.tscn",
	};

	public Dictionary<string, Building> buildingTemplates;

	public override void _Ready()
	{
		Instance = this;
		buildingTemplates = new Dictionary<string, Building>
		{
			{ "GuildHall", guildHall }
		};
	}
}
