namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class BreakNormal : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "break-normal";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = """
               overflow-wrap: normal;
               word-break: normal;
               """,
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