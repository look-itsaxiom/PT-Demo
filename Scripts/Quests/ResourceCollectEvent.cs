using Godot;
using TownResources;
using CharacterData;

public partial class ResourceCollectEvent : QuestEvent
{
    public override string EventName { get; set; }
    public override Character AttributedCharacter { get; set; }
    public TownResource EventData { get; set; }
}
