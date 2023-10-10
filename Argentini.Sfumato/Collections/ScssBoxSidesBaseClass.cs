namespace Argentini.Sfumato.Collections;

public sealed class ScssBoxSidesBaseClass
{
    public string SelectorPrefix { get; set; } = string.Empty;
    public string PropertyName { get; set; } = string.Empty;
    public string PrefixValueTypes { get; set; } = string.Empty;

    private Dictionary<string, string> _options = new();
    public Dictionary<string, string>? Options
    {
        get => _options;
        set
        {
            _options = value ?? new Dictionary<string, string>();
            
            var step = (decimal)0.5;

            for (var x = (decimal)0.5; x < 97; x += step)
            {
                if (x == 4)
                    step = 1;

                Options?.TryAdd($"{x:0.#}", $"{x / 4:0.###}rem");
            }
            
            Generate();
        }
    }

    /// <summary>
    /// Generate the classes.
    /// </summary>
    public void Generate()
    {
        Classes.Clear();

        foreach (var item in Options ?? new Dictionary<string, string>())
        {
            if (item is { Key: "-", Value: "" } && PrefixValueTypes != string.Empty && SelectorPrefix != string.Empty)
            {
                Classes.Add($"{SelectorPrefix}-", new ScssClass
                {
                    ValueTypes = PrefixValueTypes,
                    Template = $"{PropertyName}: {{value}};"
                });

                continue;
            }
            
            Classes.Add($"{(SelectorPrefix != string.Empty ? $"{SelectorPrefix}-" : string.Empty)}{item.Key}", new ScssClass
            {
                Value = item.Value,
                Template = $"{PropertyName}: {{value}};"
            });
        }
    }

    private Dictionary<string, ScssClass> _classes = new();
    public Dictionary<string, ScssClass> Classes
    {
        get => _classes;
        set
        {
            _classes = value;
            Generate();
        }
    }
}