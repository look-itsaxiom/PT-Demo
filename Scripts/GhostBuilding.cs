using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GhostBuilding : Node3D
{
    public Vector2I GridSize;
    public List<Vector3I> AllowedTiles;
    public BuildGrid BuildGrid;
    public Action<Transform3D, List<Vector3I>> OnPlacementConfirmed;
    public Action OnPlacementCancelled;

    private Vector3I _pivotTile;
    private List<Vector3I> _tileFootprint;
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

        // handles the center position nature of the grid for visual appeal
        GridSize = building.GridSize + new Vector2I(1, 1);
        AllowedTiles = allowedTiles;
        BuildGrid = buildGrid;

        _pivotTile = GetOriginTile();
        _rotation = 0;
        GlobalPosition = BuildGrid.GridToWorld(_pivotTile);
        _tileFootprint = GetRequestedFootprint();
        BuildGrid.PaintBuildingFootprint(_tileFootprint, AllowedTiles);
    }

    private Vector3I GetOriginTile()
    {

        return AllowedTiles[0] - new Vector3I(GridSize.X, 0, 0);
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
            _pivotTile += move;
            ApplyMovement();
        }

        if (@event.IsActionPressed("rotate"))
        {
            _rotation = (_rotation + 90) % 360;
            RotationDegrees = new Vector3(0, _rotation, 0);
            ApplyMovement();
        }

        if (@event.IsActionPressed("ui_accept"))
        {
            if (BuildGrid.CanPlaceBuilding(_tileFootprint))
            {
                OnPlacementConfirmed?.Invoke(this.Transform, _tileFootprint);
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

    private void ApplyMovement()
    {
        GlobalPosition = BuildGrid.GridToWorld(_pivotTile);
        _tileFootprint = GetRequestedFootprint();
        BuildGrid.PaintBuildingFootprint(_tileFootprint, AllowedTiles);
    }

    private List<Vector3I> GetRequestedFootprint()
    {
        int w = GridSize.X;
        int h = GridSize.Y;

        List<Vector3I> requestedTiles = new List<Vector3I>();

        switch (_rotation % 360)
        {
            case 0:
                for (int x = 0; x < w; x++)
                {
                    for (int z = 0; z < h; z++)
                    {
                        requestedTiles.Add(_pivotTile + new Vector3I(x, 0, z));
                    }
                }
                break;
            case 90:
                for (int x = 0; x < h; x++)
                {
                    for (int z = 0; z < w; z++)
                    {
                        requestedTiles.Add(_pivotTile + new Vector3I(x, 0, -z));
                    }
                }
                break;
            case 180:
                for (int x = 0; x < w; x++)
                {
                    for (int z = 0; z < h; z++)
                    {
                        requestedTiles.Add(_pivotTile + new Vector3I(-x, 0, -z));
                    }
                }
                break;
            case 270:
                for (int x = 0; x < h; x++)
                {
                    for (int z = 0; z < w; z++)
                    {
                        requestedTiles.Add(_pivotTile + new Vector3I(-x, 0, z));
                    }
                }
                break;
        }

        return requestedTiles;
    }
}
