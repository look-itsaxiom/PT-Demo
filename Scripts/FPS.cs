using Godot;
using System;

public partial class FPS : Label
{
    public override void _Ready()
    {
        Text = $"FPS: {Engine.GetFramesPerSecond()}";
    }

    public override void _Process(double delta)
    {
        if (Engine.GetFramesPerSecond() != 0)
        {
            Text = $"FPS: {Engine.GetFramesPerSecond()}";
        }
    }

}
