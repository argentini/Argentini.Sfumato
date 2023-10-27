namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class Divide : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "divide";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["solid"] = """
                    & > * + * {
                        border-style: solid;
                    }
                    """,
        ["dashed"] = """
                     & > * + * {
                         border-style: dashed;
                     }
                     """,
        ["dotted"] = """
                     & > * + * {
                         border-style: dotted;
                     }
                     """,
        ["double"] = """
                     & > * + * {
                         border-style: double;
                     }
                     """,
        ["none"] = """
                   & > * + * {
                       border-style: none;
                   }
                   """
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        // Static utilities (e.g. bg-no-repeat)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Calculated Utilities
        
        // Color preset (e.g. divide-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $$"""
                   & > * + * {
                       border-color: {{color}};
                   }
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $$"""
                   & > * + * {
                       border-color: {{cssSelector.ArbitraryValue}};
                   }
                   """;

        #endregion

        return string.Empty;
    }
}