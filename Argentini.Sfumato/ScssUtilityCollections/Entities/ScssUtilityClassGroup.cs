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
    public async Task AddClassesAsync(Dictionary<string,string> classes, string scssTemplate)
    {
        foreach (var (key, value) in classes)
        {
            Classes.Add(new ScssUtilityClass
            {
                Selector = $"{SelectorPrefix}{(string.IsNullOrEmpty(key) ? string.Empty : $"-{key}")}",
                Value = value,
                ScssTemplate = scssTemplate
            });
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// Add a dictionary of classes, injecting the prefix before each key.
    /// </summary>
    /// <param name="classes"></param>
    /// <param name="prefix"></param>
    /// <param name="scssTemplate"></param>
    public async Task AddClassesWithPrefixAsync(Dictionary<string,string> classes, string prefix, string scssTemplate)
    {
        foreach (var (key, value) in classes)
        {
            Classes.Add(new ScssUtilityClass
            {
                Selector = $"{SelectorPrefix}-{prefix}{(string.IsNullOrEmpty(key) ? string.Empty : $"-{key}")}",
                Value = value,
                ScssTemplate = scssTemplate
            });
        }

        await Task.CompletedTask;
    }
    
    /// <summary>
    /// Add a dictionary of vanity class groups (e.g. "uppercase", "lowercase", etc.)
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="classes"></param>
    /// <param name="scssTemplate"></param>
    public static async Task AddVanityClassGroups(ConcurrentDictionary<string, ScssUtilityClassGroup> collection, Dictionary<string,string> classes, string scssTemplate)
    {
        foreach (var (key, value) in classes)
        {
            var scssUtilityClass = new ScssUtilityClassGroup
            {
                SelectorPrefix = key
            };

            await scssUtilityClass.AddClassesAsync(
                new Dictionary<string, string>
                {
                    [""] = value
                },
                scssTemplate
            );

            collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        }
    }
    
    /// <summary>
    /// Add an arbitrary value option (e.g. text-[#08ffcd]).
    /// </summary>
    /// <param name="valueTypes"></param>
    /// <param name="scssTemplate"></param>
    public async Task AddAbitraryValueClassAsync(string valueTypes, string scssTemplate)
    {
        Classes.Add(new ScssUtilityClass
        {
            Selector = $"{SelectorPrefix}-",
            ArbitraryValueTypes = valueTypes.Split(','),
            ScssTemplate = scssTemplate
        });

        await Task.CompletedTask;
    }
    
    /// <summary>
    /// Add an arbitrary value option (e.g. "text-base/").
    /// </summary>
    /// <param name="suffix"></param>
    /// <param name="valueTypes"></param>
    /// <param name="scssTemplate"></param>
    public async Task AddAbitrarySlashValueClassAsync(string suffix, string valueTypes, string scssTemplate)
    {
        Classes.Add(new ScssUtilityClass
        {
            Selector = $"{SelectorPrefix}{(string.IsNullOrEmpty(suffix) ? string.Empty : $"-{suffix}")}/",
            ArbitraryValueTypes = valueTypes.Split(','),
            ScssTemplate = scssTemplate
        });

        await Task.CompletedTask;
    }
}