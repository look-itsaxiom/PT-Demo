using CharacterData;
using Godot;
using System;
using Godot.Collections;

public partial class CharacterInfoMenu : Control
{
    private Label nameLabel;
    private Label classLabel;
    private Label raceLabel;
    private Label levelLabel;
    private Dictionary<Stat.StatKey, Label> statValueLabels = new Dictionary<Stat.StatKey, Label>();
    private Dictionary<Stat.StatKey, Label> statGrowthRateLabels = new Dictionary<Stat.StatKey, Label>();
    public Func<bool> OnClose { get; internal set; }


    public override void _Ready()
    {
        nameLabel = GetNode<Label>("Panel/NamePanel/NameLabel");
        classLabel = GetNode<Label>("Panel/RaceClassPanel/ClassLabel");
        raceLabel = GetNode<Label>("Panel/RaceClassPanel/RaceLabel");
        levelLabel = GetNode<Label>("Panel/StatPanel/LevelLabel");
        foreach (Stat.StatKey key in Enum.GetValues(typeof(Stat.StatKey)))
        {
            statValueLabels[key] = GetNode<Label>($"Panel/StatPanel/{key}/{key}Value");
            statGrowthRateLabels[key] = GetNode<Label>($"Panel/StatPanel/{key}/{key}GrowthLabel");
        }
    }

    public void ShowCharacterInfo(Character character)
    {
        nameLabel.Text = character.CharacterName;
        classLabel.Text = character.Class.Name;
        raceLabel.Text = character.Race.Name;
        levelLabel.Text = character.Level.ToString();
        foreach (var key in statValueLabels.Keys)
        {
            statValueLabels[key].Text = character.CurrentStats[key].Value.ToString();
            statGrowthRateLabels[key].Text = character.GrowthRates[key].GrowthRateSymbol;
        }

        Visible = true;
    }

    public void HideCharacterInfo()
    {
        Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            HideCharacterInfo();
            OnClose?.Invoke();
        }
    }
}
