using Godot;
using System;

public partial class WorldSelect : Control
{
    public override void _Ready()
    {
        foreach (Button btn in GetNode("MapImage/NodeLayer").GetChildren())
        {
            if (btn is LocationButton locBtn && locBtn.LinkedLocation != null)
            {
                if (ExplorationManager.Instance.CurrentLocation == locBtn.LinkedLocation)
                    btn.GrabFocus();

                btn.Pressed += () =>
                {
                    ExplorationManager.Instance.StartRun(locBtn.LinkedLocation);
                    GetTree().ChangeSceneToFile("res://Scenes/ExplorationRunner.tscn");
                };
            }
        }

        GetNode<Button>("MapImage/TownButton").Pressed += () =>
            GetTree().ChangeSceneToFile("res://Scenes/Town.tscn");

        if (ExplorationManager.Instance.CurrentLocation == null)
            GetNode<Button>("MapImage/TownButton").GrabFocus();
    }
}
