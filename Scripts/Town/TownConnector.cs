using System;
using System.Collections.Generic;
using CharacterData;
using Godot;
using TownResources;

public partial class TownConnector : Node3D
{
    public BuildGrid BuildGrid;
    public EnvGridMap EnvGridMap;
    public VBoxContainer TownResourceDisplay;
    public Dictionary<ResourceType, TownResource> ResourceDisplayNames;

    public override void _Ready()
    {
        BuildGrid = GetNode<BuildGrid>("BuildGrid");
        EnvGridMap = GetNode<EnvGridMap>("EnvGridMap");
        TownResourceDisplay = GetNode<VBoxContainer>("TownHUD/TownResourceDisplay");
        UpdateTownResourceDisplay();
    }

    public override void _Process(double delta)
    {
        UpdateTownResourceDisplay();
    }


    private void UpdateTownResourceDisplay()
    {
        ResourceDisplayNames = new Dictionary<ResourceType, TownResource>
        {
            { ResourceType.Urum, TownManager.Instance.Urum },
            { ResourceType.Terratite, TownManager.Instance.Terratite },
            { ResourceType.Aquatite, TownManager.Instance.Aquatite },
            { ResourceType.Ventite, TownManager.Instance.Ventite },
            { ResourceType.Ignitite, TownManager.Instance.Ignitite },
            { ResourceType.Lumia, TownManager.Instance.Lumia },
            { ResourceType.Tenebria, TownManager.Instance.Tenebria }
        };

        foreach (var resourceType in Enum.GetValues<ResourceType>())
        {
            var label = TownResourceDisplay.GetNode<Label>($"{resourceType}Label");
            label.Text = $"{resourceType}: {ResourceDisplayNames[resourceType].Amount}";
        }
    }

    private void OnBuildingPlaced(string buildingKey)
    {
        if (buildingKey == "GuildHall")
        {
            GD.Print("Adventuring unlocked!");
            var newAdventurerData = CharacterSystem.Instance.GenerateRandomCharacter();
            var newAdventurer = GD.Load<PackedScene>("res://Scenes/npc.tscn").Instantiate() as TownNPC;
            newAdventurer.Initialize(newAdventurerData);
            AddChild(newAdventurer);
            newAdventurer.GlobalPosition = new Vector3(0, 1, 0);
        }
    }
}
