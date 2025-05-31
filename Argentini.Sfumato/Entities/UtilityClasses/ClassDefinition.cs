// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Argentini.Sfumato.Entities.UtilityClasses;

public sealed class ClassDefinition
{
    /// <summary>
    /// Is a simple utility with static properties (e.g. "antialiased");
    /// is used as-is and doesn't have any custom properties.
    /// </summary>
    public bool InSimpleUtilityCollection { get; set; }

    /// <summary>
    /// Class uses an integer (e.g. "3%")
    /// </summary>
    public bool InPercentageCollection { get; set; }

    /// <summary>
    /// Class uses float (e.g. "leading-[1.3]")
    /// </summary>
    public bool InFloatNumberCollection { get; set; }

    /// <summary>
    /// Class uses angles (e.g. "90deg")
    /// </summary>
    public bool InAngleHueCollection { get; set; }

    /// <summary>
    /// Class uses color (e.g. "#aabbcc")
    /// </summary>
    public bool InColorCollection { get; set; }

    /// <summary>
    /// Class uses length (e.g. "1rem")
    /// </summary>
    public bool InLengthCollection { get; set; }

    /// <summary>
    /// Class uses duration (e.g. "10s")
    /// </summary>
    public bool InDurationCollection { get; set; }

    /// <summary>
    /// Class uses flex (e.g. "1fr")
    /// </summary>
    public bool InFlexCollection { get; set; }

    /// <summary>
    /// Class uses frequency (e.g. "10Hz")
    /// </summary>
    public bool InFrequencyCollection { get; set; }

    /// <summary>
    /// Class uses a URL (e.g. "url('/images/bg.jpg')")
    /// </summary>
    public bool InUrlCollection { get; set; }

    /// <summary>
    /// Class uses an integer (e.g. "3")
    /// </summary>
    public bool InIntegerCollection { get; set; }

    /// <summary>
    /// Class uses a ratio (e.g. "1 / 2")
    /// </summary>
    public bool InRatioCollection { get; set; }

    /// <summary>
    /// Class uses abstract value (e.g. "ui-sans-serif, system-ui")
    /// </summary>
    public bool InAbstractValueCollection { get; set; }

    /// <summary>
    /// Class uses resolution (e.g. "10dpi")
    /// </summary>
    public bool InResolutionCollection { get; set; }

    /// <summary>
    /// Class uses a string (e.g. "'hello world'")
    /// </summary>
    public bool InStringCollection { get; set; }

    /// <summary>
    /// Class uses slash modifiers (e.g. "text-base/2")
    /// </summary>
    public bool UsesSlashModifier { get; set; }

    /// <summary>
    /// CSS class property template (e.g. "font-smoothing: antialiased;", "top: {0};").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for custom value.
    /// </summary>
    public string Template { get; set; } = string.Empty;

    /// <summary>
    /// CSS class property template using a modifier (e.g. "font-size: {0}; line-height: calc(var(--spacing) * {1});").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for custom value.
    /// Use placeholder {1} for slash modifier value.
    /// </summary>
    public string ModifierTemplate { get; set; } = string.Empty;

    /// <summary>
    /// CSS class property template using an arbitrary modifier (e.g. "font-size: {0}; line-height: {1};").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for custom value.
    /// Use placeholder {1} for slash modifier value.
    /// </summary>
    public string ArbitraryModifierTemplate { get; set; } = string.Empty;

    /// <summary>
    /// CSS class property template for arbitrary CSS values (e.g. "text-[1rem]").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for custom value.
    /// </summary>
    public string ArbitraryCssValueTemplate { get; set; } = string.Empty;

    /// <summary>
    /// CSS class property template for custom CSS values using a modifier (e.g. "text-[1rem]/5").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for custom value.
    /// Use placeholder {1} for slash modifier value.
    /// </summary>
    public string ArbitraryCssModifierTemplate { get; set; } = string.Empty;

    /// <summary>
    /// Order output class declarations by this integer (default is 0).
    /// </summary>
    public int SelectorSort { get; set; } = 0;
}

public sealed class ExportItem
{
    public string Category { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public string GroupDescription { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Dictionary<string,ClassDefinition> Usages { get; } = new(StringComparer.Ordinal);
}