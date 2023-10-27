namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class DivideY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "divide-y";
    
    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["reverse"] = "--sf-divide-y-reverse: 1;"
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Calculated Utilities
        
        // Color preset (e.g. divide-rose-100)
        if (cssSelector.AppState.DivideWidthOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $$"""
                   & > * + * {
                       border-top-width: 0px;
                       border-bottom-width: {{size}};
                   }
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $$"""
                   & > * + * {
                       border-top-width: 0px;
                       border-bottom-width: {{cssSelector.ArbitraryValue}};
                   }
                   """;

        #endregion

        return string.Empty;
    }
}