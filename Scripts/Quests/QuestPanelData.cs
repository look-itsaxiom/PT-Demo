using CharacterData;
using Godot;
using System;

public partial class QuestPanelData : Button
{
    [Export]
    public Quest Quest { get; set; }

    public Action<Quest> OnQuestSelected;

    private Label QuestNameLabel;
    private Label QuestDescriptionLabel;
    private Label QuestAvailabilityLabel;
    private Label AssignedCharacterLabel;

    public void Initialize(Quest quest)
    {
        QuestNameLabel = GetNode<Label>("QuestNameLabel");
        QuestDescriptionLabel = GetNode<Label>("QuestDescriptionLabel");
        QuestAvailabilityLabel = GetNode<Label>("QuestAvailabilityLabel");
        AssignedCharacterLabel = GetNode<Label>("AssignedCharacterLabel");
        this.Pressed += () => OnQuestSelected?.Invoke(Quest);
        if (quest == null)
        {
            GD.PrintErr("Quest is null");
            return;
        }
        GD.Print($"Initializing quest panel with quest: {quest.QuestName}");
        Quest = quest;
        QuestNameLabel.Text = Quest.QuestName;
        QuestDescriptionLabel.Text = quest.Description;
        if (Quest.Availability == Quest.QuestAvailability.Player)
        {
            QuestAvailabilityLabel.Text = "Player";
        }
        else if (Quest.Availability == Quest.QuestAvailability.NPC)
        {
            QuestAvailabilityLabel.Text = "Adventurer";
        }
        else
        {
            QuestAvailabilityLabel.Text = "Player/Adventurer";
        }
    }

    public void InitializeActiveQuest(Quest quest, Character assignedCharacter)
    {
        QuestNameLabel = GetNode<Label>("QuestNameLabel");
        QuestDescriptionLabel = GetNode<Label>("QuestDescriptionLabel");
        QuestAvailabilityLabel = GetNode<Label>("QuestAvailabilityLabel");
        AssignedCharacterLabel = GetNode<Label>("AssignedCharacterLabel");
        this.Disabled = true;
        if (quest == null)
        {
            GD.PrintErr("Quest is null");
            return;
        }
        GD.Print($"Initializing quest panel with quest: {quest.QuestName}");
        Quest = quest;
        QuestNameLabel.Text = Quest.QuestName;
        QuestDescriptionLabel.Text = quest.Description;
        AssignedCharacterLabel.Text = assignedCharacter.CharacterName;
    }
}
