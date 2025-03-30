// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Argentini.Sfumato.Entities.UtilityClasses;

public sealed class ClassDefinition
{
    /// <summary>
    /// Is a simple utility with static properties (e.g. "antialiased");
    /// is used as-is and doesn't have any custom properties.
    /// </summary>
    public bool IsSimpleUtility { get; set; }

    /// <summary>
    /// Class uses spacing number (e.g. "m-4")
    /// </summary>
    public bool UsesSpacing { get; set; }

    /// <summary>
    /// Class uses float (e.g. "leading-[1.3]")
    /// </summary>
    public bool UsesAlphaNumber { get; set; }

    /// <summary>
    /// Class uses angles (e.g. "90deg")
    /// </summary>
    public bool UsesAngleHue { get; set; }

    /// <summary>
    /// Class uses color (e.g. "#aabbcc")
    /// </summary>
    public bool UsesColor { get; set; }

    /// <summary>
    /// Class uses length (e.g. "1rem")
    /// </summary>
    public bool UsesDimensionLength { get; set; }

    /// <summary>
    /// Class uses duration (e.g. "10s")
    /// </summary>
    public bool UsesDurationTime { get; set; }

    /// <summary>
    /// Class uses flex (e.g. "1fr")
    /// </summary>
    public bool UsesFlex { get; set; }

    /// <summary>
    /// Class uses frequency (e.g. "10Hz")
    /// </summary>
    public bool UsesFrequency { get; set; }

    /// <summary>
    /// Class uses a URL (e.g. "url('/images/bg.jpg')")
    /// </summary>
    public bool UsesImageUrl { get; set; }

    /// <summary>
    /// Class uses an integer (e.g. "3")
    /// </summary>
    public bool UsesInteger { get; set; }

    /// <summary>
    /// Class uses a ratio (e.g. "1 / 2")
    /// </summary>
    public bool UsesRatio { get; set; }

    /// <summary>
    /// Class uses resolution (e.g. "10dpi")
    /// </summary>
    public bool UsesResolution { get; set; }

    /// <summary>
    /// Class uses a string (e.g. "'hello world'")
    /// </summary>
    public bool UsesString { get; set; }

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
    /// List of CSS custom property names used by the utility (e.g. --spacing).
    /// </summary>
    public List<string> UsesCssCustomProperties { get; set; } = [];

    /// <summary>
    /// Order output class declarations by this integer (default is 0).
    /// </summary>
    public int SelectorSort { get; set; } = 0;
}