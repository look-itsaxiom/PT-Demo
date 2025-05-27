using Godot;
using System;
using Godot.Collections;

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
            GD.Print("Exiting Town");
            ExplorationManager.Instance.PartyMembers = new Array<CharacterData.Character> { player.PlayerCharacter };
            GetTree().ChangeSceneToFile("res://Scenes/WorldSelect.tscn");
        }
    }
}
