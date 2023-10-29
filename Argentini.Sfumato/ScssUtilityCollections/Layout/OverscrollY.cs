namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class OverscrollY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overscroll-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "overscroll-behavior-y: auto;",
        ["contain"] = "overscroll-behavior-y: contain;",
        ["none"] = "overscroll-behavior-y: none;",
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