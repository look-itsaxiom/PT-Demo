using Godot;
using System;

public class Building
{
    public string Name { get; set; }
    public Vector2 Size { get; set; }

    public PackedScene BuildingScene { get; set; }

    public Building(string name, int sizeX, int sizeY, string scenePath)
    {
        Name = name;
        Size = new Vector2(sizeX, sizeY);
        BuildingScene = GD.Load<PackedScene>(scenePath);
    }
}
