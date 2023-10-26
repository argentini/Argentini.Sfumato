namespace Argentini.Sfumato.ScssUtilityCollections.Entities;

public static class CollectionExtensions
{
    /// <summary>
    /// Add numbered rem size classes (e.g. basis-0.5, basis-1.5, etc.).
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
    /// Return classes for incremental whole numbers where that translate to fractional values from zero to one (e.g. ["opacity-5"] => "opacity: 0.05;").
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
    /// Return classes for incremental whole numbers where the prefix and value arte the same (e.g. ["order-1"] => "order: 1;").
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
    /// Add options for fractions from 1/2 up through 11/12, and "full" =&gt; 100%.
    /// Used by inherited classes.
    /// </summary>
    public static void AddVerbatimFractionOptions(this Dictionary<string,string> dictionary, Dictionary<string,string> fractionOptions)
    {
        foreach (var percentage in fractionOptions)
            dictionary.TryAdd(percentage.Key, percentage.Value);
    }
}