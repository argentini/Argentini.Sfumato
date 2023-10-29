namespace Argentini.Sfumato.ScssUtilityCollections.Sizing;

public class MinW : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "min-w";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "min-width: 0px;",
        ["px"] = "min-width: 1px;",
        ["screen"] = "min-width: 100vw;",
        ["min"] = "min-width: min-content;",
        ["max"] = "min-width: max-content;",
        ["fit"] = "min-width: fit-content;"
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
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer,length,percentage", cssSelector, "min-width: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}