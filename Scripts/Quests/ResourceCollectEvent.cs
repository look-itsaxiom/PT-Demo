using Godot;
using TownResources;
using CharacterData;

public partial class ResourceCollectEvent : QuestEvent<TownResource>
{
    public override string EventName { get; set; }
    public override Character AttributedCharacter { get; set; }
    public override TownResource EventData { get; set; }
}
