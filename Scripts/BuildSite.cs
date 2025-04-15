using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;

public partial class BuildSite : Interactable, IInteractable
{
	[Export] public Vector2 BuildSiteSize;

	[Export] public BuildMenu buildMenu;

	[Export] public BuildGrid buildGrid;

	public DataRegistry dataRegistry;

	public List<Vector2I> ownedTiles = new();

	public override void _Ready()
	{
		buildMenu = GetNode<BuildMenu>("../BuildMenu");
		dataRegistry = GetNode<DataRegistry>("/root/DataRegistry");
		buildGrid = GetNode<BuildGrid>("../BuildGrid");
		base._Ready();
		FindOwnedTiles();
		GD.Print("BuildSite owns " + ownedTiles.Count + " tiles.");
	}

	public override void Interact(Player interactor)
	{
		if (!playerInRange) return;

		GD.Print("Interacting with the Build Site");
		buildMenu.OnBuildSelected = OnBuildingChosen;
		buildMenu.Open();
	}

	public override void OnBodyExited(Node3D body)
	{
		base.OnBodyExited(body);
		if (!playerInRange && buildMenu.Visible)
		{
			buildMenu.Close();
		}
	}

	private void OnBuildingChosen(string buildingKey)
	{
		var building = dataRegistry.buildingTemplates[buildingKey];
		GD.Print("Building Scene: " + building.Name);
		var buildingInstance = (Node3D)GD.Load<PackedScene>(building.ScenePath).Instantiate();
		buildingInstance.Transform = this.GlobalTransform;
		GetParent().AddChild(buildingInstance);
		GD.Print(building.Name + " built!");
		QueueFree();
	}

	private void FindOwnedTiles()
	{
		var myBounds = GetVisualBounds();

		foreach (var tile in buildGrid.tileStates.Keys)
		{
			Vector3 worldPos = buildGrid.GridToWorld(tile);
			if (myBounds.HasPoint(worldPos))
			{
				ownedTiles.Add(tile);
			}
		}
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
