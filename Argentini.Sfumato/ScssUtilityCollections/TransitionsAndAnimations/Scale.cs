namespace Argentini.Sfumato.ScssUtilityCollections.TransitionsAndAnimations;

public class Scale : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scale";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "transform: scale(0);",
        ["50"] = "transform: scale(0.5);",
        ["75"] = "transform: scale(0.75);",
        ["90"] = "transform: scale(0.90);",
        ["95"] = "transform: scale(0.95);",
        ["100"] = "transform: scale(1.0);",
        ["105"] = "transform: scale(1.05);",
        ["110"] = "transform: scale(1.1);",
        ["125"] = "transform: scale(1.25);",
        ["150"] = "transform: scale(1.5);"
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
        
        if (ProcessArbitraryValues("number", cssSelector, "transform: scale({value});", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}