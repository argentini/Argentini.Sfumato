namespace Argentini.Sfumato.Collections.Entities;

public sealed class ScssUtilityBaseClass
{
    public string Selector { get; set; } = string.Empty;
    public string ChildSelector { get; set; } = string.Empty;
    public string GlobalGrouping { get; set; } = string.Empty;

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
    
    /// <summary>
    /// Generate the classes.
    /// </summary>
    public void Generate()
    {
        Classes.Clear();
        Classes.Add(new ScssClass
        {
            RootClassName = $"{Selector}",
            GlobalGrouping = GlobalGrouping,
            IsUtilityClass = true,
            ChildSelector = ChildSelector,
            Template = _template
        });
    }
}