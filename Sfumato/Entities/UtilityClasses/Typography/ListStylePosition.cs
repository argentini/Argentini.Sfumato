// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

public sealed class ListStylePosition : ClassDictionaryBase
{
    public ListStylePosition()
    {
        Group = "list-style-position";
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