using Godot;
using System;
using System.Drawing;

public partial class BuildSite : Interactable, IInteractable
{
	[Export] public Vector2 BuildSiteSize;

	private Vector2 MeshSize = new Vector2(4, 4);

	public new void Interact(Player interactor)
	{
		if (playerInRange)
		{
			GD.Print("Interacting with this Build Site");
			var DataRegistry = GetNode<DataRegistry>("/root/DataRegistry");
			var building = DataRegistry.buildingTemplates["GuildHall"];
			GD.Print("Building Scene: " + building.Name);
			var buildingInstance = (Node3D)building.BuildingScene.Instantiate();
			buildingInstance.Transform = this.GlobalTransform;
			GetParent().AddChild(buildingInstance);
			GD.Print(building.Name + " built!");
			QueueFree();
		}
	}
}
