// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Argentini.Sfumato.Entities;

public sealed class CssClass
{
    /// <summary>
    /// Utility class name from scanned files.
    /// (e.g. "dark:tabp:text-base/6")
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Name broken into variant segments;
    /// last segment is the core utility class and any modifier.
    /// (e.g. "dark:tabp:text-base/6" => ["dark", "tabp", "text-base/6"])
    /// </summary>
    public List<string> NameSegments { get; set; } = [];

    /// <summary>
    /// CSS selector generated from the Name; 
    /// (e.g. "dark:tabp:text-base/6" => ".dark\:tabp\:text-base\/6")
    /// </summary>
    public string CssSelector { get; set; } = string.Empty;

    /// <summary>
    /// Determines the order of generated CSS classes;
    /// defaults to zero.
    /// </summary>
    public int SelectorSort { get; set; } = 0;

    /// <summary>
    /// Master class definition for this utility class.
    /// </summary>
    public ClassDefinition? ClassDefinition { get; set; }
    






}