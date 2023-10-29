namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class List : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "list";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["inside"] = "list-style-position: inside;",
        ["outside"] = "list-style-position: outside;",
        
        ["none"] = "list-style-type: none;",
        ["disc"] = "list-style-type: disc;",
        ["decimal"] = "list-style-type: decimal;",
        
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
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "list-style-type: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}