using CharacterData;
using Godot;
using Godot.Collections;

public abstract partial class QuestEvent : Resource
{
    public abstract string EventName { get; set; }
    public abstract Character AttributedCharacter { get; set; }
}
