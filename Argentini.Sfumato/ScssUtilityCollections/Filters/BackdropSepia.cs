namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class BackdropSepia : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-sepia";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "backdrop-filter: sepia(100%);",
        ["0"] = "backdrop-filter: sepia(0);"
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
    }
    
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
        
        if (cssSelector.ArbitraryValueType == "percentage")
            return $"backdrop-filter: sepia({cssSelector.ArbitraryValue});";
        
        #endregion

        return string.Empty;
    }
}