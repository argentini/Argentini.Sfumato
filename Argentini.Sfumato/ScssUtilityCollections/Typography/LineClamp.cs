namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class LineClamp : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "line-clamp";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = """
                   overflow: visible;
                   display: block;
                   -webkit-box-orient: horizontal;
                   -webkit-line-clamp: none;
                   """,
        ["1"] = """
                -webkit-line-clamp: 1;
                overflow: hidden;
                display: -webkit-box;
                -webkit-box-orient: vertical;
                """,
        ["2"] = """
                -webkit-line-clamp: 2;
                overflow: hidden;
                display: -webkit-box;
                -webkit-box-orient: vertical;
                """,
        ["3"] = """
                -webkit-line-clamp: 3;
                overflow: hidden;
                display: -webkit-box;
                -webkit-box-orient: vertical;
                """,
        ["4"] = """
                -webkit-line-clamp: 4;
                overflow: hidden;
                display: -webkit-box;
                -webkit-box-orient: vertical;
                """,
        ["5"] = """
                -webkit-line-clamp: 5;
                overflow: hidden;
                display: -webkit-box;
                -webkit-box-orient: vertical;
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
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "integer")
            return $"""
                   -webkit-line-clamp: {cssSelector.ArbitraryValue};
                   overflow: hidden;
                   display: -webkit-box;
                   -webkit-box-orient: vertical;
                   """;

        #endregion

        return string.Empty;
    }
}