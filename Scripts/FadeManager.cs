using Godot;
using System;

public partial class FadeManager : Control
{
	private ColorRect FadeLayer;
	private AnimationPlayer animationPlayer;

	[Signal] public delegate void FadeOutFinishedEventHandler();
	[Signal] public delegate void FadeInFinishedEventHandler();

	public override void _Ready()
	{
		FadeLayer = GetNode<ColorRect>("FadeLayer");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
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
			EmitSignal(SignalName.FadeOutFinished);
		}
		else if (name == "fade_in")
		{
			EmitSignal(SignalName.FadeInFinished);
		}
	}
}
