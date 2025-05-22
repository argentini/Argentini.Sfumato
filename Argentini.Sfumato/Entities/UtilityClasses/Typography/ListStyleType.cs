// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class ListStyleType : ClassDictionaryBase
{
    public ListStyleType()
    {
        Group = "list-style-type";
        Description = "Utilities for setting the type of list marker.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "list-", new ClassDefinition
                {
                    InAbstractValueCollection = true,
                    Template = """
                               list-style-type: {0};
                               """
                }
            },
            {
                "list-disc", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               list-style-type: disc;
                               """
                }
            },
            {
                "list-decimal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               list-style-type: decimal;
                               """
                }
            },
            {
                "list-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               list-style-type: none;
                               """
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}