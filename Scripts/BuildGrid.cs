using Godot;
using System;
using System.Collections.Generic;

public partial class BuildGrid : Node3D
{
	[Export] public GridMap buildGridMap;

	public Dictionary<Vector2I, bool> tileStates = new();

	public override void _Ready()
	{
		buildGridMap = GetNode<GridMap>("BuildGridMap");
		BuildTileMapFromGrid();
	}

	private void BuildTileMapFromGrid()
	{
		tileStates.Clear();

		var usedCells = buildGridMap.GetUsedCells();

		foreach (Vector3I cell in usedCells)
		{
			// We're only interested in X and Z — Y is vertical height
			Vector2I pos2D = new Vector2I(cell.X, cell.Z);
			tileStates[pos2D] = false; // false = unoccupied
		}

		GD.Print("Total buildable tiles found: ", tileStates.Count);
	}

	public Vector3 GridToWorld(Vector2I tile)
	{
		var cellSize = buildGridMap.CellSize;
		return new Vector3(
			tile.X * cellSize.X + cellSize.X / 2.0f,
			0,
			tile.Y * cellSize.Z + cellSize.Z / 2.0f
		);
	}

}
