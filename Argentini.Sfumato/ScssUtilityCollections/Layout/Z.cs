namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Z : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "z";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "z-index: auto;",
        ["top"] = $"z-index: {int.MaxValue.ToString()};",
        ["bottom"] = $"z-index: {int.MinValue.ToString()};",
        ["0"] = "z-index: 0;",
        ["10"] = "z-index: 10;",
        ["20"] = "z-index: 20;",
        ["30"] = "z-index: 30;",
        ["40"] = "z-index: 40;",
        ["50"] = "z-index: 50;"
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
        
        // Static utilities (e.g. flex)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "integer")
            return $"z-index: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}