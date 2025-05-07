using Godot;
using Godot.Collections;

[GlobalClass]
public partial class QuestReward : Resource
{
    public enum RewardType
    {
        Gold,
        Wood,
        Stone,
        Food,
        Item,
        Experience
    }

    [Export]
    public RewardType Type { get; set; }
    [Export]
    public int Amount { get; set; } = 0;
    [Export]
    public string ItemKey { get; set; } = string.Empty;
}
