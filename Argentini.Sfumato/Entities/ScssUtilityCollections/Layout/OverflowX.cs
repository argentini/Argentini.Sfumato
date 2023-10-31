namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class OverflowX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overflow-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "overflow-x: auto;",
        ["hidden"] = "overflow-x: hidden;",
        ["clip"] = "overflow-x: clip;",
        ["visible"] = "overflow-x: visible;",
        ["scroll"] = "overflow-x: scroll;"
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