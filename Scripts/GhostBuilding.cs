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

        _currentTile = GetStartingTile();
        GlobalPosition = BuildGrid.GridToWorld(_currentTile);
    }

    private Vector3I GetStartingTile()
    {
        int minX = int.MaxValue;
        int minZ = int.MaxValue;
        int y = 0;

        foreach (var tile in AllowedTiles)
        {
            if (tile.X < minX) minX = tile.X;
            if (tile.Z < minZ) minZ = tile.Z;
            y = tile.Y;
        }
        return new Vector3I(minX, y, minZ);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        Vector3I move = Vector3I.Zero;

        if (@event.IsActionPressed("ui_up"))
            move.Z -= 1;
        if (@event.IsActionPressed("ui_down"))
            move.Z += 1;
        if (@event.IsActionPressed("ui_left"))
            move.X -= 1;
        if (@event.IsActionPressed("ui_right"))
            move.X += 1;

        if (move != Vector3I.Zero)
        {
            var newTile = _currentTile + move;

            if (IsValidMove(newTile))
            {
                _currentTile = newTile;
                GlobalPosition = BuildGrid.GridToWorld(_currentTile);
            }
        }

        // if (@event.IsActionPressed("rotate_left"))
        // {
        //     _rotation = (_rotation + 270) % 360;
        //     RotationDegrees = new Vector3(0, _rotation, 0);
        // }

        // if (@event.IsActionPressed("rotate_right"))
        // {
        //     _rotation = (_rotation + 90) % 360;
        //     RotationDegrees = new Vector3(0, _rotation, 0);
        // }

        if (@event.IsActionPressed("interact"))
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

    private bool IsValidMove(Vector3I newTile)
    {
        if (!AllowedTiles.Contains(newTile))
        {
            return false;
        }

        var afterMoveOccupiedTiles = GetFootprintTiles(newTile);
        foreach (var tile in afterMoveOccupiedTiles)
        {
            if (!AllowedTiles.Contains(tile))
                return false;
        }
        return true;
    }

    private List<Vector3I> GetFootprintTiles(Vector3I newTile)
    {
        var tiles = new List<Vector3I>();
        for (int x = 0; x < GridSize.X; x++)
            for (int z = 0; z < GridSize.Y; z++)
            {
                var tile = new Vector3I(newTile.X + x, newTile.Y, newTile.Z + z);
                tiles.Add(tile);
            }
        return tiles;
    }

}
