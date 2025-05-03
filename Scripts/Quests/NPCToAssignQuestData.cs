using CharacterData;
using Godot;
using System;

public partial class NPCToAssignQuestData : Button
{
    [Export]
    public Character AssignedCharacter { get; set; }

    public Action<Character> OnCharacterSelected;

    private Label CharacterNameLabel;
    private Label CharacterClassLabel;
    private Label NPCLevelLabel;

    public void Initialize(Character character)
    {
        CharacterNameLabel = GetNode<Label>("NPCName");
        CharacterClassLabel = GetNode<Label>("NPCClass");
        NPCLevelLabel = GetNode<Label>("NPCLevel");
        this.Pressed += () => OnCharacterSelected?.Invoke(AssignedCharacter);
        if (character == null)
        {
            GD.PrintErr("Character is null");
            return;
        }
        GD.Print($"Initializing character panel with character: {character.CharacterName}");
        AssignedCharacter = character;
        CharacterNameLabel.Text = AssignedCharacter.CharacterName;
        CharacterClassLabel.Text = AssignedCharacter.Class.Name;
        NPCLevelLabel.Text = $"Level: {AssignedCharacter.Level}";
    }
}
