using CharacterData;
using Godot;
using System;
using System.Collections.Generic;

public partial class QuestManager : Node
{
    [Export]
    public static QuestManager Instance;
    public DataRegistry DataRegistry;

    public struct ActiveQuest
    {
        public Quest Quest;
        public Character AssignedCharacter;
        public int Progress;
    }

    public struct CompletedQuest
    {
        public Quest Quest;
        public Character AssignedCharacter;
        public bool ResourcesCollected;
    }

    public List<ActiveQuest> ActiveQuests = new List<ActiveQuest>();
    public List<CompletedQuest> CompletedQuests = new List<CompletedQuest>();
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

    public void StartQuest(Quest quest, Character assignedCharacter)
    {
        GD.Print($"Starting quest: {quest.QuestName}");

        var activeQuest = new ActiveQuest
        {
            Quest = quest,
            AssignedCharacter = assignedCharacter,
            Progress = 0
        };
        // Logic to start the quest
        ActiveQuests.Add(activeQuest);

        if (IsRepeatableQuest(quest))
        {
            GD.Print($"Quest {quest.QuestName} is repeatable and already active.");
            return;
        }
        else
        {
            AvailableQuests.Remove(quest);
        }

    }

    private bool IsRepeatableQuest(Quest quest)
    {
        bool IsRepeatable = false;
        if (quest.IsRepeatable)
        {
            IsRepeatable = true;
        }

        if (quest.RepeatableCount > 0)
        {
            if (ActiveQuests.FindAll(x => x.Quest == quest).Count >= quest.RepeatableCount)
            {
                IsRepeatable = false;
            }
        }
        else if (quest.RepeatableCount == -1)
        {
            IsRepeatable = true;
        }

        return IsRepeatable;
    }


    public void CompleteQuest(Quest quest, Character assignedCharacter)
    {
        GD.Print($"Completing quest: {quest.QuestName}");

        var completedQuest = new CompletedQuest
        {
            Quest = quest,
            AssignedCharacter = assignedCharacter,
            ResourcesCollected = false
        };

        TownManager townManager = GetNode<TownManager>("./Town");

        townManager.AddResources(quest);

        CompletedQuests.Add(completedQuest);

        var activeQuest = ActiveQuests.Find(x => x.Quest == quest && x.AssignedCharacter == assignedCharacter);
        ActiveQuests.Remove(activeQuest);
    }
}
