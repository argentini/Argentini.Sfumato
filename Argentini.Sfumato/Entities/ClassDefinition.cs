// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Argentini.Sfumato.Entities;

public class ClassDefinition
{
    /// <summary>
    /// Is a simple utility with static properties (e.g. "antialiased");
    /// is used as-is and doesn't have any custom properties.
    /// </summary>
    public virtual bool IsSimpleUtility { get; set; }

    /// <summary>
    /// Class uses spacing number (e.g. "m-4")
    /// </summary>
    public virtual bool UsesSpacing { get; set; }

    /// <summary>
    /// Class uses float (e.g. "leading-[1.3]")
    /// </summary>
    public virtual bool UsesAlphaNumber { get; set; }

    /// <summary>
    /// Class uses angles (e.g. "90deg")
    /// </summary>
    public virtual bool UsesAngleHue { get; set; }

    /// <summary>
    /// Class uses color (e.g. "#aabbcc")
    /// </summary>
    public virtual bool UsesColor { get; set; }

    /// <summary>
    /// Class uses length (e.g. "1rem")
    /// </summary>
    public virtual bool UsesDimensionLength { get; set; }

    /// <summary>
    /// Class uses duration (e.g. "10s")
    /// </summary>
    public virtual bool UsesDurationTime { get; set; }

    /// <summary>
    /// Class uses flex (e.g. "1fr")
    /// </summary>
    public virtual bool UsesFlex { get; set; }

    /// <summary>
    /// Class uses frequency (e.g. "10Hz")
    /// </summary>
    public virtual bool UsesFrequency { get; set; }

    /// <summary>
    /// Class uses a URL (e.g. "url('/images/bg.jpg')")
    /// </summary>
    public virtual bool UsesImageUrl { get; set; }

    /// <summary>
    /// Class uses an integer (e.g. "3")
    /// </summary>
    public virtual bool UsesInteger { get; set; }

    /// <summary>
    /// Class uses a percentage (e.g. "50.25%")
    /// </summary>
    public virtual bool UsesPercentage { get; set; }

    /// <summary>
    /// Class uses a ratio (e.g. "1 / 2")
    /// </summary>
    public virtual bool UsesRatio { get; set; }

    /// <summary>
    /// Class uses resolution (e.g. "10dpi")
    /// </summary>
    public virtual bool UsesResolution { get; set; }

    /// <summary>
    /// Class uses a string (e.g. "'hello world'")
    /// </summary>
    public virtual bool UsesString { get; set; }

    /// <summary>
    /// Class uses slash modifiers (e.g. "text-base/2")
    /// </summary>
    public virtual bool UsesSlashModifier { get; set; }

    /// <summary>
    /// CSS class property template (e.g. "font-smoothing: antialiased;", "top: {0};").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for custom value.
    /// </summary>
    public virtual string Template { get; set; } = string.Empty;

    /// <summary>
    /// CSS class property template using a modifier (e.g. "font-size: {0}; line-height: calc(var(--spacing) * {1});").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for custom value.
    /// Use placeholder {1} for slash modifier value.
    /// </summary>
    public virtual string ModifierTemplate { get; set; } = string.Empty;

    /// <summary>
    /// CSS class property template for custom CSS values (e.g. "text-[1rem]").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for custom value.
    /// </summary>
    public virtual string CustomCssTemplate { get; set; } = string.Empty;

    /// <summary>
    /// CSS class property template for custom CSS values using a modifier (e.g. "text-[1rem]/5").
    /// Can contain more than one property assignment.
    /// Use placeholder {0} for custom value.
    /// Use placeholder {1} for slash modifier value.
    /// </summary>
    public virtual string CustomCssModifierTemplate { get; set; } = string.Empty;

    /// <summary>
    /// Property value inserted into Template (e.g. "1rem").
    /// </summary>
    public virtual string Value { get; set; } = string.Empty;

    /// <summary>
    /// List of CSS custom property names used by the utility (e.g. --spacing).
    /// </summary>
    public virtual List<string> UsesCssCustomProperties { get; set; } = [];

    /// <summary>
    /// Order output class declarations by this integer (default is 0).
    /// </summary>
    public virtual int SelectorSort { get; set; } = 0;






}