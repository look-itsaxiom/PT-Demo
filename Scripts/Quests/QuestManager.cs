using Godot;
using System;
using System.Collections.Generic;

public partial class QuestManager : Node
{
    [Export]
    public static QuestManager Instance;
    public DataRegistry DataRegistry;

    public List<Quest> ActiveQuests = new List<Quest>();
    public List<Quest> CompletedQuests = new List<Quest>();
    public List<Quest> AvailableQuests = new List<Quest>();

    public override void _Ready()
    {
        Instance = this;
        DataRegistry = DataRegistry.Instance;
        // Load quests or initialize quest data here
        GetAvailableQuests();
    }

    public void GetAvailableQuests()
    {
        // Load quests from the DataRegistry or any other source
        foreach (var quest in DataRegistry.Quests.Values)
        {
            AvailableQuests.Add(quest);
            GD.Print($"Available quest: {quest.QuestName}");
        }
    }

    public void StartQuest(Quest quest)
    {
        GD.Print($"Starting quest: {quest.QuestName}");
        // Logic to start the quest
        ActiveQuests.Add(quest);
        AvailableQuests.Remove(quest);
    }

    public void CompleteQuest(Quest quest)
    {
        GD.Print($"Completing quest: {quest.QuestName}");
        // Logic to complete the quest
        CompletedQuests.Add(quest);
        ActiveQuests.Remove(quest);
    }
}
