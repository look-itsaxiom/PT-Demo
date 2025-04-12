using Godot;
using System;

public partial class BuildSite : Interactable, IInteractable
{
	public new void Interact(Player interactor)
	{
		if (playerInRange)
		{
			GD.Print("Interacting with Guild Hall Build Site");
		}
	}
}
