using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using TownResources;

public class RoomGenerationContext
{
    public RoomTemplate Template;
    public RoomType Type;
    public Location Location;
    public RandomNumberGenerator Rng;
}


public partial class ExplorationManager : Node
{
    public static ExplorationManager Instance { get; private set; }

    public Location CurrentLocation { get; private set; }
    public List<GeneratedRoom> CurrentRun { get; private set; } = new();
    public int CurrentRoomIndex { get; private set; } = 0;

    public override void _Ready()
    {
        Instance = this;
    }

    public void StartRun(Location location)
    {
        CurrentLocation = location;
        CurrentRoomIndex = 0;
        CurrentRun = GenerateRun(location);
    }

    public GeneratedRoom GetCurrentRoom()
    {
        if (CurrentRoomIndex < 0 || CurrentRoomIndex >= CurrentRun.Count)
            return null;

        return CurrentRun[CurrentRoomIndex];
    }

    public bool AdvanceRoom()
    {
        if (CurrentRoomIndex < CurrentRun.Count - 1)
        {
            CurrentRoomIndex++;
            return true;
        }

        return false;
    }

    public bool IsRunComplete()
    {
        return CurrentRoomIndex >= CurrentRun.Count;
    }

    public List<GeneratedRoom> GenerateRun(Location location)
    {
        var run = new List<GeneratedRoom>();
        var rng = new RandomNumberGenerator();
        rng.Randomize();

        var layout = location.LayoutDefinition;
        var totalRoomCount = rng.RandiRange(layout.RoomCountMin, layout.RoomCountMax);
        int roomCount = totalRoomCount;

        // Reserve slots
        bool hasFirst = layout.FirstRoomOverride != null;
        bool hasLast = layout.LastRoomOverride != null;

        if (hasFirst) roomCount--;
        if (hasLast) roomCount--;

        var structure = layout.RoomSequence;
        var useSequence = structure.Count > 0;

        for (int i = 0; i < roomCount; i++)
        {
            RoomType type = useSequence ? structure[i % structure.Count] : GetRandomRoomType(rng);
            var template = PickTemplateForType(type, location);
            var ctx = new RoomGenerationContext
            {
                Template = template,
                Type = type,
                Location = location,
                Rng = rng
            };
            var room = CreateRoomFromTemplate(ctx);
            run.Add(room);
        }

        if (hasFirst)
        {
            var firstCtx = new RoomGenerationContext
            {
                Template = layout.FirstRoomOverride,
                Type = layout.FirstRoomOverride.RoomType,
                Location = location,
                Rng = rng
            };
            var first = CreateRoomFromTemplate(firstCtx);
            run.Insert(0, first);
        }

        if (hasLast)
        {
            var lastCtx = new RoomGenerationContext
            {
                Template = layout.LastRoomOverride,
                Type = layout.LastRoomOverride.RoomType,
                Location = location,
                Rng = rng
            };
            var last = CreateRoomFromTemplate(lastCtx);
            run.Add(last);
        }


        return run;
    }

    private RoomType GetRandomRoomType(RandomNumberGenerator rng)
    {
        var values = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().Where(rt => rt != RoomType.Random).ToList();
        return values[rng.RandiRange(0, values.Count - 1)];
    }

    private RoomTemplate PickTemplateForType(RoomType type, Location location)
    {
        var matches = location.UniqueRoomTemplates.Where(t => t.RoomType == type).ToList();
        if (matches.Count > 0)
        {
            var rng = new RandomNumberGenerator();
            rng.Randomize();
            return matches[rng.RandiRange(0, matches.Count - 1)];
        }

        // Fallback generic
        return new RoomTemplate
        {
            TemplateName = $"{type} Room",
            RoomType = type,
            IsUnique = false
        };
    }


    private GeneratedRoom CreateRoomFromTemplate(RoomGenerationContext ctx)
    {
        var room = new GeneratedRoom
        {
            TemplateUsed = ctx.Template,
            DisplayName = ctx.Template.TemplateName,
            Description = ctx.Template.Description,
            SceneToLoad = ctx.Template.RoomScene,
            Type = ctx.Type,
            IsUnique = ctx.Template.IsUnique
        };

        switch (ctx.Type)
        {
            case RoomType.Combat:
                var monsterCount = ctx.Rng.RandiRange(1, 3);
                for (int i = 0; i < monsterCount && ctx.Location.MonsterPool.Count > 0; i++)
                {
                    var monster = ctx.Location.MonsterPool[ctx.Rng.RandiRange(0, ctx.Location.MonsterPool.Count - 1)];
                    room.Monsters.Add(monster);
                }
                break;

            case RoomType.ResourceNode:
                if (ctx.Location.ResourcePool.Count > 0)
                {
                    room.ResourceNode = GenerateResourceNode(ctx.Location.ResourcePool, ctx.Rng);


                }
                break;

            case RoomType.Treasure:
                if (ctx.Location.ItemPool.Count > 0)
                {
                    room.Item = ctx.Location.ItemPool[ctx.Rng.RandiRange(0, ctx.Location.ItemPool.Count - 1)];
                }
                break;
        }

        return room;
    }

    private ResourceNodeDefinition GenerateResourceNode(Array<WeightedResource> pool, RandomNumberGenerator rng)
    {
        var node = new ResourceNodeDefinition
        {
            NodeName = "Generic Resource Node",
            InteractionsAllowed = rng.RandiRange(2, 6),
            Contents = new Array<WeightedResource>(pool)
        };

        return node;
    }


}
