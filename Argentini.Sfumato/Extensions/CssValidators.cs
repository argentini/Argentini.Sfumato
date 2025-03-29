namespace Argentini.Sfumato.Extensions;

public static class CssValidators
{
    #region Identify Value Data Types
    
    private static string GetUnit(this string value)
    {
        var index = 0;

        while (index < value.Length && (char.IsDigit(value[index]) || value[index] == '.'))
            index++;

        return index >= value.Length ? string.Empty : value[index..];         
    }
    
    public static bool ValueIsAlphaNumber(this string value)
    {
        return value.All(c => char.IsDigit(c) || c == '.');
    }

    public static bool ValueIsAngleHue(this string value, AppState appState)
    {
        var unit = GetUnit(value);

        return string.IsNullOrEmpty(unit) == false && appState.Library.CssAngleUnits.Any(u => u == unit);
    }

    public static bool ValueIsColorName(this string value, AppState appState)
    {
        return appState.Library.Colors.ContainsKey(value);
    }

    public static bool ValueIsDimensionLength(this string value, AppState appState)
    {
        var unit = GetUnit(value);

        return string.IsNullOrEmpty(unit) == false && appState.Library.CssLengthUnits.Any(u => u == unit);
    }

    public static bool ValueIsDurationTime(this string value, AppState appState)
    {
        var unit = GetUnit(value);

        return string.IsNullOrEmpty(unit) == false && appState.Library.CssDurationUnits.Any(u => u == unit);
    }

    public static bool ValueIsFrequency(this string value, AppState appState)
    {
        var unit = GetUnit(value);

        return string.IsNullOrEmpty(unit) == false && appState.Library.CssFrequencyUnits.Any(u => u == unit);
    }

    public static bool ValueIsImageUrl(this string value)
    {
        return value.StartsWith("url(", StringComparison.Ordinal) || Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out _);
    }

    public static bool ValueIsInteger(this string value)
    {
        return value.All(char.IsDigit);
    }

    public static bool ValueIsRatio(this string value)
    {
        var segments = value.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (segments.Length != 2)
            return false;
        
        return int.TryParse(segments[0].Trim(), out _) && int.TryParse(segments[1].Trim(), out _);
    }

    public static bool ValueIsPercentage(this string value)
    {
        return value.All(c => char.IsDigit(c) || c == '.') && value.EndsWith('%');
    }

    public static bool ValueIsResolution(this string value, AppState appState)
    {
        var unit = GetUnit(value);

        return string.IsNullOrEmpty(unit) == false && appState.Library.CssResolutionUnits.Any(u => u == unit);
    }

    #endregion
}