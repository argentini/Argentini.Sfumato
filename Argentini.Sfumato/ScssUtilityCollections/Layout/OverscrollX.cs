namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class OverscrollX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overscroll-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "overscroll-behavior-x: auto;",
        ["contain"] = "overscroll-behavior-x: contain;",
        ["none"] = "overscroll-behavior-x: none;",
    }; 
        
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
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