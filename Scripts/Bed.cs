using Godot;
using System;

public partial class Bed : Interactable, IInteractable
{
	[Export] public Chronos chronos;

	public override void Interact(Player interactor)
	{
		if (playerInRange)
		{
			GD.Print("Interacting with the bed.");
			chronos.EndDay();
		}
	}
}
