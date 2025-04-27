using Godot;
using System;

public partial class Chronos : Node3D
{
	public double currentTime = 0f;
	[Export] public int timeInDay = 120;
	[Export] public int currentDay = 0;
	public float shadowShift = 4.5f;

	public Timer timer;

	public DirectionalLight3D sun;

	public FadeManager fadeManager;

	public Label clock;
	public Label fps;

	public override void _Ready()
	{
		sun = GetNode<DirectionalLight3D>("Helios");
		fadeManager = GetNode<FadeManager>("../FadeManager");
		clock = GetNode<Label>("Clock");
		fps = GetNode<Label>("FPS");
		timer = new Timer();
		timer.WaitTime = timeInDay;
		timer.Timeout += EndDay;
		timer.OneShot = false;
		timer.Autostart = false;
		AddChild(timer);
		StartDay();
		fadeManager.FadeOutFinished += StartDay;
	}

	public override void _PhysicsProcess(double delta)
	{
		// Update current time
		currentTime = timeInDay - timer.TimeLeft;

		// Calculate the left and right parts of the clock
		int left = (int)(currentTime / 10); // Increment every 10 seconds
		int right = (int)(currentTime % 10); // Count from 00 to 09

		clock.Text = $"Day: {currentDay} - {left}:{right:00}";

		// Calculate sun rotation based on time progression
		if (sun != null)
		{
			float sunRotation = (float)(currentTime / timeInDay) * 180.0f; // Full rotation over a day
			sun.RotationDegrees = new Vector3(sunRotation - 90.0f, sun.RotationDegrees.Y, sun.RotationDegrees.Z);
		}
	}

	public override void _Process(double delta)
	{
		if (Engine.GetFramesPerSecond() != 0)
		{
			fps.Text = $"FPS: {Engine.GetFramesPerSecond()}";
		}
	}

	public void EndDay()
	{
		var newRotation = RotationDegrees.Y + shadowShift > 360f ? 0 : RotationDegrees.Y + shadowShift;
		RotationDegrees = new Vector3(RotationDegrees.X, newRotation, this.RotationDegrees.Z); // Example rotation change
		timer.Paused = true;
		fadeManager.FadeOut();
	}

	public void StartDay()
	{
		currentDay += 1;
		currentTime = 0f;
		fadeManager.FadeIn();
		timer.Paused = false;
		timer.Start();
	}
}
