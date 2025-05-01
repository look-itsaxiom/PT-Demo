using System;
using System.Collections.Generic;
using Godot;

public partial class TownManager : Node3D
{
	public int TownLevel { get; set; } = 1;
	public int TownLevelMax { get; set; } = 2;
	public int Experience { get; set; } = 0;
	public TownResource Gold { get; set; }
	public TownResource Wood { get; set; }
	public TownResource Stone { get; set; }
	public TownResource Food { get; set; }

	public BuildGrid BuildGrid;
	public EnvGridMap EnvGridMap;
	public VBoxContainer TownResourceDisplay;

	public GameSignalBus GameSignalBus;

	public override void _Ready()
	{
		BuildGrid = GetNode<BuildGrid>("BuildGrid");
		EnvGridMap = GetNode<EnvGridMap>("EnvGridMap");
		TownResourceDisplay = GetNode<VBoxContainer>("TownHUD/TownResourceDisplay");
		GameSignalBus = GameSignalBus.Instance;
		BuildGrid.BuildingPlaced += OnBuildingPlaced;
		InitializeTownResources();
		GameSignalBus.Connect(GameSignalBus.SignalName.QuestCompleted, Callable.From<Quest>(OnQuestCompleted));
	}

	private void InitializeTownResources()
	{
		Gold = new TownResource { ResourceKey = TownResource.ResourceType.Gold, Amount = 0 };
		Wood = new TownResource { ResourceKey = TownResource.ResourceType.Wood, Amount = 0 };
		Stone = new TownResource { ResourceKey = TownResource.ResourceType.Stone, Amount = 0 };
		Food = new TownResource { ResourceKey = TownResource.ResourceType.Food, Amount = 0 };
		UpdateTownResourceDisplay();
	}

	private void UpdateTownResourceDisplay()
	{
		var resourceDisplayNames = new Dictionary<TownResource.ResourceType, TownResource>
		{
			{ TownResource.ResourceType.Gold, Gold },
			{ TownResource.ResourceType.Wood, Wood },
			{ TownResource.ResourceType.Stone, Stone },
			{ TownResource.ResourceType.Food, Food }
		};

		foreach (var resourceType in Enum.GetValues<TownResource.ResourceType>())
		{
			var label = TownResourceDisplay.GetNode<Label>($"{resourceType}Label");
			label.Text = $"{resourceType}: {resourceDisplayNames[resourceType].Amount}";
		}
	}

	private void OnQuestCompleted(Quest completedQuest)
	{
		if (completedQuest != null)
		{
			foreach (var reward in completedQuest.Rewards)
			{
				switch (reward.Type)
				{
					case QuestReward.RewardType.Gold:
						Gold.Amount += reward.Amount;
						break;
					case QuestReward.RewardType.Wood:
						Wood.Amount += reward.Amount;
						break;
					case QuestReward.RewardType.Stone:
						Stone.Amount += reward.Amount;
						break;
					case QuestReward.RewardType.Food:
						Food.Amount += reward.Amount;
						break;
					case QuestReward.RewardType.Item:
						// Handle item rewards
						break;
					case QuestReward.RewardType.Experience:
						Experience += reward.Amount;
						break;
				}
			}
			UpdateTownResourceDisplay();
		}
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
}
