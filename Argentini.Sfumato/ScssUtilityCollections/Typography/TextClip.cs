namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class TextClip : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "text-clip";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "text-overflow: clip;",
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion

        return string.Empty;
    }
}