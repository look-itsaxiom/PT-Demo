using Godot;

public partial class TownManager : Node3D
{
	public int TownLevel { get; set; } = 1;
	public int TownLevelMax { get; set; } = 2;
	public int Gold { get; set; } = 100;
	public int Wood { get; set; } = 50;
	public int Stone { get; set; } = 10;
	public int Food { get; set; } = 10;

	public override void _Ready()
	{
		var buildGrid = GetNode<BuildGrid>("BuildGrid");
		buildGrid.BuildingPlaced += OnBuildingPlaced;
	}

	private void OnBuildingPlaced(string buildingKey)
	{
		if (buildingKey == "GuildHall")
		{
			TownLevel++;
			GD.Print("Adventuring unlocked!");
			var newAdventurer = CharacterSystem.GenerateRandomCharacter();
			GD.Print($"New adventurer generated: {newAdventurer.CharacterName}");
			GD.Print($"{newAdventurer}");
		}
	}

}
