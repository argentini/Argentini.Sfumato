// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class TabSize : ClassDictionaryBase
{
    public TabSize()
    {
        Group = "tab-size";
        Description = "Utilities for controlling tab character width.";
        Data.Add("tab-", new ClassDefinition
        {
            InIntegerCollection = true,
            InLengthCollection = true,
            ArbitraryLengthOnly = true,
            DisallowsFractions = true,
            Template = "tab-size: {0};",
            ArbitraryCssValueTemplate = "tab-size: {0};",
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
