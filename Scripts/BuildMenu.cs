using Godot;
using System;

public partial class BuildMenu : Control
{
	public Action<string> OnBuildSelected;

	private Button guildHallButton;

	public override void _Ready()
	{
		guildHallButton = GetNode<Button>("VBoxContainer/GuildHallButton");
		guildHallButton.Pressed += () => HandleSelection("GuildHall");

		this.VisibilityChanged += () => { if (Visible) guildHallButton.GrabFocus(); };
	}

	public void Open()
	{
		Visible = true;
		SetProcessInput(true);
	}

	public void Close()
	{
		Visible = false;
		SetProcessInput(false);
	}

	private void HandleSelection(string buildingKey)
	{
		GD.Print("Selected building: " + buildingKey);
		OnBuildSelected?.Invoke(buildingKey);
		Close();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_cancel"))
		{
			Close();
		}
	}

}
