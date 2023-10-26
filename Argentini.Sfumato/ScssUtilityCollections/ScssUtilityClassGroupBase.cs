namespace Argentini.Sfumato.ScssUtilityCollections;

public abstract class ScssUtilityClassGroupBase
{
    public virtual string SelectorPrefix => string.Empty;
    public virtual string Category => string.Empty;

    public virtual string GetStyles(CssSelector cssSelector)
    {
        return string.Empty;
    }
}