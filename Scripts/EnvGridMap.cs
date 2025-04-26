using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class EnvGridMap : GridMap
{
    BuildGrid buildGrid;
    public List<Vector3I> usedCells;
    public List<Vector3I> buildCells;

    public override void _Ready()
    {
        buildGrid = GetParent().GetNode<BuildGrid>("BuildGrid");
        usedCells = GetUsedCells().ToList();
        buildCells = buildGrid.buildGridMap.GetUsedCells().ToList();
    }

    public Vector3 GetRandomTownPoint()
    {
        var randomPoint = usedCells[GD.RandRange(0, usedCells.Count - 1)];
        if (buildCells.Contains(randomPoint))
        {
            return GetRandomTownPoint();
        }

        return GridToWorld(randomPoint);
    }

    public Vector3 GridToWorld(Vector3I tile)
    {
        var worldPos = ToGlobal(MapToLocal(tile));
        GD.Print("World Position: ", worldPos);
        worldPos.Y += CellSize.Y;
        GD.Print("Adjusted World Position: ", worldPos);

        return worldPos;
    }
}
