// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class BorderWidth : ClassDictionaryBase
{
    public static readonly Dictionary<string, string> BorderWidths = new ()
    {
        {
            "border",
            """
            border-style: var(--sf-border-style);
            border-width: {0};
            """
        },
        {
            "border-x",
            """
            border-inline-style: var(--sf-border-style);
            border-inline-width: {0};
            """
        },
        {
            "border-y",
            """
            border-block-style: var(--sf-border-style);
            border-block-width: {0};
            """
        },
        {
            "border-s",
            """
            border-inline-start-style: var(--sf-border-style);
            border-inline-start-width: {0};
            """
        },
        {
            "border-e",
            """
            border-inline-end-style: var(--sf-border-style);
            border-inline-end-width: {0};
            """
        },
        {
            "border-t",
            """
            border-top-style: var(--sf-border-style);
            border-top-width: {0};
            """
        },
        {
            "border-r",
            """
            border-right-style: var(--sf-border-style);
            border-right-width: {0};
            """
        },
        {
            "border-b",
            """
            border-bottom-style: var(--sf-border-style);
            border-bottom-width: {0};
            """
        },
        {
            "border-l",
            """
            border-left-style: var(--sf-border-style);
            border-left-width: {0};
            """
        },
    };

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var border in BorderWidths)
        {
            var key = border.Key;
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = border.Value.Replace("{0}", "1px"),
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
            
            key = $"{border.Key}-";
            value = new ClassDefinition
            {
                InLengthCollection = true,
                Template = border.Value.Replace("{0}", "{0}px"),
                ArbitraryCssValueTemplate = border.Value,
            };

            if (appRunner.Library.LengthClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.LengthClasses[key] = value;
        }
    }
}
