// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Typography;

public sealed class FontFeatures : ClassDictionaryBase
{
    public FontFeatures()
    {
        Group = "font-feature-settings";
        Description = "Utilities for setting OpenType font features.";
        Data.Add("font-features-", new ClassDefinition
        {
            InAbstractValueCollection = true,
            ArbitraryCssValueTemplate = "font-feature-settings: {0};",
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
