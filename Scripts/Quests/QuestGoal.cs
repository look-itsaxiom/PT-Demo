using Godot;
using System;
using System.Collections.Generic;

public interface IQuestGoal
{
    void OnEventRaw(string signalName, object rawEventData);
    bool IsComplete();
}

public abstract partial class QuestGoal<T> : Resource, IQuestGoal
{
    public abstract void OnEvent(string signalName, T eventData);
    public abstract bool IsComplete();

    void IQuestGoal.OnEventRaw(string signalName, object rawEventData)
    {
        if (rawEventData is T typed)
        {
            OnEvent(signalName, typed);
        }
        else
        {
            GD.PrintErr($"Invalid event data type for {GetType().Name}. Expected {typeof(T)}, got {rawEventData?.GetType()}");
        }
    }
}
