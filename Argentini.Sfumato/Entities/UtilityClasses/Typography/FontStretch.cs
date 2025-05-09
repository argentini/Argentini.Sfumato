// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontStretch : ClassDictionaryBase
{
    public FontStretch()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "font-stretch-", new ClassDefinition
                {
                    InPercentageCollection = true,
                    InAbstractValueCollection = true,
                    Template = """
                               font-stretch: {0};
                               """,
                    ArbitraryCssValueTemplate =
                        """
                        font-stretch: {0};
                        """
                }
            },
            {
                "font-stretch-ultra-condensed", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-stretch: ultra-condensed;
                               """
                }
            },
            {
                "font-stretch-extra-condensed", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-stretch: extra-condensed;
                               """
                }
            },
            {
                "font-stretch-condensed", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-stretch: condensed;
                               """
                }
            },
            {
                "font-stretch-semi-condensed", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-stretch: semi-condensed;
                               """
                }
            },
            {
                "font-stretch-normal", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-stretch: normal;
                               """
                }
            },
            {
                "font-stretch-semi-expanded", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-stretch: semi-expanded;
                               """
                }
            },
            {
                "font-stretch-expanded", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-stretch: expanded;
                               """
                }
            },
            {
                "font-stretch-extra-expanded", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-stretch: extra-expanded;
                               """
                }
            },
            {
                "font-stretch-ultra-expanded", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-stretch: ultra-expanded;
                               """
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}