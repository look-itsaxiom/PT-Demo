using Godot;
using System;

public partial class TownExit : Interactable
{
    public GameSignalBus GameSignalBus;

    public override void _Ready()
    {
        GameSignalBus = GameSignalBus.Instance;
        base._Ready();
    }

    public override void Interact(Player player)
    {
        if (player != null && CanInteract)
        {
            GameSignalBus.EmitSignal(GameSignalBus.SignalName.ResourceCollected, "Wood", 1);
        }
    }
}
