using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class AccessibilityCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static void AddAllAccessibilityClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection, List<Task> tasks)
    {
        tasks.Add(collection.AddScreenReadersGroupAsync());
    }
    
    public static async Task AddScreenReadersGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["sr-only"] = ""
            },
            """
            position: absolute;
            width: 1px;
            height: 1px;
            padding: 0;
            margin: -1px;
            overflow: hidden;
            clip: rect(0, 0, 0, 0);
            white-space: nowrap;
            border-width: 0;
            """
        );
        
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["not-sr-only"] = ""
            },
            """
            position: static;
            width: auto;
            height: auto;
            padding: 0;
            margin: 0;
            overflow: visible;
            clip: auto;
            white-space: normal;
            """
        );
    }
}