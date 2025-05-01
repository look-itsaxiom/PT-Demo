using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class ResourceCollectGoal : QuestGoal
{
    [Export]
    public string ResourceKey;

    [Export]
    public int Amount = 0;

    private int Progress = 0;

    public override void OnEvent(string signalName, Dictionary<string, Variant> data)
    {
        if (signalName == "ResourceCollected" && (string)data["resourceKey"] == ResourceKey)
        {
            int collectedAmount = (int)data["amount"];
            Progress += collectedAmount;
        }
    }

    public override bool IsComplete()
    {
        return Progress >= Amount;
    }
}
