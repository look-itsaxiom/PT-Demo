using Godot;
using System;

public partial class WorldSelect : Control
{
    public override void _Ready()
    {
        GetNode<Button>("StartNearwoodButton").Pressed += OnStart;
    }

    private void OnStart()
    {
        var location = GD.Load<Location>("res://Data/Exploration/Nearwood.tres");
        ExplorationManager.Instance.StartRun(location);
        GetTree().ChangeSceneToFile("res://Scenes/ExplorationRunner.tscn");
    }
}
