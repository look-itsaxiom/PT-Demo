using CharacterData;
using Godot;
using System.Collections.Generic;

[GlobalClass]
public partial class Quest : Resource
{
    [Export]
    public string QuestName { get; set; } = "New Quest";

    [Export]
    public string Description { get; set; } = "Quest description";

    public int QuestID { get; set; } = 0;

    [Export]
    public QuestAvailability Availability { get; set; } = QuestAvailability.Player;

    public enum QuestAvailability
    {
        Player,
        NPC,
        Both
    }
    public static Dictionary<QuestAvailability, string> QuestAvailabilityNames = new()
    {
        { QuestAvailability.Player, "Player" },
        { QuestAvailability.NPC, "NPC" },
        { QuestAvailability.Both, "Both" }
    };
    public static Dictionary<QuestAvailability, string> QuestAvailabilityDescriptions = new()
    {
        { QuestAvailability.Player, "Available for Player" },
        { QuestAvailability.NPC, "Available for NPC" },
        { QuestAvailability.Both, "Available for both Player and NPC" }
    };
}

