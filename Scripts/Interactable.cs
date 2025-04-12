using Godot;
using System;

public abstract partial class Interactable : Area3D, IInteractable
{
	public bool playerInRange;
	public Player Player { get; set; }
	public string ObjectName { get; set; }

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyExited(Node3D body)
	{
		if (body.IsInGroup("Player"))
		{
			playerInRange = false;
			Player = null;
		}
	}


	private void OnBodyEntered(Node3D body)
	{
		if (body.IsInGroup("Player"))
		{
			playerInRange = true;
			Player = body as Player;
			Player.interactTarget = this;
		}
	}

	public bool CanInteract() => playerInRange;

	public void Interact(Player player)
	{
		GD.Print("Interacting with the interactable: " + this.ObjectName);
	}
}
