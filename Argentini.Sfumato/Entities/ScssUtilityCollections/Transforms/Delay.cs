namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Transforms;

public class Delay : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "delay";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "transition-delay: 0s;",
        ["75"] = "transition-delay: 75ms;",
        ["100"] = "transition-delay: 100ms;",
        ["150"] = "transition-delay: 150ms;",
        ["200"] = "transition-delay: 200ms;",
        ["300"] = "transition-delay: 300ms;",
        ["500"] = "transition-delay: 500ms;",
        ["700"] = "transition-delay: 700ms;",
        ["1000"] = "transition-delay: 1000ms;"
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
        
        if (ProcessArbitraryValues("time", cssSelector, "transition-delay: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}