using Godot;
using System;

public partial class Chronos : Node
{
	[Export]
	public static Chronos Instance;
	[Export]
	public double CurrentTime = 0f;
	[Export]
	public int TimeInDay = 120;
	[Export]
	public int CurrentDay = 0;

	public Timer DayTimer;

	public override void _Ready()
	{
		Instance = this;
		DayTimer = new Timer();
		DayTimer.WaitTime = TimeInDay;
		DayTimer.Timeout += EndDay;
		DayTimer.OneShot = false;
		DayTimer.Autostart = false;
		AddChild(DayTimer);
		StartDay();
		GameSignalBus.Instance.Connect(GameSignalBus.SignalName.FadeOutFinished, Callable.From(StartDay));
	}

	public override void _PhysicsProcess(double delta)
	{
		// Update current time
		CurrentTime = TimeInDay - DayTimer.TimeLeft;

	}

	public void EndDay()
	{
		DayTimer.Paused = true;
		GameSignalBus.Instance.EmitSignal(GameSignalBus.SignalName.DayEnded);
	}

	public void StartDay()
	{
		CurrentDay += 1;
		CurrentTime = 0f;
		GameSignalBus.Instance.EmitSignal(GameSignalBus.SignalName.DayStarted);
		DayTimer.Paused = false;
		DayTimer.Start();
	}
}
