using CharacterData;
using Godot;
using System;

public partial class InteractNPC : Interactable
{
    public Character Character { get; set; }
    public TownNPC TownNPC { get; set; }
    public CharacterInfoMenu CharacterInfoMenu { get; set; }

    public override void _Ready()
    {
        base._Ready();
        // Initialize any additional properties or nodes here
        TownNPC = GetParent<TownNPC>();
        Character = TownNPC.MyCharacter;
        ObjectName = Character.CharacterName;
    }

    public override void Interact(Player player)
    {
        // Implement interaction logic here
        if (!playerInRange || !CanInteract) return;
        GD.Print("Interacting with NPC: " + Character.CharacterName);
        TownNPC.IsInteracting = true;
        TownNPC.LookAt(player.GlobalPosition, Vector3.Up, true);
        TownNPC.AnimationPlayer.Play("Interact");
        player.CanMove = false;
        CharacterInfoMenu = GetTree().CurrentScene.GetNode<CharacterInfoMenu>("CharacterInfoMenu");
        CharacterInfoMenu.ShowCharacterInfo(Character);
        CharacterInfoMenu.OnClose += () =>
        {
            TownNPC.AnimationPlayer.Play("Cheer");
            player.CanMove = true;
            TownNPC.IsInteracting = false;
            return true;
        };
    }
}
