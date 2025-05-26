using Godot;
using Godot.Collections;
using System;

public partial class CombatRunner : Node3D
{
    [Export]
    public Array<Node3D> PartyZones;

    [Export]
    public Array<Node3D> EnemyZones;

    public override void _Ready()
    {
        GeneratedRoom currentRoom = ExplorationManager.Instance.GetCurrentRoom();
        PartyZones = new Array<Node3D>();
        EnemyZones = new Array<Node3D>();
        foreach (Node3D zone in GetNode<Node3D>("PartyZones").GetChildren())
        {
            PartyZones.Add(zone);
        }

        foreach (Node3D zone in GetNode<Node3D>("EnemyZones").GetChildren())
        {
            EnemyZones.Add(zone);
        }
    }
}
