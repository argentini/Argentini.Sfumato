// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Backgrounds;

public sealed class FromColor : ClassDictionaryBase
{
    public FromColor()
    {
        Data.AddRange(new Dictionary<string, ClassDefinition>(StringComparer.Ordinal)
        {
            {
                "from-", new ClassDefinition
                {
                    UsesColor = true,
                    SelectorSort = 1,
                    Template =
                        """
                        --sf-gradient-from: {0};
                        --sf-gradient-stops: var(--sf-gradient-via-stops, var(--sf-gradient-position), var(--sf-gradient-from) var(--sf-gradient-from-position), var(--sf-gradient-to) var(--sf-gradient-to-position));
                        """,
                    UsesCssCustomProperties = [ "--sf-gradient-from", "--sf-gradient-stops", "--sf-gradient-via-stops", "--sf-gradient-position", "--sf-gradient-from-position", "--sf-gradient-to", "--sf-gradient-to-position" ]
                }
            },
        });
    }
    
    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}