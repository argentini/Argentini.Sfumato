// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class LineClamp : ClassDictionaryBase
{
    public LineClamp()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "line-clamp-", new ClassDefinition
                {
                    InIntegerCollection = true,
                    Template = """
                               overflow: hidden;
                               display: -webkit-box;
                               -webkit-box-orient: vertical;
                               -webkit-line-clamp: {0};
                               """
                }
            },
            {
                "line-clamp-none", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               overflow: visible;
                               display: block;
                               -webkit-box-orient: horizontal;
                               -webkit-line-clamp: unset;
                               """
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}