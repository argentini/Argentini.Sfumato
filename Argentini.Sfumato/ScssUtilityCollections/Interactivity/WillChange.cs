namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class WillChange : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "will-change";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "will-change: auto;",
        ["scroll"] = "will-change: scroll-position;",
        ["contents"] = "will-change: contents;",
        ["transform"] = "will-change: transform;"
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        return string.Empty;
    }
}