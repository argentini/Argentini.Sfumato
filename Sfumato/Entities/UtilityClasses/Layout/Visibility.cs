// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Layout;

public sealed class Visibility : ClassDictionaryBase
{
    public Visibility()
    {
        Group = "visibility";
        Description = "Utilities for controlling the visibility of elements.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "visible", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               visibility: visible;
                               """
                }
            },
            {
                "invisible", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               visibility: hidden;
                               """
                }
            },
            {
                "collapse", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    Template = """
                               visibility: collapse;
                               """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}