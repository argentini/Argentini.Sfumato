// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class TextTransform : ClassDictionaryBase
{
    public TextTransform()
    {
        Description = "";
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