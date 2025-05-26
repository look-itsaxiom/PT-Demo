using Godot;
using System;
using System.Collections.Generic;
using TownResources;

public class GeneratedRoom
{
    public string DisplayName;
    public RoomType Type;
    public string Description;
    public RoomTemplate TemplateUsed;
    public PackedScene SceneToLoad;

    public List<MonsterTemplate> Monsters = new();
    public ResourceNodeDefinition ResourceNode;
    public PackedScene Item;

    public bool IsUnique;
    public bool IsDiscovered = false;
    public bool IsCleared = false;
}
