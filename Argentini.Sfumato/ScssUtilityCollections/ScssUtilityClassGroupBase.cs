namespace Argentini.Sfumato.ScssUtilityCollections;

public abstract class ScssUtilityClassGroupBase
{
    public virtual string SelectorPrefix { get; set; } = string.Empty;
    public virtual string Category { get; set; } = string.Empty;
    public virtual List<string> SelectorIndex { get; set; } = new();
    protected string Result = string.Empty;

    public virtual void Initialize(SfumatoAppState appState)
    {
    }
    
    public virtual string GetStyles(CssSelector cssSelector)
    {
        return string.Empty;
    }

    /// <summary>
    /// Process a property dictionary (static) option in a utility class's GetStyles() method.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="cssSelector"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected static bool ProcessStaticDictionaryOptions(Dictionary<string, string>? dictionary, CssSelector cssSelector, out string result)
    {
        if (cssSelector.HasArbitraryValue == false)
        {
            if (dictionary?.TryGetValue(cssSelector.CoreSegment, out var value) ?? false)
            {
                result = value;
                return true;
            }
        }

        result = string.Empty;
        return false;
    }
    
    /// <summary>
    /// Process a dictionary (calculated) option in a utility class's GetStyles() method.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="cssSelector"></param>
    /// <param name="propertyTemplate"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected static bool ProcessDictionaryOptions(Dictionary<string, string>? dictionary, CssSelector cssSelector, string propertyTemplate, out string result)
    {
        if (cssSelector.HasArbitraryValue == false)
        {
            if (dictionary?.TryGetValue(cssSelector.CoreSegment, out var value) ?? false)
            {
                result = propertyTemplate.Replace("{value}", value);
                return true;
            }
        }

        result = string.Empty;
        return false;
    }

    /// <summary>
    /// Process a color modifier in a utility class's GetStyles() method.
    /// 
    /// </summary>
    /// <param name="cssSelector"></param>
    /// <param name="propertyTemplate"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected static bool ProcessColorModifierOptions(CssSelector cssSelector, string propertyTemplate, out string result)
    {
        if (cssSelector.HasModifierValue || cssSelector.HasArbitraryValue)
        {
            if (cssSelector.AppState?.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out var color) ?? false)
            {
                var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;
                
                if (valueType == "integer")
                {
                    var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;
                    var opacity = int.Parse(modifierValue) / 100m;

                    result = propertyTemplate.Replace("{value}", color.Replace(",1.0)", $",{opacity:F2})"));
                    return true;
                }
                
                if (valueType == "number")
                {
                    var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

                    result = propertyTemplate.Replace("{value}", color.Replace(",1.0)", $",{modifierValue})"));
                    return true;
                }
            }
        }

        result = string.Empty;
        return false;
    }

    /// <summary>
    /// Process a font line height modifier in a utility class's GetStyles() method.
    /// </summary>
    /// <param name="cssSelector"></param>
    /// <param name="propertyTemplate"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected static bool ProcessTextSizeLeadingModifierOptions(CssSelector cssSelector, string propertyTemplate, out string result)
    {
        if (cssSelector.HasModifierValue || cssSelector.HasArbitraryValue)
        {
            if (cssSelector.AppState?.TextSizeOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out var fontSize) ?? false)
            {
                var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;

                if (valueType is "length" or "percentage")
                {
                    var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

                    result = propertyTemplate.Replace("{fontSize}", fontSize).Replace("{value}", modifierValue);
                    return true;
                }
                    
                if (valueType is "integer" or "number" or "")
                {
                    var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

                    if (valueType is "integer" or "" && cssSelector.AppState.LeadingOptions.TryGetValue(modifierValue, out var option))
                    {
                        result = propertyTemplate.Replace("{fontSize}", fontSize).Replace("{value}", option);
                        return true;
                    }

                    if (valueType is "integer" or "number")
                    {
                        result = propertyTemplate.Replace("{fontSize}", fontSize).Replace("{value}", modifierValue);
                        return true;
                    }
                }
            }
        }

        result = string.Empty;
        return false;
    }
    
    /// <summary>
    /// Process an arbitrary value in a utility class's GetStyles() method.
    /// </summary>
    /// <param name="valueTypes"></param>
    /// <param name="cssSelector"></param>
    /// <param name="propertyTemplate"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected static bool ProcessArbitraryValues(string valueTypes, CssSelector cssSelector, string propertyTemplate, out string result)
    {
        if (cssSelector.HasArbitraryValue)
        {
            var valueTypesArray = valueTypes.Split(',');
            
            if (valueTypesArray.Contains(cssSelector.ArbitraryValueType) || valueTypes == string.Empty || valueTypesArray.Length == 0)
            {
                result = propertyTemplate.Replace("{value}", cssSelector.ArbitraryValue);
                return true;
            }
        }

        result = string.Empty;
        return false;
    }
}