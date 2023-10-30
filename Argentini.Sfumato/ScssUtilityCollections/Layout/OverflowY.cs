namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class OverflowY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overflow-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "overflow-y: auto;",
        ["hidden"] = "overflow-y: hidden;",
        ["clip"] = "overflow-y: clip;",
        ["visible"] = "overflow-y: visible;",
        ["scroll"] = "overflow-y: scroll;"
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