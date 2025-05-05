using Godot;
using Godot.Collections;

namespace ChronosSpace
{
	public partial class ChronosTimeHook : Resource
	{
		public int TriggerDay;
		public double TriggerTime;
		public string TriggerName;
		public string ReturnSignal;
		public Array<Variant> ReturnSignalArgs = new();
	}

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

		public Array<ChronosTimeHook> TimeHooks = new();

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
			GameSignalBus.Instance.Connect(GameSignalBus.SignalName.RegisterTimeHook, Callable.From<ChronosTimeHook>(RegisterTimeHook));
		}

		private void RegisterTimeHook(ChronosTimeHook hook)
		{
			TimeHooks.Add(hook);
		}

		public override void _PhysicsProcess(double delta)
		{
			// Update current time
			CurrentTime = TimeInDay - DayTimer.TimeLeft;
			foreach (var timeHook in TimeHooks)
			{

				if ((CurrentDay == timeHook.TriggerDay && CurrentTime >= timeHook.TriggerTime) || CurrentDay > timeHook.TriggerDay)
				{
					GameSignalBus.Instance.EmitSignal(timeHook.ReturnSignal, timeHook.ReturnSignalArgs);
					TimeHooks.Remove(timeHook);
				}
			}
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
}
