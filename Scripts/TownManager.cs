using Godot;
using System;

public partial class TownManager : Node
{
	public int TownLevel { get; set; } = 1;
	public int TownLevelMax { get; set; } = 2;
	public int Gold { get; set; } = 100;
	public int Wood { get; set; } = 50;
	public int Stone { get; set; } = 10;
	public int Food { get; set; } = 10;
}
