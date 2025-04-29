using Godot;
using System;
using System.Collections.Generic;

public partial class QuestMenu : Control
{
    public Action OnClose;
    public PackedScene QuestPanelScene = GD.Load<PackedScene>("res://Scenes/UI/QuestPanel.tscn");
    public List<Quest> AvailableQuests = new List<Quest>();
    public Quest selectedQuest;
    private VBoxContainer AvailableQuestsList;
    private VBoxContainer AssignedQuestsList;
    private Button AssignPlayerQuestButton;
    private Button AssignNPCQuestButton;

    public override void _Ready()
    {
        AssignPlayerQuestButton = GetNode<Button>("AvailableQuestsList/AssignPlayerQuestButton");
        AssignNPCQuestButton = GetNode<Button>("AvailableQuestsList/AssignNPCQuestButton");
        AvailableQuestsList = GetNode<VBoxContainer>("AvailableQuestsList/AvailableQuestList");
        AssignedQuestsList = GetNode<VBoxContainer>("AssignedQuestsList/AssignedQuestList");

        //playerQuestButton.Pressed += OnPlayerQuestSelected;
        //npcQuestButton.Pressed += OnNPCQuestSelected;

        this.VisibilityChanged += () => { if (Visible) AssignPlayerQuestButton.GrabFocus(); };
    }

    public void Open()
    {
        Visible = true;
        SetProcessInput(true);
        foreach (var quest in QuestManager.Instance.AvailableQuests)
        {
            GD.Print($"Quest: {quest.QuestName}");
            GD.Print("Creating quest panel");
            var questPanel = QuestPanelScene.Instantiate();
            var questPanelData = questPanel as QuestPanelData;
            questPanelData.Initialize(quest);
            questPanelData.OnQuestSelected = (Quest selectedQuest) =>
            {
                this.selectedQuest = selectedQuest;
                GD.Print($"Selected quest: {selectedQuest.QuestName}");
            };
            AvailableQuestsList.AddChild(questPanel);
        }
    }

    public void Close()
    {
        AvailableQuests.Clear();
        Visible = false;
        SetProcessInput(false);
        OnClose?.Invoke();
        foreach (Node child in AvailableQuestsList.GetChildren())
        {
            child.QueueFree();
        }
        foreach (Node child in AssignedQuestsList.GetChildren())
        {
            child.QueueFree();
        }
        selectedQuest = null;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            Close();
        }
    }
}

