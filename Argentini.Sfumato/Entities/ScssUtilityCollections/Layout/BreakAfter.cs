namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class BreakAfter : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "break-after";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "break-after: auto;",
        ["avoid"] = "break-after: avoid;",
        ["all"] = "break-after: all;",
        ["avoid-page"] = "break-after: avoid-page;",
        ["page"] = "break-after: page;",
        ["left"] = "break-after: left;",
        ["right"] = "break-after: right;",
        ["column"] = "break-after: column;"
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