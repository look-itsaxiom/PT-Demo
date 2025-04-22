using Godot;
using System;
using System.Collections.Generic;

public partial class GhostBuilding : Node3D
{
    public Vector2I GridSize;
    public List<Vector3I> AllowedTiles;
    public BuildGrid BuildGrid;
    public Action<Vector3I> OnPlacementConfirmed;
    public Action OnPlacementCancelled;

    private Vector3I _currentTile;
    private int _rotation = 0;

    public void Initialize(Building building, List<Vector3I> allowedTiles, BuildGrid buildGrid)
    {
        var buildingScene = GD.Load<PackedScene>(building.ScenePath);
        var buildingInstance = (Node3D)buildingScene.Instantiate();
        AddChild(buildingInstance);

        foreach (var child in buildingInstance.GetChildren())
        {
            if (child is MeshInstance3D mesh)
            {
                if (mesh is not null)
                {
                    var material = mesh.GetActiveMaterial(0).Duplicate() as StandardMaterial3D;
                    material.AlbedoColor = new Color(1, 1, 1, 0.5f);
                    material.Transparency = BaseMaterial3D.TransparencyEnum.Alpha;

                    mesh.SetSurfaceOverrideMaterial(0, material);
                }
            }
        }

        GridSize = building.GridSize;
        AllowedTiles = allowedTiles;
        BuildGrid = buildGrid;

        _currentTile = allowedTiles[0];
        GlobalPosition = BuildGrid.GridToWorld(_currentTile);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        Vector3I move = Vector3I.Zero;

        if (@event.IsActionPressed("ui_up"))
            move.Z += 1;
        if (@event.IsActionPressed("ui_down"))
            move.Z -= 1;
        if (@event.IsActionPressed("ui_left"))
            move.X += 1;
        if (@event.IsActionPressed("ui_right"))
            move.X -= 1;

        if (move != Vector3I.Zero)
        {
            var newTile = _currentTile + move;
            _currentTile = newTile;
            GlobalPosition = BuildGrid.GridToWorld(_currentTile);
            GD.Print($"Current tile: {_currentTile}");
        }

        // if (@event.IsActionPressed("rotate"))
        // {
        //     _rotation = (_rotation + 270) % 360;
        //     RotationDegrees = new Vector3(0, _rotation, 0);
        // }

        if (@event.IsActionPressed("ui_accept"))
        {
            if (BuildGrid.CanPlaceBuilding(_currentTile, GridSize))
            {
                OnPlacementConfirmed?.Invoke(_currentTile);
                QueueFree();
            }
            else
            {
                GD.Print("Cannot place building: not enough space");
            }

        }

        if (@event.IsActionPressed("ui_cancel"))
        {
            OnPlacementCancelled?.Invoke();
            QueueFree();
        }
    }
}
