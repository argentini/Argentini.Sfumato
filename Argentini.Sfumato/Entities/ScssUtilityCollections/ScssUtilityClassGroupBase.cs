namespace Argentini.Sfumato.Entities.ScssUtilityCollections;

public abstract class ScssUtilityClassGroupBase
{
    public virtual string SelectorPrefix { get; set; } = string.Empty;
    public virtual string Category { get; set; } = string.Empty;
    public virtual List<string> SelectorIndex { get; set; } = new();
    protected string Result = string.Empty;

    public virtual Task InitializeAsync(SfumatoAppState appState)
    {
        return Task.CompletedTask;
    }
    
    public virtual string GetStyles(CssSelector cssSelector)
    {
        return string.Empty;
    }

    /// <summary>
    /// Add items to the selector index in a utility class's Initialize() method.
    /// </summary>
    /// <param name="dictionary"></param>
    protected async ValueTask AddToIndexAsync(Dictionary<string,string> dictionary)
    {
        foreach (var corePrefix in dictionary.Keys.Where(k => k != string.Empty))
        {
            var key = $"{SelectorPrefix}-{corePrefix}";
            
            if (SelectorIndex.Contains(key) == false)
                SelectorIndex.Add(key);
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// Add items to the selector index in a utility class's Initialize() method.
    /// </summary>
    /// <param name="list"></param>
    protected async ValueTask AddToIndexAsync(IEnumerable<string> list)
    {
        foreach (var corePrefix in list.Where(k => k != string.Empty))
        {
            var key = $"{SelectorPrefix}-{corePrefix}";
            
            if (SelectorIndex.Contains(key) == false)
                SelectorIndex.Add(key);
        }

        await Task.CompletedTask;
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
                if (dictionary == cssSelector.AppState?.ColorOptions)
                    value = value.WebColorToRgba();
                
                result = propertyTemplate.Replace("{value}", value);
                return true;
            }
        }

        result = string.Empty;
        return false;
    }

    /// <summary>
    /// Process a dictionary (calculated) option in a utility class's GetStyles() method.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="cssSelector"></param>
    /// <param name="propertyTemplate"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected static bool ProcessListOptions(List<string>? list, CssSelector cssSelector, string propertyTemplate, out string result)
    {
        if (cssSelector.HasArbitraryValue == false)
        {
            if (list?.Contains(cssSelector.CoreSegment) ?? false)
            {
                result = propertyTemplate.Replace("{value}", cssSelector.CoreSegment);
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
        if (cssSelector.UsesModifier)
        {
            if (cssSelector.AppState?.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out var color) ?? false)
            {
                if (string.IsNullOrEmpty(color))
                {
                    result = string.Empty;
                    return false;
                }

                var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;
                var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

                if (valueType == "integer")
                {
                    color = color.WebColorToRgba(int.Parse(modifierValue));
                    result = propertyTemplate.Replace("{value}", color);

                    return true;
                }

                if (valueType == "number")
                {
                    color = color.WebColorToRgba(decimal.Parse(modifierValue));
                    result = propertyTemplate.Replace("{value}", color);

                    return true;
                }
            }
        }

        result = string.Empty;
        return false;
    }

    /// <summary>
    /// Process a fraction modifier in a utility class's GetStyles() method.
    /// 
    /// </summary>
    /// <param name="cssSelector"></param>
    /// <param name="propertyTemplate"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected static bool ProcessFractionModifierOptions(CssSelector cssSelector, string propertyTemplate, out string result)
    {
        if (cssSelector.UsesModifier)
        {
            var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;

            if (valueType == "integer")
            {
                if (decimal.TryParse(cssSelector.CoreSegment, out var dividend))
                {
                    var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

                    if (decimal.TryParse(modifierValue, out var divisor))
                    {
                        result = propertyTemplate.Replace("{value}", $"{((dividend / divisor) * 100):0.##}%");

                        return true;
                    }
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
        if (cssSelector.UsesModifier)
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