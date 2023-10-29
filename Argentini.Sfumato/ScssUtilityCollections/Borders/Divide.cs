namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class Divide : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "divide";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
        
        foreach (var corePrefix in appState.ColorOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }

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
        
        #region Modifier Utilities
        
        if ((cssSelector.HasModifierValue || cssSelector.HasArbitraryValue) && cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out color))
        {
            var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;
            
            if (valueType == "integer")
            {
                var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;
                var opacity = int.Parse(modifierValue) / 100m;

                return $$"""
                       & > * + * {
                           border-color: {{color.Replace(",1.0)", $",{opacity:F2})")}};
                       }
                       """;
            }
            
            if (valueType == "number")
            {
                var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

                return $$"""
                       & > * + * {
                           border-color: {{color.Replace(",1.0)", $",{modifierValue})")}};
                       }
                       """;
            }
        }

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