namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Tracking : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "tracking";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["tighter"] = "letter-spacing: -0.05em;",
        ["tight"] = "letter-spacing: -0.025em;",
        ["normal"] = "letter-spacing: 0em;",
        ["wide"] = "letter-spacing: 0.025em;",
        ["wider"] = "letter-spacing: 0.05em;",
        ["widest"] = "letter-spacing: 0.1em;"
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);
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
        
        if (ProcessArbitraryValues("length", cssSelector, "letter-spacing: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}