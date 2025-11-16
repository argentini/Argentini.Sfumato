// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextOverflow : ClassDictionaryBase
{
    public TextOverflow()
    {
        Group = "text-overflow";
        Description = "Utilities for controlling text overflow and ellipsis.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "truncate", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow: hidden;
                               text-overflow: ellipsis;
                               white-space: nowrap;
                               """
                }
            },
            {
                "text-ellipsis", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-overflow: ellipsis;
                               """
                }
            },
            {
                "text-clip", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               text-overflow: clip;
                               """
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}