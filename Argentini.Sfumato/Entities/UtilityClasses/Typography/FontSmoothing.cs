// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontSmoothing : ClassDictionaryBase
{
    public FontSmoothing()
    {
        Description = "Utilities for controlling font smoothing and antialiasing.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "antialiased", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               -webkit-font-smoothing: antialiased;
                               -moz-osx-font-smoothing: grayscale;
                               """
                }
            },
            {
                "subpixel-antialiased", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               -webkit-font-smoothing: auto;
                               -moz-osx-font-smoothing: auto;
                               """
                }
            }
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}