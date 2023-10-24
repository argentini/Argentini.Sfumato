namespace Argentini.Sfumato.ScssUtilityCollections.Entities;

public sealed class ScssUtilityClassGroup
{
    public string SelectorPrefix { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public ConcurrentBag<ScssUtilityClass> Classes { get; } = new();

    /// <summary>
    /// Add a dictionary of classes.
    /// </summary>
    /// <param name="classes"></param>
    /// <param name="scssTemplate"></param>
    /// <param name="sortSeed"></param>
    public async Task<int> AddClassesAsync(Dictionary<string,string> classes, string scssTemplate, int sortSeed)
    {
        foreach (var (key, value) in classes)
        {
            Classes.Add(new ScssUtilityClass
            {
                Selector = $"{SelectorPrefix}{(string.IsNullOrEmpty(key) ? string.Empty : $"-{key}")}",
                SortOrder = sortSeed++,
                Category = Category,
                Value = value,
                ScssTemplate = scssTemplate
            });
        }

        return await Task.FromResult(sortSeed);
    }

    /// <summary>
    /// Add a dictionary of vanity class groups (e.g. "uppercase", "lowercase", etc.)
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="classes"></param>
    /// <param name="scssTemplate"></param>
    /// <param name="sortSeed"></param>
    public static async Task<int> AddVanityClassGroups(Dictionary<string, ScssUtilityClassGroup> collection, Dictionary<string,string> classes, string scssTemplate, int sortSeed)
    {
        foreach (var (key, value) in classes)
        {
            var scssUtilityClassGroup = new ScssUtilityClassGroup
            {
                SelectorPrefix = key
            };

            sortSeed = await scssUtilityClassGroup.AddClassesAsync(
                new Dictionary<string, string>
                {
                    [""] = value
                },
                scssTemplate,
                sortSeed
            );

            if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        }
        
        return await Task.FromResult(sortSeed);
    }
    
    /// <summary>
    /// Add an arbitrary value option (e.g. text-[#08ffcd]).
    /// </summary>
    /// <param name="valueTypes"></param>
    /// <param name="scssTemplate"></param>
    /// <param name="sortSeed"></param>
    public async Task<int> AddAbitraryValueClassAsync(string valueTypes, string scssTemplate, int sortSeed)
    {
        Classes.Add(new ScssUtilityClass
        {
            Selector = $"{SelectorPrefix}-",
            SortOrder = sortSeed++,
            Category = Category,
            ArbitraryValueTypes = valueTypes.Split(','),
            ScssTemplate = scssTemplate
        });

        return await Task.FromResult(sortSeed);
    }
    
    /// <summary>
    /// Add an arbitrary value option (e.g. "text-base/").
    /// </summary>
    /// <param name="suffix"></param>
    /// <param name="valueTypes"></param>
    /// <param name="scssTemplate"></param>
    /// <param name="sortSeed"></param>
    public async Task<int> AddAbitrarySlashValueClassAsync(string suffix, string valueTypes, string scssTemplate, int sortSeed)
    {
        Classes.Add(new ScssUtilityClass
        {
            Selector = $"{SelectorPrefix}{(string.IsNullOrEmpty(suffix) ? string.Empty : $"-{suffix}")}/",
            SortOrder = sortSeed++,
            Category = Category,
            ArbitraryValueTypes = valueTypes.Split(','),
            ScssTemplate = scssTemplate
        });

        return await Task.FromResult(sortSeed);
    }
}