using Godot;
using System;
using System.Drawing;

public partial class BuildSite : Interactable, IInteractable
{
	[Export] public Vector2 BuildSiteSize;

	[Export] public BuildMenu buildMenu;

	public DataRegistry dataRegistry;

	private Vector2 MeshSize = new Vector2(4, 4);

	public override void _Ready()
	{
		buildMenu = GetNode<BuildMenu>("../BuildMenu");
		dataRegistry = GetNode<DataRegistry>("/root/DataRegistry");
		base._Ready();
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
		var buildingInstance = (Node3D)building.BuildingScene.Instantiate();
		buildingInstance.Transform = this.GlobalTransform;
		GetParent().AddChild(buildingInstance);
		GD.Print(building.Name + " built!");
		QueueFree();
	}
}
