namespace Argentini.Sfumato.ScssUtilityCollections.Entities;

public sealed class ScssUtilityClass
{
    public string SelectorPrefix { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public ConcurrentBag<ScssUtilityClassOption> Options { get; } = new();

    /// <summary>
    /// Add a dictionary of options.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="scssTemplate"></param>
    public async Task AddOptionsAsync(Dictionary<string,string> options, string scssTemplate)
    {
        foreach (var (key, value) in options)
        {
            Options.Add(new ScssUtilityClassOption
            {
                Selector = $"{SelectorPrefix}-{key}",
                Value = value,
                ScssTemplate = scssTemplate
            });
        }

        await Task.CompletedTask;
    }
    
    /// <summary>
    /// Add an arbitrary value option.
    /// </summary>
    /// <param name="valueTypes"></param>
    /// <param name="scssTemplate"></param>
    /// <param name="separator"></param>
    public async Task AddAbitraryValueOptionAsync(string valueTypes, string scssTemplate, string separator = "-")
    {
        Options.Add(new ScssUtilityClassOption
        {
            Selector = $"{SelectorPrefix}{separator}",
            ArbitraryValueTypes = valueTypes.Split(','),
            ScssTemplate = scssTemplate
        });

        await Task.CompletedTask;
    }
}