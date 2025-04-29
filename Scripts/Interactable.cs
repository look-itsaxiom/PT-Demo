using Godot;
using System;

public abstract partial class Interactable : Area3D, IInteractable
{
	public bool playerInRange;
	public Player Player { get; set; }
	public string ObjectName { get; set; }
	public bool CanInteract { get; set; } = true;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	public virtual void OnBodyExited(Node3D body)
	{
		if (body.IsInGroup("Player"))
		{
			playerInRange = false;
			Player.interactTarget = null;
			Player = null;
		}
	}

	public virtual void OnBodyEntered(Node3D body)
	{
		if (body.IsInGroup("Player"))
		{
			playerInRange = true;
			Player = body as Player;
			Player.interactTarget = this;
		}
	}

	public virtual void Interact(Player player)
	{
		GD.Print("Interacting with the interactable: " + this.ObjectName);
	}
}
