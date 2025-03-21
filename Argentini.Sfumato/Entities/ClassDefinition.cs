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
    public virtual bool UsesNumber { get; set; }

    /// <summary>
    /// Class uses length (e.g. "1rem")
    /// </summary>
    public virtual bool UsesLength { get; set; }

    /// <summary>
    /// Class uses color (e.g. "#aabbcc")
    /// </summary>
    public virtual bool UsesColor { get; set; }

    /// <summary>
    /// Class uses duration (e.g. "10s")
    /// </summary>
    public virtual bool UsesDuration { get; set; }

    /// <summary>
    /// Class uses angles (e.g. "90deg")
    /// </summary>
    public virtual bool UsesAngle { get; set; }

    /// <summary>
    /// Class uses frequency (e.g. "10Hz")
    /// </summary>
    public virtual bool UsesFrequency { get; set; }

    /// <summary>
    /// Class uses resolution (e.g. "10dpi")
    /// </summary>
    public virtual bool UsesResolution { get; set; }

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