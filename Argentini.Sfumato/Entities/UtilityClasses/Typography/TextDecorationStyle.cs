// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextDecorationStyle : ClassDictionaryBase
{
    public TextDecorationStyle()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "decoration-solid", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-decoration-style: solid;
                               """
                }
            },
            {
                "decoration-double", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-decoration-style: double;
                               """
                }
            },
            {
                "decoration-dotted", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-decoration-style: dotted;
                               """
                }
            },
            {
                "decoration-dashed", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-decoration-style: dashed;
                               """
                }
            },
            {
                "decoration-wavy", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-decoration-style: wavy;
                               """
                }
            },
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}