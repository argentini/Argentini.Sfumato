using Argentini.Sfumato.Entities.UtilityClasses;

namespace Argentini.Sfumato.Tests.Export;

// ReSharper disable CollectionNeverQueried.Global
public sealed class ExportItem
{
    public string Category { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public string GroupDescription { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Dictionary<string, ClassDefinition> Usages { get; set; } = new();
}