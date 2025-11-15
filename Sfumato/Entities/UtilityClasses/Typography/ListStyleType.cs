// ReSharper disable RawStringCanBeSimplified

using Sfumato.Helpers;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

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
                "list-circle", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               list-style-type: circle;
                               """
                }
            },
            {
                "list-square", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               list-style-type: square;
                               """
                }
            },
            {
                "list-mdash", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               list-style-type: "—";
                               """
                }
            },
            {
                "list-ndash", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               list-style-type: "–";
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