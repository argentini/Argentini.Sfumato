namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Overflow : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overflow";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "overflow: auto;",
        ["hidden"] = "overflow: hidden;",
        ["clip"] = "overflow: clip;",
        ["visible"] = "overflow: visible;",
        ["scroll"] = "overflow: scroll;"
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
        
        return string.Empty;
    }
}