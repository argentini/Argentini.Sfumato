namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class ContentCenter : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-center";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "align-content: center;",
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