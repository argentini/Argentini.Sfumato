namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class LineThrough : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "line-through";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "text-decoration-line: line-through;",
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