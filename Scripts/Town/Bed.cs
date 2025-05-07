using Godot;
using System;
using ChronosSpace;

public partial class Bed : Interactable, IInteractable
{
	public override void Interact(Player interactor)
	{
		if (playerInRange)
		{
			GD.Print("Interacting with the bed.");
			Chronos.Instance.EndDay();
		}
	}
}
