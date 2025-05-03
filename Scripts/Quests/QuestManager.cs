using CharacterData;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class QuestManager : Node
{
    [Export]
    public static QuestManager Instance;
    public DataRegistry DataRegistry;
    public GameSignalBus GameSignalBus;

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
        GameSignalBus = GameSignalBus.Instance;
        // Load quests or initialize quest data here
        GetAvailableQuests();

        GameSignalBus.Connect(GameSignalBus.SignalName.EnemyDefeated, Callable.From<string>((enemyKey) =>
        {
            OnGameSignalReceived(GameSignalBus.SignalName.EnemyDefeated, new()
            {
                { "enemyKey", enemyKey }
            });
        }));

        GameSignalBus.Connect(GameSignalBus.SignalName.ItemCollected, Callable.From<string, int>((itemKey, amount) =>
        {
            OnGameSignalReceived(GameSignalBus.SignalName.ItemCollected, new()
            {
            { "itemKey", itemKey },
            { "amount", amount }
            });
        }));

        GameSignalBus.Connect(GameSignalBus.SignalName.ResourceCollected, Callable.From<string, int>((resourceKey, amount) =>
        {
            OnGameSignalReceived(GameSignalBus.SignalName.ResourceCollected, new()
            {
                { "resourceKey", resourceKey },
                { "amount", amount }
            });
        }));
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

        GameSignalBus.EmitSignal(GameSignalBus.SignalName.QuestCompleted, quest);

        CompletedQuests.Add(completedQuest);

        var activeQuest = ActiveQuests.Find(x => x.Quest == quest && x.AssignedCharacter == assignedCharacter);
        ActiveQuests.Remove(activeQuest);
    }

    public void OnGameSignalReceived(string signalName, Dictionary<string, Variant> eventData)
    {
        foreach (var activeQuest in ActiveQuests.ToList())
        {
            foreach (var goal in activeQuest.Quest.Goals)
            {
                goal.OnEvent(signalName, eventData);
            }

            if (activeQuest.Quest.Goals.All(g => g.IsComplete()))
            {
                CompleteQuest(activeQuest.Quest, activeQuest.AssignedCharacter);
            }
        }
    }

    internal bool IsCharacterOnQuest(Character npc)
    {
        return ActiveQuests.Any(x => x.AssignedCharacter.CharacterId == npc.CharacterId);
    }

}
