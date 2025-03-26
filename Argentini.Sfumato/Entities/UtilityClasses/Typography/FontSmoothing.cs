// ReSharper disable RawStringCanBeSimplified

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontSmoothing : ClassDictionaryBase
{
    public FontSmoothing()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "antialiased", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               -webkit-font-smoothing: antialiased;
                               -moz-osx-font-smoothing: grayscale;
                               """
                }
            },
            {
                "subpixel-antialiased", new ClassDefinition
                {
                    IsSimpleUtility = true,
                    Template = """
                               -webkit-font-smoothing: auto;
                               -moz-osx-font-smoothing: auto;
                               """
                }
            }
        });
    }
}