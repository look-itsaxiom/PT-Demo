using System;
using Godot;

public partial class TownManager : Node3D
{
	public int TownLevel { get; set; } = 1;
	public int TownLevelMax { get; set; } = 2;
	public int Experience { get; set; } = 0;
	public int Gold { get; set; } = 100;
	public int Wood { get; set; } = 50;
	public int Stone { get; set; } = 10;
	public int Food { get; set; } = 10;

	public BuildGrid BuildGrid;
	public EnvGridMap EnvGridMap;

	public override void _Ready()
	{
		BuildGrid = GetNode<BuildGrid>("BuildGrid");
		EnvGridMap = GetNode<EnvGridMap>("EnvGridMap");
		BuildGrid.BuildingPlaced += OnBuildingPlaced;
	}

	private void OnBuildingPlaced(string buildingKey)
	{
		if (buildingKey == "GuildHall")
		{
			TownLevel++;
			GD.Print("Adventuring unlocked!");
			var newAdventurerData = CharacterSystem.Instance.GenerateRandomCharacter();
			var newAdventurer = GD.Load<PackedScene>("res://Scenes/npc.tscn").Instantiate() as TownNPC;
			newAdventurer.Initialize(newAdventurerData);
			AddChild(newAdventurer);
			newAdventurer.GlobalPosition = new Vector3(0, 0, 0);
		}
	}

	internal void AddResources(Quest quest)
	{
		Gold += quest.GoldReward;
		Wood += quest.WoodReward;
		Stone += quest.StoneReward;
		Food += quest.FoodReward;
		Experience += quest.ExperienceReward;
		if (Experience >= TownLevel * 100)
		{
			TownLevel++;
			GD.Print($"Town level increased to {TownLevel}");
		}

	}

}
