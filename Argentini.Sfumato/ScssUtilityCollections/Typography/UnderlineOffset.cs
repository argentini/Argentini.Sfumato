namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class UnderlineOffset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "underline-offset";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "text-underline-offset: auto;",
        ["0"] = "text-underline-offset: 0px;",
        ["1"] = "text-underline-offset: 1px;",
        ["2"] = $"text-underline-offset: {2.PxToRem()};",
        ["4"] = $"text-underline-offset: {4.PxToRem()};",
        ["8"] = $"text-underline-offset: {8.PxToRem()};"
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
        
        if (cssSelector.ArbitraryValueType == "length")
            return $"text-underline-offset: {cssSelector.ArbitraryValue};";

        #endregion

        return string.Empty;
    }
}