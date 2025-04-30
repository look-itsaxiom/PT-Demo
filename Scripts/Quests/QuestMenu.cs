using Godot;
using System;
using System.Collections.Generic;

public partial class QuestMenu : Control
{
    public Action OnClose;
    public PackedScene QuestPanelScene = GD.Load<PackedScene>("res://Scenes/UI/QuestPanel.tscn");
    public Quest selectedQuest;
    public Player Player;
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

        AssignPlayerQuestButton.Pressed += OnPlayerQuestSelected;
        AssignNPCQuestButton.Pressed += OnNPCQuestSelected;

        this.VisibilityChanged += () => { if (Visible) AssignPlayerQuestButton.GrabFocus(); };
    }

    private void OnNPCQuestSelected()
    {
        throw new NotImplementedException();
    }


    private void OnPlayerQuestSelected()
    {
        QuestManager.Instance.StartQuest(selectedQuest, Player.PlayerCharacter);
        foreach (QuestPanelData child in AvailableQuestsList.GetChildren())
        {
            if (child.Quest == selectedQuest)
            {
                PopulateAssignedQuests();
                child.QueueFree();
                break;
            }
        }
    }


    public void Open(Player player)
    {
        Visible = true;
        SetProcessInput(true);
        Player = player;
        PopulateAvailableQuests();
    }

    private void PopulateAvailableQuests()
    {
        foreach (var quest in QuestManager.Instance.AvailableQuests)
        {
            GD.Print($"Quest: {quest.QuestName}");
            GD.Print("Creating quest panel");
            var questPanel = QuestPanelScene.Instantiate();
            var questPanelData = questPanel as QuestPanelData;
            questPanelData._Toggled(false);
            questPanelData.Initialize(quest);
            questPanelData.OnQuestSelected = (Quest selectedQuest) =>
            {
                this.selectedQuest = selectedQuest;
                foreach (QuestPanelData child in AssignedQuestsList.GetChildren())
                {
                    child._Toggled(child.Quest == selectedQuest);
                }
            };
            AvailableQuestsList.AddChild(questPanel);
        }
    }

    private void PopulateAssignedQuests()
    {
        foreach (var quest in QuestManager.Instance.ActiveQuests)
        {
            GD.Print($"Quest: {quest.Quest.QuestName}");
            GD.Print("Creating quest panel");
            var questPanel = QuestPanelScene.Instantiate();
            var questPanelData = questPanel as QuestPanelData;
            questPanelData._Toggled(false);
            questPanelData.InitializeActiveQuest(quest.Quest, quest.AssignedCharacter);
            AssignedQuestsList.AddChild(questPanel);
        }
    }

    public void Close()
    {
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

