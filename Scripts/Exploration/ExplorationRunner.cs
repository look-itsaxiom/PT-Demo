using Godot;
using System;

public partial class ExplorationRunner : Control
{
    private ExplorationManager Manager => ExplorationManager.Instance;

    public Button EnterRoomBtn;
    public Button AdvanceBtn;

    public override void _Ready()
    {
        EnterRoomBtn = GetNode<Button>("MarginContainer/VBoxContainer/EnterRoomButton");
        AdvanceBtn = GetNode<Button>("MarginContainer/VBoxContainer/AdvanceButton");
        EnterRoomBtn.Visible = false;
        AdvanceBtn.Visible = true;
        DisplayRoom(Manager.GetCurrentRoom());
        AdvanceBtn.Pressed += OnAdvance;
    }

    private void DisplayRoom(GeneratedRoom room)
    {
        if (room == null)
        {
            GD.PrintErr("No room to display!");
            return;
        }

        if (Manager.IsRunComplete())
        {
            GD.Print("Run complete from ResourceMiniGame return.");
            GetTree().ChangeSceneToFile("res://Scenes/Town.tscn");
            return;
        }


        AdvanceBtn.GrabFocus();

        GetNode<Label>("MarginContainer/VBoxContainer/RoomTitle").Text = room.DisplayName;
        GetNode<Label>("MarginContainer/VBoxContainer/RoomType").Text = $"Type: {room.Type}";
        GetNode<RichTextLabel>("MarginContainer/VBoxContainer/RoomDescription").Text = room.Description;

        if (room.Type == RoomType.ResourceNode)
        {
            AdvanceBtn.Visible = false;
            EnterRoomBtn.Visible = true;
            EnterRoomBtn.GrabFocus();
            EnterRoomBtn.Pressed += GoToResourceScene;
            return;
        }

        if (room.Type == RoomType.Combat)
        {
            AdvanceBtn.Visible = false;
            EnterRoomBtn.Visible = true;
            EnterRoomBtn.GrabFocus();
            EnterRoomBtn.Pressed += GoToCombatScene;
            return;
        }
    }

    private void GoToCombatScene()
    {
        GetTree().ChangeSceneToFile("res://Scenes/CombatRunner.tscn");
    }


    private void GoToResourceScene()
    {
        GetTree().ChangeSceneToFile("res://Scenes/ResourceMiniGameRunner.tscn");
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
