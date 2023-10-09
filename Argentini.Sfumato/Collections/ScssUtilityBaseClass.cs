namespace Argentini.Sfumato.Collections;

public sealed class ScssUtilityBaseClass
{
    public string SelectorPrefix { get; set; } = string.Empty;
    public string PropertyName { get; set; } = string.Empty;

    private Dictionary<string, string> _options = new();
    public Dictionary<string, string>? Options
    {
        get => _options;
        set
        {
            _options = value ?? new Dictionary<string, string>();
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
            Classes.Add($"{SelectorPrefix}-{item.Key}", new ScssClass
            {
                Template = $"{PropertyName}: {item.Value};"
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