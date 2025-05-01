using Godot;
using System;
using System.Collections.Generic;

public abstract partial class QuestGoal : Resource
{
    public abstract void OnEvent(string signalName, Dictionary<string, Variant> eventData);
    public abstract bool IsComplete();

}
