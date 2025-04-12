using Godot;
using System;

public partial class Bed : Interactable, IInteractable
{
	[Export] public Chronos chronos;

	public new void Interact(Player interactor)
	{
		if (playerInRange)
		{
			GD.Print("Interacting with the bed.");
			chronos.EndDay();
		}
	}
}