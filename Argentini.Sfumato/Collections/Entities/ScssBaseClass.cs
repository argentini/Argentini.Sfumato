namespace Argentini.Sfumato.Collections.Entities;

public sealed class ScssBaseClass
{
    public string SelectorPrefix { get; set; } = string.Empty;
    public string PropertyName { get; set; } = string.Empty;
    public string PrefixValueTypes { get; set; } = string.Empty;
    public string ChildSelector { get; set; } = string.Empty;
    public string PropertyTemplate { get; set; } = "{property}: {value};";
    public string GlobalGrouping { get; set; } = string.Empty;
    
    public int AddNumberedOptionsMinValue { get; set; } = -1;
    public int AddNumberedOptionsMaxValue { get; set; } = -1;
    public bool AddNumberedOptionsIsNegative { get; set; }
    public string AddNumberedOptionsValueTemplate { get; set; } = string.Empty;
    public decimal AddNumberedRemUnitsOptionsMinValue { get; set; } = -1;
    public decimal AddNumberedRemUnitsOptionsMaxValue { get; set; } = -1;
    public bool AddFractionOptions { get; set; }
    public bool AddPercentageOptions { get; set; }
    public bool AddOneBasedPercentageOptions { get; set; }
    public bool AddColorOptions { get; set; }
    
    private Dictionary<string, string> _options = new();
    public Dictionary<string, string>? Options
    {
        get => _options;
        set
        {
            _options = value ?? new Dictionary<string, string>();

            if (AddOneBasedPercentageOptions == false && AddNumberedOptionsMinValue > -1 && AddNumberedOptionsMaxValue > -1 && AddNumberedOptionsMaxValue > AddNumberedOptionsMinValue)
            {
                AddNumberedOptions();
            }

            if (AddNumberedRemUnitsOptionsMinValue > -1 && AddNumberedRemUnitsOptionsMaxValue > -1 && AddNumberedRemUnitsOptionsMaxValue > AddNumberedRemUnitsOptionsMinValue)
            {
                AddNumberedRemUnitsOptions();
            }

            if (AddFractionOptions)
            {
                AddFractionsOptions();
            }

            if (AddPercentageOptions)
            {
                AddPercentagesOptions();
            }

            if (AddOneBasedPercentageOptions)
            {
                AddOneBasedPercentagesOptions();
            }
            
            if (AddColorOptions)
            {
                AddColors();
            }
            
            Generate();
        }
    }
    
    private List<ScssClass> _classes = new();
    public List<ScssClass> Classes
    {
        get => _classes;
        set
        {
            _classes = value;

            Generate();
        }
    }
    
    #region Helper Methods

    /// <summary>
    /// Add master named color list to options. 
    /// </summary>
    public void AddColors()
    {
        foreach (var color in SfumatoScss.Colors)
        {
            Options?.TryAdd(color.Key, color.Value);
        }
    }
    
    /// <summary>
    /// Add options for incremental whole numbers where the prefix and value arte the same (e.g. ["order-1"] => "order: 1;").
    /// Used by inherited classes.
    /// </summary>
    public void AddNumberedOptions()
    {
        for (var x = AddNumberedOptionsMinValue; x <= AddNumberedOptionsMaxValue; x++)
        {
            var value = (AddNumberedOptionsIsNegative ? x * -1 : x).ToString();
            
            if (AddNumberedOptionsValueTemplate != string.Empty)
                value = AddNumberedOptionsValueTemplate.Replace("{value}", value);
            
            Options?.TryAdd(x.ToString(), value);
        }
    }

    /// <summary>
    /// Add options for incremental whole numbers where that translate to fractional values from zero to one (e.g. ["opacity-5"] => "opacity: 0.05;").
    /// Used by inherited classes.
    /// </summary>
    public void AddOneBasedPercentagesOptions()
    {
        for (var x = AddNumberedOptionsMinValue; x <= AddNumberedOptionsMaxValue; x++)
        {
            var percentage = x / 100m;
            var value = $"{percentage}";
            
            if (AddNumberedOptionsValueTemplate != string.Empty)
                value = AddNumberedOptionsValueTemplate.Replace("{value}", value);
            
            Options?.TryAdd(x.ToString(), value);
        }
    }
    
    /// <summary>
    /// Add options for percentages by number.
    /// Used by inherited classes.
    /// </summary>
    public void AddPercentagesOptions()
    {
        for (var x = 0; x <= 100; x+=5)
        {
            Options?.TryAdd($"{x}%", $"{x}%");
        }
    }
    
    /// <summary>
    /// Add options for fractions from 1/2 up through 11/12, and "full" =&gt; 100%.
    /// Used by inherited classes.
    /// </summary>
    public void AddFractionsOptions()
    {
        foreach (var percentage in SfumatoScss.Fractions)
            Options?.TryAdd(percentage.Key, percentage.Value);
    }

    /// <summary>
    /// Add numbered size options from 1 to 96 using rem units (e.g. basis-0.5, basis-1.5, etc.).
    /// Used by inherited classes.
    /// </summary>
    public void AddNumberedRemUnitsOptions()
    {
        var step = 0.5m;

        for (var x = AddNumberedRemUnitsOptionsMinValue; x <= AddNumberedRemUnitsOptionsMaxValue; x += step)
        {
            if (x == 4)
                step = 1;

            Options?.TryAdd($"{x:0.#}", $"{x / 4:0.###}rem");
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
            var template = string.Empty;
            var propertyNames = PropertyName.Split(',');

            foreach (var propName in propertyNames)
                template += PropertyTemplate.Replace("{property}", propName) + " ";
            
            if (item is { Key: "-", Value: "" } && SelectorPrefix != string.Empty)
            {
                Classes.Add(new ScssClass($"{SelectorPrefix}-")
                {
                    GlobalGrouping = GlobalGrouping,
                    ValueTypes = PrefixValueTypes,
                    ChildSelector = ChildSelector,
                    Template = template.Trim()
                });

                continue;
            }

            Classes.Add(new ScssClass($"{(SelectorPrefix != string.Empty ? $"{SelectorPrefix}{(item.Key != string.Empty ? "-" : string.Empty)}" : string.Empty)}{item.Key}")
            {
                GlobalGrouping = GlobalGrouping,
                ChildSelector = ChildSelector,
                Value = item.Value,
                Template = template.Trim()
            });
        }
    }
    
    #endregion
}