using Godot;
using Locations;
using System;
using Godot.Collections;
using System.Linq;
using TownResources;
using ChronosSpace;
using System.Threading.Tasks;

public partial class WorldMapNodeSelect : Control
{
    Button HomeNode;
    public Dictionary<Location, Button> MapLocationButtons = new Dictionary<Location, Button>();
    public DataRegistry DataRegistry = DataRegistry.Instance;
    public Button currentLocationBtn;
    public Location currentLocation;
    public ColorRect locationVisual;
    public PackedScene townScene = GD.Load<PackedScene>("res://Scenes/Town.tscn");
    public override void _Ready()
    {
        HomeNode = GetNode<Button>("Home");
        locationVisual = GetNode<ColorRect>("CurrentLocationVisual");
        currentLocationBtn = HomeNode;
        locationVisual.Position = HomeNode.GlobalPosition + new Vector2(HomeNode.Size.X / 2, -HomeNode.Size.Y);
        locationVisual.Visible = true;
        HomeNode.Pressed += OnHomeNodePressed;

        foreach (var location in DataRegistry.Locations.Values)
        {
            var button = GetNode<Button>(location.LocationName);
            button.Pressed += () => OnMapLocationPressed(location);
            MapLocationButtons.Add(location, button);
        }

        HomeNode.GrabFocus();
        base._Ready();
    }

    private void MoveLocationVisual()
    {
        var tween = CreateTween();
        tween.TweenProperty(locationVisual, "position", currentLocationBtn.GlobalPosition + new Vector2(currentLocationBtn.Size.X / 2, -currentLocationBtn.Size.Y), currentLocation?.DistanceFromTown ?? 3f)
             .SetTrans(Tween.TransitionType.Sine)
             .SetEase(Tween.EaseType.InOut);
    }


    private void OnHomeNodePressed()
    {
        // Emit a signal or call a method to handle the home node press
        GD.Print("Home node pressed");
        Chronos.Instance.AdvanceTime(currentLocation.DistanceFromTown);
        currentLocationBtn = HomeNode;
        currentLocation = null;
        MoveLocationVisual();
        GetTree().ChangeSceneToPacked(townScene);
        //GetTree().ChangeSceneToFile("res://Scenes/Town.tscn");
    }

    private async Task OnMapLocationPressed(Location location)
    {
        if (currentLocationBtn != MapLocationButtons[location])
        {
            TravelToLocation(location);
        }

        var playerCharacter = CharacterSystem.Instance.GetPlayerCharacter();

        for (var i = 0; i < 5; i++)
        {
            GameSignalBus.Instance.EmitSignal(GameSignalBus.SignalName.ResourceCollected, new ResourceCollectEvent()
            {
                EventData = new TownResource() { ResourceKey = ResourceType.Terratite, Amount = 50 },
                EventName = "ResourceCollectEvent",
                AttributedCharacter = playerCharacter
            });
            GD.Print($"Collected 50 Terratite from {location.LocationName}");
            await Task.Delay(1000);
        }

    }

    private void TravelToLocation(Location location)
    {
        Chronos.Instance.AdvanceTime(location.DistanceFromTown);
        currentLocationBtn = MapLocationButtons[location];
        currentLocation = location;
        MoveLocationVisual();
        currentLocationBtn.GrabFocus();
    }

}
