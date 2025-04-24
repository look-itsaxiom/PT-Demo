using Characters;
using Godot;
using Godot.Collections;

public partial class JsonToResourceTool : Node
{
    public override void _Ready()
    {
        GenerateRaces();
        GenerateClasses();
    }

    private void GenerateRaces()
    {
        var jsonPath = "res://Data/Characters/races.json";
        var targetFolder = "res://Data/Characters/Races/";

        var jsonFile = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read);
        var jsonText = jsonFile.GetAsText();
        var raceDict = Json.ParseString(jsonText).AsGodotDictionary();

        foreach (var raceKey in raceDict.Keys)
        {
            var raceData = raceDict[raceKey].AsGodotDictionary();
            var resource = new RaceData
            {
                Name = raceData["name"].ToString(),
                Description = raceData["description"].ToString(),
                BaseStats = new Dictionary<Stat.StatKey, StatRange>()
            };

            var stats = raceData["baseStats"].AsGodotDictionary();
            foreach (var statKey in stats.Keys)
            {
                var statData = stats[statKey].AsGodotDictionary();
                resource.BaseStats[Stat.StatKeyNamesReverse[statKey.ToString()]] = new StatRange(
                (int)statData["min"], (int)statData["max"]);
            }

            ResourceSaver.Save(resource, $"{targetFolder}{raceKey}.tres");
        }

        GD.Print("Race resources generated!");
    }

    private void GenerateClasses()
    {
        var jsonPath = "res://Data/Characters/classes.json";
        var targetFolder = "res://Data/Characters/Classes/";

        var jsonFile = FileAccess.Open(jsonPath, FileAccess.ModeFlags.Read);
        var jsonText = jsonFile.GetAsText();
        var classDict = Json.ParseString(jsonText).AsGodotDictionary();

        foreach (var classKey in classDict.Keys)
        {
            var classData = classDict[classKey].AsGodotDictionary();
            var resource = new ClassData
            {
                Name = classData["name"].ToString(),
                Description = classData["description"].ToString(),
                tier = (int)classData["tier"],
                Requirements = (string[])classData["requirements"] ?? System.Array.Empty<string>(),
                Modifiers = new Dictionary<Stat.StatKey, float>()
            };

            var stats = classData["modifiers"].AsGodotDictionary();
            foreach (var statKey in stats.Keys)
            {
                var modifier = stats[statKey];
                resource.Modifiers[Stat.StatKeyNamesReverse[statKey.ToString()]] = (float)modifier;
            }

            ResourceSaver.Save(resource, $"{targetFolder}{classKey}.tres");
        }

        GD.Print("Race resources generated!");
    }
}

