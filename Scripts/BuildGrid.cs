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

	[Export] public float buildingVerticalOffset = 0.05f;

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

	public bool CanPlaceBuilding(List<Vector3I> requestedTiles)
	{
		foreach (var tile in requestedTiles)
		{
			if (tileStates.TryGetValue(tile, out TileData tileData))
			{
				if (tileData.IsOccupied)
				{
					GD.Print($"Tile {tile} is already occupied.");
					return false;
				}
			}
			else
			{
				GD.Print($"Tile {tile} is out of bounds.");
				return false;
			}
		}
		return true;
	}

	public void PlaceBuilding(string buildingKey, Node3D buildingInstance, List<Vector3I> requestedTiles)
	{
		foreach (var tile in requestedTiles)
		{
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
		var worldPos = ToGlobal(buildGridMap.MapToLocal(tile));
		worldPos.Y += buildingVerticalOffset;

		return worldPos;
	}

	public void PaintBuildingFootprint(List<Vector3I> requestedTiles, List<Vector3I> availableTiles)
	{
		for (int i = 0; i < availableTiles.Count; i++)
		{
			var tilePos = availableTiles[i];
			if (tileStates.TryGetValue(tilePos, out TileData tileData))
			{
				if (tileData.IsOccupied || requestedTiles.Contains(tilePos))
				{
					buildGridMap.SetCellItem(tilePos, 1);
				}
				else
				{
					buildGridMap.SetCellItem(tilePos, 0);
				}
			}
		}
	}
}
