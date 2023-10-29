namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class DivideX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "divide-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["reverse"] = "--sf-divide-x-reverse: 1;"
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
        
        foreach (var corePrefix in appState.DivideWidthOptions.Keys)
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.DivideWidthOptions, cssSelector,
            """
            & > * + * {
                border-right-width: 0px;
                border-left-width: {value};
            }
            """, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
            """
            & > * + * {
                border-right-width: 0px;
                border-left-width: {value};
            }
            """, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}