// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class ListStylePosition : ClassDictionaryBase
{
    public ListStylePosition()
    {
        Description = "Utilities for controlling position of list markers.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "list-inside", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               list-style-position: inside;
                               """
                }
            },
            {
                "list-outside", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               list-style-position: outside;
                               """
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}