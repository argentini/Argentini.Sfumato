namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class BackdropGrayscale : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-grayscale";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "backdrop-filter: grayscale(100%);",
        ["0"] = "backdrop-filter: grayscale(0);"
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
            return $"backdrop-filter: grayscale({cssSelector.ArbitraryValue});";
        
        #endregion

        return string.Empty;
    }
}