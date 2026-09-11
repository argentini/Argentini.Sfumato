// ReSharper disable RawStringCanBeSimplified

using Sfumato.Entities.Runners;

namespace Sfumato.Entities.UtilityClasses.Interactivity;

public sealed class Zoom : ClassDictionaryBase
{
    public Zoom()
    {
        Group = "zoom";
        Description = "Utilities for controlling element magnification.";
        Data.Add("zoom-", new ClassDefinition
        {
            InIntegerCollection = true,
            Template = "zoom: {0}%;",
            ArbitraryCssValueTemplate = "zoom: {0};",
        });
    }

    public override void ProcessThemeSettings(AppRunner appRunner)
    {}
}
