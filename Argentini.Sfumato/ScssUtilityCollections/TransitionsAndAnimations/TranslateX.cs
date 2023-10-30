namespace Argentini.Sfumato.ScssUtilityCollections.TransitionsAndAnimations;

public class TranslateX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "translate-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "transform: translateX(0px);",
        ["px"] = "transform: translateX(1px);",
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);

        await AddToIndexAsync(appState.LayoutRemUnitOptions);

        await AddToIndexAsync(appState.VerbatimFractionOptions);
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "transform: translateX({value});", out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.VerbatimFractionOptions, cssSelector, "transform: translateX({value});", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "transform: translateX({value});", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}