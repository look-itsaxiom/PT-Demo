using System;
using System.Collections.Generic;
using CharacterData;
using Godot;
using TownResources;

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

	public TownResource this[ResourceType resourceKey]
	{
		get
		{
			return resourceKey switch
			{
				ResourceType.Gold => Gold,
				ResourceType.Wood => Wood,
				ResourceType.Stone => Stone,
				ResourceType.Food => Food,
				_ => throw new ArgumentOutOfRangeException(nameof(resourceKey), "Invalid resource type")
			};
		}
	}

	public EnvGridMap EnvGridMap;
	public VBoxContainer TownResourceDisplay;

	public GameSignalBus GameSignalBus;

	public override void _Ready()
	{
		BuildGrid = GetNode<BuildGrid>("BuildGrid");
		EnvGridMap = GetNode<EnvGridMap>("EnvGridMap");
		TownResourceDisplay = GetNode<VBoxContainer>("TownHUD/TownResourceDisplay");
		GameSignalBus = GameSignalBus.Instance;
		GameSignalBus.Connect(GameSignalBus.SignalName.OnBuildingPlaced, Callable.From<string>(OnBuildingPlaced));
		InitializeTownResources();
		GameSignalBus.Connect(GameSignalBus.SignalName.QuestCompleted, Callable.From<Quest, Character>(OnQuestCompleted));
	}

	private void InitializeTownResources()
	{
		Gold = new TownResource { ResourceKey = ResourceType.Gold, Amount = 0 };
		Wood = new TownResource { ResourceKey = ResourceType.Wood, Amount = 5 };
		Stone = new TownResource { ResourceKey = ResourceType.Stone, Amount = 0 };
		Food = new TownResource { ResourceKey = ResourceType.Food, Amount = 0 };
		UpdateTownResourceDisplay();
	}

	private void UpdateTownResourceDisplay()
	{
		var resourceDisplayNames = new Dictionary<ResourceType, TownResource>
		{
			{ ResourceType.Gold, Gold },
			{ ResourceType.Wood, Wood },
			{ ResourceType.Stone, Stone },
			{ ResourceType.Food, Food }
		};

		foreach (var resourceType in Enum.GetValues<ResourceType>())
		{
			var label = TownResourceDisplay.GetNode<Label>($"{resourceType}Label");
			label.Text = $"{resourceType}: {resourceDisplayNames[resourceType].Amount}";
		}
	}

	private void OnQuestCompleted(Quest completedQuest, Character assignedCharacter)
	{
		if (completedQuest != null)
		{
			GD.Print($"Quest completed: {completedQuest.QuestName}");
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
			GD.Print("Quest rewards distributed!");
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
			newAdventurer.GlobalPosition = new Vector3(0, 1, 0);
		}
	}

	public bool HasNecessaryResources(Godot.Collections.Array<TownResource> buildingRequirements)
	{
		foreach (TownResource requirement in buildingRequirements)
		{
			if (requirement.Amount > this[requirement.ResourceKey].Amount)
			{
				return false;
			}
		}
		return true;
	}

	public void SpendResources(Godot.Collections.Array<TownResource> buildingRequirements)
	{
		foreach (TownResource requirement in buildingRequirements)
		{
			this[requirement.ResourceKey].Amount -= requirement.Amount;
		}
		UpdateTownResourceDisplay();
	}
}
