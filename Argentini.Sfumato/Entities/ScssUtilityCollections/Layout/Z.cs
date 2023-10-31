namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Z : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "z";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "z-index: auto;",
        ["top"] = $"z-index: {int.MaxValue.ToString()};",
        ["bottom"] = $"z-index: {int.MinValue.ToString()};",
        ["0"] = "z-index: 0;",
        ["10"] = "z-index: 10;",
        ["20"] = "z-index: 20;",
        ["30"] = "z-index: 30;",
        ["40"] = "z-index: 40;",
        ["50"] = "z-index: 50;"
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
        
        if (ProcessArbitraryValues("integer", cssSelector, "z-index: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}