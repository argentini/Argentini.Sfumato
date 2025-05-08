// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextTransform : ClassDictionaryBase
{
    public TextTransform()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "uppercase", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-transform: uppercase;
                               """
                }
            },
            {
                "lowercase", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-transform: lowercase;
                               """
                }
            },
            {
                "capitalize", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               text-transform: capitalize;
                               """
                }
            },
            {
                "normal-case", new ClassDefinition
                {
                    IsSimpleUtility = true,
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