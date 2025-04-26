using Godot;

public partial class TownManager : Node3D
{
	public int TownLevel { get; set; } = 1;
	public int TownLevelMax { get; set; } = 2;
	public int Gold { get; set; } = 100;
	public int Wood { get; set; } = 50;
	public int Stone { get; set; } = 10;
	public int Food { get; set; } = 10;

	public BuildGrid BuildGrid;

	public override void _Ready()
	{
		BuildGrid = GetNode<BuildGrid>("BuildGrid");
		BuildGrid.BuildingPlaced += OnBuildingPlaced;
	}

	private void OnBuildingPlaced(string buildingKey)
	{
		if (buildingKey == "GuildHall")
		{
			TownLevel++;
			GD.Print("Adventuring unlocked!");
			var newAdventurerData = CharacterSystem.GenerateRandomCharacter();
			var newAdventurer = GD.Load<PackedScene>("res://Scenes/npc.tscn").Instantiate() as TownNPC;
			newAdventurer.Initialize(newAdventurerData);
			AddChild(newAdventurer);
			var guildHall = GetNode<Node3D>("BuildSite2/GuildHall");
			var forwardDirection = guildHall.GlobalTransform.Basis.Z.Normalized();
			var spawnDistance = 8.0f;
			var spawnPoint = guildHall.GlobalTransform.Origin + forwardDirection * spawnDistance;
			spawnPoint.Y = 0;
			newAdventurer.GlobalPosition = spawnPoint;
		}
	}

}
