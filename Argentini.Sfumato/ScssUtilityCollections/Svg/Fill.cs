namespace Argentini.Sfumato.ScssUtilityCollections.Svg;

public class Fill : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "fill";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "fill: none;",
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.ColorOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "fill: {value};", out Result))
            return Result;

        #endregion

        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector, "fill: {value};", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "fill: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}