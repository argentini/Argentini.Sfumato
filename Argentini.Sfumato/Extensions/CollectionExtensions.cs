namespace Argentini.Sfumato.Extensions;

public static class CollectionExtensions
{
    /// <summary>
    /// Add numbered rem size options (e.g. basis-0.5, basis-1.5, etc.).
    /// </summary>
    public static void AddNumberedRemUnitOptions(this Dictionary<string,string> dictionary, decimal minValue, decimal maxValue, decimal step = 0.5m)
    {
        for (var x = minValue; x <= maxValue; x += step)
        {
            if (x == 4)
                step = 1;

            dictionary.TryAdd($"{x:0.#}", $"{x / 4:0.###}rem");
        }
    }

    /// <summary>
    /// Add incremental whole number options that translate to fractional values from zero to one (e.g. ["opacity-5"] => "opacity: 0.05;").
    /// Used by inherited classes.
    /// </summary>
    public static void AddOneBasedPercentageOptions(this Dictionary<string,string> dictionary, decimal minValue, decimal maxValue)
    {
        for (var x = minValue; x <= maxValue; x++)
        {
            var percentage = x / 100m;
            var value = $"{percentage}";
            
            dictionary.TryAdd($"{x}", value);
        }
    }

    /// <summary>
    /// Add incremental whole number options where the prefix and value arte the same (e.g. ["order-1"] => "order: 1;").
    /// Used by inherited classes.
    /// </summary>
    public static void AddWholeNumberOptions(this Dictionary<string,string> dictionary, int minValue, int maxValue, bool isNegative = false)
    {
        for (var x = minValue; x <= maxValue; x++)
        {
            var value = (isNegative ? x * -1 : x).ToString();
            
            dictionary.TryAdd(x.ToString(), value);
        }
    }

    /// <summary>
    /// Add whole number percentage number options that end with '%' e.g. ["50%"] = "50%".
    /// Used by inherited classes.
    /// </summary>
    public static void AddPercentageOptions(this Dictionary<string, string> dictionary, int minValue, int maxValue)
    {
        for (var x = minValue; x <= maxValue; x++)
            dictionary.TryAdd($"{x}%", $"{x}%");
    }
}