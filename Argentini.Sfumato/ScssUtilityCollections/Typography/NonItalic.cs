namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class NonItalic : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "non-italic";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "font-style: normal;",
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