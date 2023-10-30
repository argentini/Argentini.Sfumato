namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class BreakBefore : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "break-before";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "break-before: auto;",
        ["avoid"] = "break-before: avoid;",
        ["all"] = "break-before: all;",
        ["avoid-page"] = "break-before: avoid-page;",
        ["page"] = "break-before: page;",
        ["left"] = "break-before: left;",
        ["right"] = "break-before: right;",
        ["column"] = "break-before: column;"
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