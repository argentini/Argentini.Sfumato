namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Borders;

public class RingInset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ring-inset";
    public override string Category => "ring";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);
    }

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "--sf-ring-inset: inset;",
    }; 
    
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