// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextOverflow : ClassDictionaryBase
{
    public TextOverflow()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "truncate", new ClassDefinition
                {
                    IsSimpleUtility = true,
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
                    IsSimpleUtility = true,
                    Template = """
                               text-overflow: ellipsis;
                               """
                }
            },
            {
                "text-clip", new ClassDefinition
                {
                    IsSimpleUtility = true,
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