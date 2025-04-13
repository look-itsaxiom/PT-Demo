using Godot;
using System.Collections.Generic;

public partial class DataRegistry : Node
{
	public Building guildHall = new Building("GuildHall", 2, 3, "res://Scenes/Buildings/GuildHall.tscn");
	public Dictionary<string, Building> buildingTemplates;

	public DataRegistry()
	{
		buildingTemplates = new Dictionary<string, Building>
		{
			{ "GuildHall", guildHall }
		};
	}
}