namespace Argentini.Sfumato.Collections;

public sealed class ScssUtilityBaseClass
{
    public string Selector { get; set; } = string.Empty;

    private string _template = string.Empty;
    public string Template
    {
        get => _template;
        set
        {
            _template = value;
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
    /// Generate the classes.
    /// </summary>
    public void Generate()
    {
        Classes.Clear();
        Classes.Add($"{Selector}", new ScssClass
        {
            Template = _template
        });
    }
}