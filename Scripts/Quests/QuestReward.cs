using Godot;
using Godot.Collections;

public abstract partial class QuestReward : Resource
{
    public enum RewardType
    {
        Resource,
        Item,
        Experience,
        Renown
    }

    public abstract RewardType Type { get; set; }
    public abstract int Amount { get; set; }
}
