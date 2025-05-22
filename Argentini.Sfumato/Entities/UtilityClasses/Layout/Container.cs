// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Container : ClassDictionaryBase
{
    public Container()
    {
        Description = "Utilities for constraining and centering layout containers.";
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "@container", new ClassDefinition
                {
                    InSimpleUtilityCollection = true,
                    UsesSlashModifier = true,
                    Template =
                        """
                        container-type: inline-size;
                        """,
                    ModifierTemplate = 
                        """
                        container-type: inline-size;
                        container-name: {1};
                        """
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}