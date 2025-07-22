namespace Argentini.Sfumato.Validators;

public static class CssValidators
{
    #region Number Validators
    
    public static bool ValueIsFloatNumber(this string value)
    {
        return value.TrimStart('-').All(c => char.IsDigit(c) || c == '.');
    }

    public static bool ValueIsPercentage(this string value)
    {
        return value.TrimStart('-').EndsWith('%') && value.TrimEnd('%').All(c => char.IsDigit(c) || c == '.');
    }

    #endregion
    
    #region Length/Dimension Validators
    
    private static string GetUnit(this string value)
    {
        var index = 0;

        while (index < value.Length && (char.IsDigit(value[index]) || value[index] == '.' || value[index] == '-'))
            index++;

        return index >= value.Length ? string.Empty : value[index..];         
    }

    public static bool ValueIsAngleHue(this string value, AppRunner appRunner)
    {
        if (value == "0")
            return true;

        var unit = GetUnit(value);

        return string.IsNullOrEmpty(unit) == false && appRunner.Library.CssAngleUnits.Any(u => u == unit);
    }

    public static bool ValueIsColorName(this string value, AppRunner appRunner)
    {
        return appRunner.Library.ColorsByName.ContainsKey(value);
    }

    public static bool ValueIsDimensionLength(this string value, AppRunner appRunner)
    {
        if (value == "0")
            return true;
        
        var unit = GetUnit(value);

        return string.IsNullOrEmpty(unit) == false && appRunner.Library.CssLengthUnits.Any(u => u == unit);
    }

    public static bool ValueIsDurationTime(this string value, AppRunner appRunner)
    {
        if (value == "0")
            return true;

        var unit = GetUnit(value);

        return string.IsNullOrEmpty(unit) == false && appRunner.Library.CssDurationUnits.Any(u => u == unit);
    }

    public static bool ValueIsFrequency(this string value, AppRunner appRunner)
    {
        if (value == "0")
            return true;

        var unit = GetUnit(value);

        return string.IsNullOrEmpty(unit) == false && appRunner.Library.CssFrequencyUnits.Any(u => u == unit);
    }

    public static bool ValueIsUrl(this string value)
    {
        return value.StartsWith("url(", StringComparison.Ordinal) || Uri.TryCreate(value, UriKind.Relative, out _) || Uri.TryCreate(value, UriKind.Absolute, out _);
    }

    public static bool ValueIsRatio(this string value)
    {
        var segments = value.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (segments.Length != 2)
            return false;
        
        return int.TryParse(segments[0].Trim(), out _) && int.TryParse(segments[1].Trim(), out _);
    }

    public static bool ValueIsResolution(this string value, AppRunner appRunner)
    {
        if (value == "0")
            return true;

        var unit = GetUnit(value);

        return string.IsNullOrEmpty(unit) == false && appRunner.Library.CssResolutionUnits.Any(u => u == unit);
    }

    #endregion
}