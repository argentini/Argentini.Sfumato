namespace Argentini.Sfumato.ScssUtilityCollections;

public abstract class ScssUtilityClassGroupBase
{
    public virtual string SelectorPrefix { get; set; } = string.Empty;
    public virtual string Category { get; set; } = string.Empty;
    public virtual List<string> SelectorIndex { get; set; } = new();

    public virtual void Initialize(SfumatoAppState appState)
    {
    }
    
    public virtual string GetStyles(CssSelector cssSelector)
    {
        return string.Empty;
    }
}