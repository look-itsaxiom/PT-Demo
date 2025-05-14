using Godot;
using System;
using TownResources;

[GlobalClass]
public partial class ResourceQuestReward : QuestReward
{
    [Export]
    public override RewardType Type { get; set; } = RewardType.Resource;

    [Export]
    public override int Amount { get; set; } = 0;

    [Export]
    public ResourceType ResourceKey { get; set; } = ResourceType.Urum;
}
