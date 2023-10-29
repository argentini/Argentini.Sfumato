namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class RoundedE : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix { get; set; } = "rounded-e";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in appState.RoundedOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.RoundedOptions, cssSelector,
            """
            border-start-end-radius: {value};
            border-end-end-radius: {value};
            """, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
                """
                border-start-end-radius: {value};
                border-end-end-radius: {value};
                """, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}