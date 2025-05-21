using Godot;
using System;
using System.Linq;

public partial class ResourceMiniGameRunner : Control
{
    private ResourceNodeDefinition _node;
    private int _interactionsRemaining;
    private RandomNumberGenerator _rng = new();

    public override void _Ready()
    {
        _node = ExplorationManager.Instance.GetCurrentRoom()?.ResourceNode;

        if (_node == null)
        {
            GD.PrintErr("No resource node found!");
            GetTree().ChangeSceneToFile("res://Scenes/ExplorationRunner.tscn");
            return;
        }

        _interactionsRemaining = _node.InteractionsAllowed;
        _rng.Randomize();

        GetNode<Label>("ResourceTitleLabel").Text = _node.NodeName;
        UpdateRemainingText();

        GetNode<Button>("ClickArea").Pressed += OnHarvestClick;
        GetNode<Button>("ClickArea").GrabFocus();
        GetNode<Button>("ClickArea").Text = "Harvest!";
    }

    private void OnHarvestClick()
    {
        if (_interactionsRemaining <= 0)
        {
            GD.Print("Node depleted!");
            ExplorationManager.Instance.AdvanceRoom();

            GetTree().ChangeSceneToFile("res://Scenes/ExplorationRunner.tscn");
            return;
        }

        var resource = PickWeightedResource(_node.Contents);
        var amount = 25; // You can tune this per node
        GameSignalBus.Instance.EmitSignal(GameSignalBus.SignalName.ResourceCollected, new ResourceCollectEvent
        {
            EventData = new TownResources.TownResource
            {
                ResourceKey = _node.Contents[0].ResourceType,
                Amount = amount,
            },
            AttributedCharacter = CharacterSystem.Instance.GetPlayerCharacter(),
        });
        var lootLabel = new Label
        {
            Text = $"+{amount} {resource}"
        };
        GetNode<VBoxContainer>("LootDisplay").AddChild(lootLabel);

        _interactionsRemaining--;
        UpdateRemainingText();

        if (_interactionsRemaining == 0)
        {
            GetNode<Button>("ClickArea").Text = "Done!";
        }
    }

    private void UpdateRemainingText()
    {
        GetNode<Label>("RemainingLabel").Text = $"Clicks remaining: {_interactionsRemaining}";
    }

    private string PickWeightedResource(Godot.Collections.Array<WeightedResource> pool)
    {
        float totalWeight = 0f;
        foreach (var res in pool)
            totalWeight += res.Weight;

        float roll = _rng.RandfRange(0, totalWeight);
        float acc = 0f;

        foreach (var res in pool)
        {
            acc += res.Weight;
            if (roll <= acc)
                return res.ResourceType.ToString();
        }

        return pool[0].ResourceType.ToString(); // Fallback
    }
}
