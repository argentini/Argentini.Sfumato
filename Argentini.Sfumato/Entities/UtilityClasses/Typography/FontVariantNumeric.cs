// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontVariantNumeric : ClassDictionaryBase
{
    public FontVariantNumeric()
    {
        Description = "Utilities for controlling numeric font variant features.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "normal-nums", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-ordinal: normal;
                               font-variant-numeric: var(--sf-ordinal) var(--sf-slashed-zero) var(--sf-numeric-figure) var(--sf-numeric-spacing) var(--sf-numeric-fraction);
                               """
                }
            },
            {
                "ordinal", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-ordinal: ordinal;
                               font-variant-numeric: var(--sf-ordinal) var(--sf-slashed-zero) var(--sf-numeric-figure) var(--sf-numeric-spacing) var(--sf-numeric-fraction);
                               """
                }
            },
            {
                "slashed-zero", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-slashed-zero: slashed-zero;
                               font-variant-numeric: var(--sf-ordinal) var(--sf-slashed-zero) var(--sf-numeric-figure) var(--sf-numeric-spacing) var(--sf-numeric-fraction);
                               """
                }
            },
            {
                "lining-nums", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-numeric-figure: lining-nums;
                               font-variant-numeric: var(--sf-ordinal) var(--sf-slashed-zero) var(--sf-numeric-figure) var(--sf-numeric-spacing) var(--sf-numeric-fraction);
                               """
                }
            },
            {
                "oldstyle-nums", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-numeric-figure: oldstyle-nums;
                               font-variant-numeric: var(--sf-ordinal) var(--sf-slashed-zero) var(--sf-numeric-figure) var(--sf-numeric-spacing) var(--sf-numeric-fraction);
                               """
                }
            },
            {
                "proportional-nums", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-numeric-spacing: proportional-nums;
                               font-variant-numeric: var(--sf-ordinal) var(--sf-slashed-zero) var(--sf-numeric-figure) var(--sf-numeric-spacing) var(--sf-numeric-fraction);
                               """
                }
            },
            {
                "tabular-nums", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-numeric-spacing: tabular-nums;
                               font-variant-numeric: var(--sf-ordinal) var(--sf-slashed-zero) var(--sf-numeric-figure) var(--sf-numeric-spacing) var(--sf-numeric-fraction);
                               """
                }
            },
            {
                "diagonal-fractions", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-numeric-fraction: diagonal-fractions;
                               font-variant-numeric: var(--sf-ordinal) var(--sf-slashed-zero) var(--sf-numeric-figure) var(--sf-numeric-spacing) var(--sf-numeric-fraction);
                               """
                }
            },
            {
                "stacked-fractions", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               --sf-numeric-fraction: stacked-fractions;
                               font-variant-numeric: var(--sf-ordinal) var(--sf-slashed-zero) var(--sf-numeric-figure) var(--sf-numeric-spacing) var(--sf-numeric-fraction);
                               """
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}