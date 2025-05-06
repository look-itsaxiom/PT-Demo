using Godot;
using System;
using TownResources;

[GlobalClass]
public partial class ResourceCollectGoal : QuestGoal<TownResource>, IQuestGoal
{
    [Export]
    public ResourceType ResourceKey;

    [Export]
    public int Amount = 0;

    private int Progress = 0;

    public override void OnEvent(string signalName, TownResource resourceCollected)
    {
        if (signalName == "ResourceCollected" && resourceCollected.ResourceKey == ResourceKey)
        {
            GD.Print($"Collected {resourceCollected.Amount} {ResourceKey}");
            Progress += resourceCollected.Amount;
        }
    }

    public override bool IsComplete()
    {
        return Progress >= Amount;
    }
}
