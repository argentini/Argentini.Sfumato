namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class Sepia : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "sepia";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "filter: sepia(100%);",
        ["0"] = "filter: sepia(0);"
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
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
            return $"filter: sepia({cssSelector.ArbitraryValue});";
        
        #endregion

        return string.Empty;
    }
}