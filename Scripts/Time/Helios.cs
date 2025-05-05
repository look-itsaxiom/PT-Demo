using Godot;
using System;
using ChronosSpace;

public partial class Helios : DirectionalLight3D
{
    public float ShadowShift = 4.5f;

    public override void _PhysicsProcess(double delta)
    {
        // Update current time
        var currentTime = Chronos.Instance.CurrentTime;
        var timeInDay = Chronos.Instance.TimeInDay;

        // Calculate sun rotation based on time progression
        float sunRotation = (float)(currentTime / timeInDay) * 180.0f; // Full rotation over a day
        RotationDegrees = new Vector3(sunRotation - 90.0f, RotationDegrees.Y, RotationDegrees.Z);

        // Update the light color based on the time of day
        if (currentTime < timeInDay / 2)
        {
            // Morning to noon
            LightColor = new Color(1.0f, 0.8f, 0.5f); // Warm light
        }
        else
        {
            // Noon to evening
            LightColor = new Color(1.0f, 0.5f, 0.5f); // Cooler light
        }
    }

    public void OnDayEnd()
    {
        var newRotation = RotationDegrees.Y + ShadowShift > 360f ? 0 : RotationDegrees.Y + ShadowShift;
        RotationDegrees = new Vector3(RotationDegrees.X, newRotation, this.RotationDegrees.Z);
    }
}
