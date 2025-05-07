using Godot;
using System;
using System.Collections.Generic;

public abstract partial class QuestGoal : Resource
{
    public abstract void OnEvent(string signalName, QuestEvent questEvent);
    public abstract bool IsComplete();
}
