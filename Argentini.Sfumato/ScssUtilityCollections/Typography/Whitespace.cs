namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Whitespace : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "whitespace";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["normal"] = "white-space: normal;",
        ["nowrap"] = "white-space: nowrap;",
        ["no-wrap"] = "white-space: nowrap;",
        ["pre"] = "white-space: pre;",
        ["pre-line"] = "white-space: pre-line;",
        ["pre-wrap"] = "white-space: pre-wrap;",
        ["break-spaces"] = "white-space: break-spaces;"
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