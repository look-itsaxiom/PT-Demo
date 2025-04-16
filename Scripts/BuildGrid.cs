using Godot;
using System;
using System.Collections.Generic;

public struct TileData
{
	public Vector3I Position;
	public bool IsOccupied;
	public string BuildingKey;
	public Node3D BuildingInstance;
}

public partial class BuildGrid : Node3D
{
	[Export] public GridMap buildGridMap;

	public Dictionary<Vector3I, TileData> tileStates = new();

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
			tileStates[cell] = new TileData
			{
				Position = cell,
				IsOccupied = false,
				BuildingKey = string.Empty,
				BuildingInstance = null
			};
		}

		GD.Print("Total buildable tiles found: ", tileStates.Count);
	}

	public bool CanPlaceBuilding(Vector3I origin, Vector2I size)
	{
		for (int x = 0; x < size.X; x++)
			for (int z = 0; z < size.Y; z++)
			{
				var tile = new Vector3I(origin.X + x, origin.Y, origin.Z + z);
				if (tileStates.TryGetValue(tile, out TileData tileData) && tileData.IsOccupied)
					return false;
			}
		return true;
	}

	public void PlaceBuilding(string buildingKey, Node3D buildingInstance, Vector3I origin, Vector2I size)
	{
		for (int x = 0; x < size.X; x++)
			for (int z = 0; z < size.Y; z++)
			{
				var tile = new Vector3I(origin.X + x, origin.Y, origin.Z + z);
				if (!tileStates.TryGetValue(tile, out var tileData))
				{
					GD.PrintErr($"Tried to place building on invalid tile: {tile}");
					continue; // Skip invalid tiles
				}

				tileData.IsOccupied = true;
				tileData.BuildingKey = buildingKey;
				tileData.BuildingInstance = buildingInstance;
				tileStates[tile] = tileData;
				buildGridMap.SetCellItem(tile, -1);
			}
	}

	public Vector3 GridToWorld(Vector3I tile)
	{
		var cellSize = buildGridMap.CellSize;
		var normieValue = new Vector3(
			tile.X * cellSize.X + cellSize.X / 2.0f,
			tile.Y + Transform.Origin.Y,
			tile.Z * cellSize.Z + cellSize.Z / 2.0f
		);

		return normieValue;
	}

}
