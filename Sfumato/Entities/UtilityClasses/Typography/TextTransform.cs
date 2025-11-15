// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextTransform : ClassDictionaryBase
{
    public TextTransform()
    {
        Group = "text-transform";
        Description = "Utilities for transforming text case.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "uppercase", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-transform: uppercase;
                               """
                }
            },
            {
                "lowercase", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-transform: lowercase;
                               """
                }
            },
            {
                "capitalize", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-transform: capitalize;
                               """
                }
            },
            {
                "normal-case", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-transform: none;
                               """
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}