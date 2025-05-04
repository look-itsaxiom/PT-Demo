using CharacterData;
using Godot;
using System;

public partial class GameSignalBus : Node
{

    public static GameSignalBus Instance;

    public override void _Ready()
    {
        Instance = this;
    }

    // Time
    [Signal]
    public delegate void TimeChangedEventHandler(int day, int hour, int minute);

    [Signal]
    public delegate void DayEndedEventHandler();

    [Signal]
    public delegate void DayStartedEventHandler();

    [Signal]
    public delegate void FadeOutFinishedEventHandler();

    [Signal]
    public delegate void FadeInFinishedEventHandler();

    // Quest
    [Signal]
    public delegate void EnemyDefeatedEventHandler(string enemyKey);

    [Signal]
    public delegate void ItemCollectedEventHandler(string itemKey, int amount);

    [Signal]
    public delegate void ResourceCollectedEventHandler(string resourceKey, int amount);

    [Signal]
    public delegate void PlayerStartedQuestEventHandler(Quest quest, Character character);

    [Signal]
    public delegate void NPCStartedQuestEventHandler(Quest quest, Character character);

    [Signal]
    public delegate void QuestCompletedEventHandler(Quest quest);

    // Building
    [Signal]
    public delegate void OnBuildingPlacedEventHandler(string buildingKey);
}
