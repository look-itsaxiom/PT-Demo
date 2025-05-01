using Godot;
using System;

public partial class GameSignalBus : Node
{

    public static GameSignalBus Instance;

    public override void _Ready()
    {
        Instance = this;
    }

    [Signal]
    public delegate void EnemyDefeatedEventHandler(string enemyKey);

    [Signal]
    public delegate void ItemCollectedEventHandler(string itemKey, int amount);

    [Signal]
    public delegate void ResourceCollectedEventHandler(string resourceKey, int amount);

    [Signal]
    public delegate void QuestCompletedEventHandler(Quest quest);
}
