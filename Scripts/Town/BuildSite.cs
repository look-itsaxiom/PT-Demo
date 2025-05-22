using Godot;
using System;
using System.Collections.Generic;
using TownResources;

public partial class BuildSite : Interactable, IInteractable
{
	[Export] public Vector2 BuildSiteSize;

	[Export] public BuildMenu BuildMenu;

	[Export] public BuildGrid BuildGrid;

	[Export] public Camera3D BuildSiteCamera;

	public DataRegistry dataRegistry;

	public List<Vector3I> ownedTiles = new();

	public Player player;

	public override void _Ready()
	{
		BuildMenu = GetNode<BuildMenu>("../BuildMenu");
		dataRegistry = GetNode<DataRegistry>("/root/DataRegistry");
		BuildGrid = GetNode<BuildGrid>("../BuildGrid");
		BuildSiteCamera = GetNode<Camera3D>("Camera3D");
		base._Ready();
		FindOwnedTiles();
	}

	public override void Interact(Player interactor)
	{
		if (!playerInRange) return;
		player = interactor;
		GD.Print("Interacting with the Build Site");
		BuildMenu.OnBuildSelected = OnBuildingChosen;
		player.CanMove = false;
		BuildMenu.Open();
	}

	public override void OnBodyExited(Node3D body)
	{
		base.OnBodyExited(body);
		if (!playerInRange && BuildMenu.Visible)
		{
			player = null;
			BuildMenu.Close();
		}
	}

	private void OnBuildingChosen(string buildingKey)
	{
		// Get building data
		var building = dataRegistry.BuildingTemplates[buildingKey];

		var townManager = TownManager.Instance;
		if (!townManager.HasNecessaryResources(building.BuildingRequirements))
		{
			GD.Print("Not enough resources to build: " + buildingKey);
			player.CanMove = true;
			return;
		}
		player.PlayerCamera.Current = false;
		BuildSiteCamera.Current = true;
		townManager.SpendResources(building.BuildingRequirements);
		var ghostWrapper = new GhostBuilding();
		ghostWrapper.OnPlacementConfirmed = (Transform3D buildingTransform, List<Vector3I> requestedTiles) =>
		{
			Vector3 worldPos = buildingTransform.Origin;
			var worldRotation = buildingTransform.Basis.GetEuler();
			var buildingInstance = (Node3D)GD.Load<PackedScene>(building.ScenePath).Instantiate();
			this.AddChild(buildingInstance);
			buildingInstance.GlobalPosition = worldPos;
			buildingInstance.GlobalRotation = worldRotation;
			BuildGrid.PlaceBuilding(buildingKey, buildingInstance, requestedTiles);
			FindOwnedTiles();
			player.PlayerCamera.Current = true;
			BuildSiteCamera.Current = false;
			player.CanMove = true;

		};
		ghostWrapper.OnPlacementCancelled = () =>
		{
			player.PlayerCamera.Current = true;
			BuildSiteCamera.Current = false;
			player.CanMove = true;
		};
		GetParent().AddChild(ghostWrapper);
		ghostWrapper.Initialize(building, ownedTiles, BuildGrid);
	}

	private void FindOwnedTiles()
	{
		ownedTiles.Clear();
		var myBounds = GetVisualBounds();

		foreach (var tile in BuildGrid.tileStates.Values)
		{
			Vector3 worldPos = BuildGrid.GridToWorld(tile.Position);
			if (myBounds.HasPoint(worldPos) && !tile.IsOccupied)
			{
				ownedTiles.Add(tile.Position);
			}
		}
	}

	private Vector3I GetTopLeftTile()
	{
		int minX = int.MaxValue;
		int minZ = int.MaxValue;

		foreach (var tile in ownedTiles)
		{
			if (tile.X < minX) minX = tile.X;
			if (tile.Z < minZ) minZ = tile.Z;
		}

		return new Vector3I(minX, ownedTiles[0].Y, minZ);
	}

	private Aabb GetVisualBounds()
	{
		var shapeNode = GetNodeOrNull<CollisionShape3D>("CollisionShape3D");
		if (shapeNode != null && shapeNode.Shape is BoxShape3D boxShape)
		{
			var boxSize = boxShape.Size;
			var globalXform = shapeNode.GlobalTransform;
			var halfSize = boxSize / 2.0f;

			// Centered AABB
			var min = globalXform.Origin - halfSize;
			var max = globalXform.Origin + halfSize;
			var worldSize = max - min;

			return new Aabb(min, worldSize);
		}

		// Fallback default
		return new Aabb(GlobalTransform.Origin - new Vector3(1, 0.1f, 1), new Vector3(2, 0.2f, 2));
	}
}
