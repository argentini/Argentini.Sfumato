namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class ListImage : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "list-image";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "list-style-image: none;",
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "url")
            return $"list-style-image: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}