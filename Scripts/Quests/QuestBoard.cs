using Godot;
using System;

public partial class QuestBoard : Interactable
{
    [Export] public QuestMenu QuestMenu;

    public override void _Ready()
    {
        base._Ready();
        QuestMenu = GetTree().CurrentScene.GetNode<QuestMenu>("QuestMenu");
    }

    public override void Interact(Player player)
    {
        if (!playerInRange || !CanInteract) return;
        GD.Print("Interacting with the Quest Board");
        player.CanMove = false;
        CanInteract = false;
        QuestMenu.Open(player);
        QuestMenu.OnClose = () =>
        {
            CanInteract = true;
            player.CanMove = true;
        };
    }
}
