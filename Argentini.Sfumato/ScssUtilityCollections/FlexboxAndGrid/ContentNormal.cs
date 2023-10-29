namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class ContentNormal : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-normal";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "align-content: normal;",
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
        
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion

        return string.Empty;
    }
}