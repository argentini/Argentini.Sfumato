namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class Scroll : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scroll";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "scroll-behavior: auto;",
        ["smooth"] = "scroll-behavior: smooth;",
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