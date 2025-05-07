using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using TownResources;

public partial class WorldMapNodeSelect : Control
{
    Button HomeNode;
    public Dictionary<string, Button> MapLocations = new Dictionary<string, Button>();
    public override void _Ready()
    {
        HomeNode = GetNode<Button>("Home");
        HomeNode.Pressed += OnHomeNodePressed;

        var locations = GetChildren().Where(x => x is Button && x.Name != "Home");
        foreach (var location in locations)
        {
            var button = location as Button;
            if (button != null)
            {
                MapLocations.Add(button.Name, button);
                button.Pressed += () => OnMapLocationPressed(button.Name);
            }
        }
        HomeNode.GrabFocus();
        base._Ready();
    }

    private void OnHomeNodePressed()
    {
        // Emit a signal or call a method to handle the home node press
        GD.Print("Home node pressed");
        GetTree().ChangeSceneToFile("res://Scenes/Town.tscn");
    }

    private void OnMapLocationPressed(string locationName)
    {
        var playerCharacter = CharacterSystem.Instance.GetPlayerCharacter();
        GameSignalBus.Instance.EmitSignal(GameSignalBus.SignalName.ResourceCollected, new ResourceCollectEvent()
        {
            EventData = new TownResource() { ResourceKey = ResourceType.Wood, Amount = 1 },
            EventName = "ResourceCollectEvent",
            AttributedCharacter = playerCharacter
        });
    }
}
