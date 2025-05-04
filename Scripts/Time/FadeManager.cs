using Godot;
using System;

public partial class FadeManager : Control
{

	private ColorRect FadeLayer;
	private AnimationPlayer animationPlayer;

	public override void _Ready()
	{
		FadeLayer = GetNode<ColorRect>("FadeLayer");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		GameSignalBus.Instance.Connect(GameSignalBus.SignalName.DayStarted, Callable.From(FadeIn));
		GameSignalBus.Instance.Connect(GameSignalBus.SignalName.DayEnded, Callable.From(FadeOut));
	}

	public void FadeOut()
	{
		animationPlayer.Play("fade_out");
	}

	public void FadeIn()
	{
		animationPlayer.Play("fade_in");
	}

	private void _on_animation_player_animation_finished(string name)
	{
		if (name == "fade_out")
		{
			GameSignalBus.Instance.EmitSignal(GameSignalBus.SignalName.FadeOutFinished);
		}
		else if (name == "fade_in")
		{
			GameSignalBus.Instance.EmitSignal(GameSignalBus.SignalName.FadeInFinished);
		}
	}
}
