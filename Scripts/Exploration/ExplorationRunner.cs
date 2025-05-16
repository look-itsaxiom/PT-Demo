using Godot;
using System;

public partial class ExplorationRunner : Control
{
    private ExplorationManager Manager => ExplorationManager.Instance;

    public override void _Ready()
    {
        DisplayRoom(Manager.GetCurrentRoom());
        GetNode<Button>("MarginContainer/VBoxContainer/AdvanceButton").Pressed += OnAdvance;
    }

    private void DisplayRoom(GeneratedRoom room)
    {
        if (room == null)
        {
            GD.PrintErr("No room to display!");
            return;
        }

        GetNode<Label>("MarginContainer/VBoxContainer/RoomTitle").Text = room.DisplayName;
        GetNode<Label>("MarginContainer/VBoxContainer/RoomType").Text = $"Type: {room.Type}";
        GetNode<RichTextLabel>("MarginContainer/VBoxContainer/RoomDescription").Text = room.Description;
    }

    private void OnAdvance()
    {
        if (Manager.AdvanceRoom())
        {
            DisplayRoom(Manager.GetCurrentRoom());
        }
        else
        {
            GD.Print("Exploration complete!");
            GetTree().ChangeSceneToFile("res://Scenes/Town.tscn"); // Replace with actual town scene path
        }
    }
}
