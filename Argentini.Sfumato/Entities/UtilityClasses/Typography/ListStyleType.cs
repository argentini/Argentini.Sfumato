// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class ListStyleType : ClassDictionaryBase
{
    public ListStyleType()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "list-", new ClassDefinition
                {
                    UsesAbstractValue = true,
                    Template = """
                               list-style-type: {0};
                               """
                }
            },
            {
                "list-disc", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               list-style-type: disc;
                               """
                }
            },
            {
                "list-decimal", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               list-style-type: decimal;
                               """
                }
            },
            {
                "list-none", new ClassDefinition
                {
                    IsSimpleUtility = true,
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