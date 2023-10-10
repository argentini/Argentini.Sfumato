namespace Argentini.Sfumato.Collections;

public sealed class ScssMeasurementClass : ScssBaseClass
{
    private Dictionary<string, string> _options = new();
    public new Dictionary<string, string>? Options
    {
        get => _options;
        set
        {
            _options = value ?? new Dictionary<string, string>();
            
            AddNumberedRemUnits(_options);
            AddPercentages(_options);
            Generate();
        }
    }
    
    /// <summary>
    /// Generate the classes.
    /// </summary>
    public new void Generate()
    {
        Classes.Clear();

        foreach (var item in Options ?? new Dictionary<string, string>())
        {
            var template = string.Empty;
            var propertyNames = PropertyName.Split(',');

            foreach (var propName in propertyNames)
                template += $"{propName}: {{value}}; ";
            
            if (item is { Key: "-", Value: "" } && PrefixValueTypes != string.Empty && SelectorPrefix != string.Empty)
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
}