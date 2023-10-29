namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class Grayscale : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "grayscale";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "filter: grayscale(100%);",
        ["0"] = "filter: grayscale(0);"
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
        
        if (ProcessArbitraryValues("percentage", cssSelector, "filter: grayscale({value});", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}