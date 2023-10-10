namespace Argentini.Sfumato.Collections;

public class ScssBaseClass
{
    #region Constants

    public static Dictionary<string, string> Percentages { get; } = new()
    {
        ["1/2"] = "50%",
        ["1/3"] = "33.333333%",
        ["2/3"] = "66.666667%",
        ["1/4"] = "25%",
        ["2/4"] = "50%",
        ["3/4"] = "75%",
        ["1/5"] = "20%",
        ["2/5"] = "40%",
        ["3/5"] = "60%",
        ["4/5"] = "80%",
        ["1/6"] = "16.666667%",
        ["2/6"] = "33.333333%",
        ["3/6"] = "50%",
        ["4/6"] = "66.666667%",
        ["5/6"] = "83.333333%",
        ["1/12"] = "8.333333%",
        ["2/12"] = "16.666667%",
        ["3/12"] = "25%",
        ["4/12"] = "33.333333%",
        ["5/12"] = "41.666667%",
        ["6/12"] = "50%",
        ["7/12"] = "58.333333%",
        ["8/12"] = "66.666667%",
        ["9/12"] = "75%",
        ["10/12"] = "83.333333%",
        ["11/12"] = "91.666667%",
        ["full"] = "100%"
    };
    
    #endregion
    
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

            ProcessOptions();
            Generate();
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
    
    /// <summary>
    /// Override this method in inherited classes to process options.
    /// </summary>
    /// <returns></returns>
    protected virtual void ProcessOptions()
    {
    }
    
    #region Helper Methods

    /// <summary>
    /// Add options for percentages from 1/2 up through 11/12, and "full" =&gt; 100%.
    /// Used by inherited classes.
    /// </summary>
    protected void AddPercentages()
    {
        foreach (var percentage in Percentages)
            Options?.TryAdd(percentage.Key, percentage.Value);
    }

    /// <summary>
    /// Add numbered size options from 1 to 96 using rem units (e.g. basis-0.5, basis-1.5, etc.).
    /// Used by inherited classes.
    /// </summary>
    protected void AddNumberedRemUnits()
    {
        var step = 0.5m;

        for (var x = 0.5m; x < 97; x += step)
        {
            if (x == 4)
                step = 1;

            Options?.TryAdd($"{x:0.#}", $"{x / 4:0.###}rem");
        }
    }
    
    /// <summary>
    /// Generate the classes.
    /// </summary>
    protected void Generate()
    {
        Classes.Clear();

        foreach (var item in Options ?? new Dictionary<string, string>())
        {
            var template = string.Empty;
            var propertyNames = PropertyName.Split(',');

            foreach (var propName in propertyNames)
                template += $"{propName}: {{value}}; ";
            
            if (item is { Key: "-", Value: "" } && SelectorPrefix != string.Empty)
            {
                Classes.Add($"{SelectorPrefix}-", new ScssClass
                {
                    ValueTypes = PrefixValueTypes,
                    Template = template.Trim()
                });

                continue;
            }

            Classes.Add($"{(SelectorPrefix != string.Empty ? $"{SelectorPrefix}-" : string.Empty)}{item.Key}", new ScssClass
            {
                Value = item.Value,
                Template = template.Trim()
            });
        }
    }
    
    #endregion
}