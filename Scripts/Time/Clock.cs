using Godot;
using System;

public partial class Clock : Label
{
    public override void _PhysicsProcess(double delta)
    {
        // Calculate the left and right parts of the clock
        int left = (int)(Chronos.Instance.CurrentTime / 10); // Increment every 10 seconds
        int right = (int)(Chronos.Instance.CurrentTime % 10); // Count from 00 to 09

        Text = $"Day: {Chronos.Instance.CurrentDay} - {left}:{right:00}";
    }

}
