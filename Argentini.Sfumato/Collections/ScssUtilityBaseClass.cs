namespace Argentini.Sfumato.Collections;

public sealed class ScssUtilityBaseClass : ScssBaseClass
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
    
    /// <summary>
    /// Generate the classes.
    /// </summary>
    public new void Generate()
    {
        Classes.Clear();
        Classes.Add($"{Selector}", new ScssClass
        {
            Template = _template
        });
    }
}