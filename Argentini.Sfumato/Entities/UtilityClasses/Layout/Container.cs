// ReSharper disable RawStringCanBeSimplified

using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Entities.UtilityClasses.Layout;

public sealed class Container : ClassDictionaryBase
{
    public Container()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "@container", new ClassDefinition
                {
                    IsSimpleUtility = true,
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