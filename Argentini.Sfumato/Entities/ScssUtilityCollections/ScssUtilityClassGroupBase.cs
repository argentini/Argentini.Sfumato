namespace Argentini.Sfumato.Entities.ScssUtilityCollections;

public abstract class ScssUtilityClassGroupBase
{
    public virtual string SelectorPrefix { get; set; } = string.Empty;
    public virtual string Category { get; set; } = string.Empty;
    public List<string> SelectorIndex { get; set; } = new();
    public int SelectorSort { get; set; }
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
    /// Add a vendor prefix variation to a property template.
    /// </summary>
    /// <example>
    /// Converts:
    /// <code>
    /// backdrop-filter: blur(1rem);
    /// </code>
    /// To this:
    /// backdrop-filter: blur(1rem); -webkit-backdrop-filter: blur(1rem);
    /// </example>
    /// <param name="propertyTemplate"></param>
    /// <param name="appState"></param>
    /// <returns></returns>
    protected static string AddVendorPrefixedProperty(string propertyTemplate, SfumatoAppState? appState)
    {
        if (appState is null)
            return propertyTemplate;
        
        if (propertyTemplate.Replace("{value}", string.Empty).Contains('{') || propertyTemplate.Contains("@media") || propertyTemplate.Contains("@font-face") || propertyTemplate.Contains("@import") || propertyTemplate.Contains("@keyframes") || propertyTemplate.Contains("@supports") || propertyTemplate.Contains('\n'))
            return propertyTemplate;
        
        var result = appState.StringBuilderPool.Get();

        try
        {
            var splits = propertyTemplate.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var propStatement in splits)
            {
                result.Append($"{propStatement};");

                var firstColonIndex = propStatement.IndexOf(':');

                if (firstColonIndex < 1 || firstColonIndex == propertyTemplate.Length - 1)
                    continue;

                var propertyName = propStatement[..firstColonIndex].Trim();
                var propertyValue = propStatement[(firstColonIndex + 1)..].Trim();

                if (propertyName == string.Empty || propertyValue == string.Empty || propertyName.StartsWith("-webkit-"))
                    continue;

                var availableInChrome = appState.ValidChromeCssPropertyNames.Contains(propertyName);
                var availableInSafari = appState.ValidSafariCssPropertyNames.Contains(propertyName);

                if (availableInChrome && availableInSafari)
                    continue;

                var vendorPrefixed = $"-webkit-{propertyName}";

                availableInChrome = appState.ValidChromeCssPropertyNames.Contains(vendorPrefixed);
                availableInSafari = appState.ValidSafariCssPropertyNames.Contains(vendorPrefixed);

                if (availableInChrome || availableInSafari)
                    result.Append($" {vendorPrefixed}: {propertyValue};");
            }

            return result.ToString();
        }

        catch
        {
            return propertyTemplate;
        }

        finally
        {
            appState.StringBuilderPool.Return(result);
        }
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
    /// <param name="appState"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected static bool ProcessStaticDictionaryOptions(Dictionary<string, string>? dictionary, CssSelector cssSelector, SfumatoAppState? appState, out string result)
    {
        result = string.Empty;

        if (cssSelector.HasArbitraryValue)
            return false;

        if ((dictionary?.TryGetValue(cssSelector.CoreSegment, out var value) ?? false) == false)
            return false;
        
        result = AddVendorPrefixedProperty(value, appState);

        return true;
    }
    
    /// <summary>
    /// Process a dictionary (calculated) option in a utility class's GetStyles() method.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="cssSelector"></param>
    /// <param name="propertyTemplate"></param>
    /// <param name="appState"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected static bool ProcessDictionaryOptions(Dictionary<string, string>? dictionary, CssSelector cssSelector, string propertyTemplate, SfumatoAppState? appState, out string result)
    {
        result = string.Empty;

        if (cssSelector.HasArbitraryValue)
            return false;

        if ((dictionary?.TryGetValue(cssSelector.CoreSegment, out var value) ?? false) == false)
            return false;
        
        if (dictionary == cssSelector.AppState?.ColorOptions)
            value = value.WebColorToRgba();

        result = AddVendorPrefixedProperty(propertyTemplate, appState).Replace("{value}", value);
                
        return true;
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
        result = string.Empty;

        if (cssSelector.HasArbitraryValue)
            return false;

        if ((list?.Contains(cssSelector.CoreSegment) ?? false) == false)
            return false;
        
        result = propertyTemplate.Replace("{value}", cssSelector.CoreSegment);

        return true;
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
        result = string.Empty;

        if (cssSelector.UsesModifier == false)
            return false;

        if ((cssSelector.AppState?.ColorOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out var color) ?? false) == false)
            return false;
        
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

        if (valueType != "number")
            return false;
        
        color = color.WebColorToRgba(decimal.Parse(modifierValue));
        result = propertyTemplate.Replace("{value}", color);

        return true;
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
        result = string.Empty;

        if (cssSelector.UsesModifier == false)
            return false;
        
        var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;

        if (valueType != "integer")
            return false;

        if (decimal.TryParse(cssSelector.CoreSegment, out var dividend) == false)
            return false;
        
        var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

        if (decimal.TryParse(modifierValue, out var divisor) == false)
            return false;
        
        result = propertyTemplate.Replace("{value}", $"{((dividend / divisor) * 100):0.##}%");

        return true;
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
        result = string.Empty;

        if (cssSelector.UsesModifier == false)
            return false;

        if ((cssSelector.AppState?.TextSizeOptions.TryGetValue(cssSelector.CoreSegment.TrimEnd(cssSelector.ModifierSegment) ?? string.Empty, out var fontSize) ?? false) == false)
            return false;
        
        var valueType = cssSelector.HasModifierValue ? cssSelector.ModifierValueType : cssSelector.ArbitraryValueType;

        if (valueType is "length" or "percentage")
        {
            var modifierValue = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

            result = propertyTemplate.Replace("{fontSize}", fontSize).Replace("{value}", modifierValue);
            return true;
        }

        if (valueType is not ("integer" or "number" or ""))
            return false;
        
        var modifierValue2 = cssSelector.HasModifierValue ? cssSelector.ModifierValue : cssSelector.ArbitraryValue;

        if (valueType is "integer" or "" && cssSelector.AppState.LeadingOptions.TryGetValue(modifierValue2, out var option))
        {
            result = propertyTemplate.Replace("{fontSize}", fontSize).Replace("{value}", option);
            return true;
        }

        if (valueType is not ("integer" or "number"))
            return false;
        
        result = propertyTemplate.Replace("{fontSize}", fontSize).Replace("{value}", modifierValue2);
        
        return true;
    }
    
    /// <summary>
    /// Process an arbitrary value in a utility class's GetStyles() method.
    /// </summary>
    /// <param name="valueTypes"></param>
    /// <param name="cssSelector"></param>
    /// <param name="propertyTemplate"></param>
    /// <param name="appState"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected static bool ProcessArbitraryValues(string valueTypes, CssSelector cssSelector, string propertyTemplate, SfumatoAppState? appState, out string result)
    {
        result = string.Empty;

        if (cssSelector.HasArbitraryValue == false)
            return false;
        
        var valueTypesArray = valueTypes.Split(',');

        if (valueTypesArray.Contains(cssSelector.ArbitraryValueType) == false && valueTypes != string.Empty && valueTypesArray.Length != 0)
            return false;
        
        result = AddVendorPrefixedProperty(propertyTemplate, appState).Replace("{value}", cssSelector.ArbitraryValue);

        return true;
    }
}