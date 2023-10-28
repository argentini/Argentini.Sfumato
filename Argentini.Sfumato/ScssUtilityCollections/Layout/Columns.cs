namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Columns : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "columns";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "columns: auto;",
        ["3xs"] = "columns: 16rem;",
        ["2xs"] = "columns: 18rem;",
        ["xs"] = "columns: 20rem;",
        ["sm"] = "columns: 24rem;",
        ["md"] = "columns: 28rem;",
        ["lg"] = "columns: 32rem;",
        ["xl"] = "columns: 36rem;",
        ["2xl"] = "columns: 42rem;",
        ["3xl"] = "columns: 48rem;",
        ["4xl"] = "columns: 56rem;",
        ["5xl"] = "columns: 64rem;",
        ["6xl"] = "columns: 72rem;",
        ["7xl"] = "columns: 80rem;"
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.LayoutWholeNumberOptions.Keys)
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
        
        #region Calculated Utilities
        
        if (cssSelector.AppState.LayoutWholeNumberOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"columns: {size};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage" or "integer")
            return $"columns: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}