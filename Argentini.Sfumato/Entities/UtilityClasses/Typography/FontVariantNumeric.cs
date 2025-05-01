// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontVariantNumeric : ClassDictionaryBase
{
    public FontVariantNumeric()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "normal-nums", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-variant-numeric: normal;
                               """
                }
            },
            {
                "ordinal", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-variant-numeric: ordinal;
                               """
                }
            },
            {
                "slashed-zero", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-variant-numeric: slashed-zero;
                               """
                }
            },
            {
                "lining-nums", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-variant-numeric: lining-nums;
                               """
                }
            },
            {
                "oldstyle-nums", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-variant-numeric: oldstyle-nums;
                               """
                }
            },
            {
                "proportional-nums", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-variant-numeric: proportional-nums;
                               """
                }
            },
            {
                "tabular-nums", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-variant-numeric: tabular-nums;
                               """
                }
            },
            {
                "diagonal-fractions", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-variant-numeric: diagonal-fractions;
                               """
                }
            },
            {
                "stacked-fractions", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               font-variant-numeric: stacked-fractions;
                               """
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}