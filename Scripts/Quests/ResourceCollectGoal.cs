using Godot;
using System;
using TownResources;

[GlobalClass]
public partial class ResourceCollectGoal : QuestGoal
{
    [Export]
    public ResourceType ResourceKey;

    [Export]
    public int Amount = 0;

    private int Progress = 0;

    public override void OnEvent(string signalName, QuestEvent questEvent)
    {
        if (signalName == "ResourceCollected" && questEvent is ResourceCollectEvent resourceCollectEvent)
        {
            GD.Print($"ResourceCollectGoal: Received resource collection event for {resourceCollectEvent.EventData.ResourceKey}");
            if (resourceCollectEvent.EventData.ResourceKey == ResourceKey)
            {
                Progress += resourceCollectEvent.EventData.Amount;
                GD.Print($"Collected {resourceCollectEvent.EventData.Amount} of {ResourceKey}. Total: {Progress}/{Amount}");
            }
        }
    }

    public override bool IsComplete()
    {
        return Progress >= Amount;
    }
}
