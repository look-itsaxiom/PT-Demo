using CharacterData;
using Godot;
using Godot.Collections;

public abstract partial class QuestEvent<T> : Resource
{
    public abstract string EventName { get; set; }
    public abstract Character AttributedCharacter { get; set; }
    public abstract T EventData { get; set; }
}
