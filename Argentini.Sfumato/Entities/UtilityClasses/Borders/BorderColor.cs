// ReSharper disable RawStringCanBeSimplified

namespace Argentini.Sfumato.Entities.UtilityClasses.Borders;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class BorderColor : ClassDictionaryBase
{
    public BorderColor()
    {
        Description = "Utilities for setting border color.";
    }

    public static readonly Dictionary<string, string> BorderColors = new ()
    {
        {
            "border",
            """
            border-color: {0};
            """
        },
        {
            "border-x",
            """
            border-inline-color: {0};
            """
        },
        {
            "border-y",
            """
            border-block-color: {0};
            """
        },
        {
            "border-s",
            """
            border-inline-start-color: {0};
            """
        },
        {
            "border-e",
            """
            border-inline-end-color: {0};
            """
        },
        {
            "border-t",
            """
            border-top-color: {0};
            """
        },
        {
            "border-r",
            """
            border-right-color: {0};
            """
        },
        {
            "border-b",
            """
            border-bottom-color: {0};
            """
        },
        {
            "border-l",
            """
            border-left-color: {0};
            """
        },
    };

    public override void ProcessThemeSettings(AppRunner appRunner)
    {
        foreach (var border in BorderColors)
        {
            var key = $"{border.Key}-inherit";
            var value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = border.Value.Replace("{0}", "inherit"),
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;

            key = $"{border.Key}-current";
            value = new ClassDefinition
            {
                InSimpleUtilityCollection = true,
                Template = border.Value.Replace("{0}", "currentColor"),
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
            
            key = $"{border.Key}-";
            value = new ClassDefinition
            {
                InColorCollection = true,
                Template = border.Value,
                ArbitraryCssValueTemplate = border.Value,
            };

            if (appRunner.Library.ColorClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.ColorClasses[key] = value;
        }
    }
}
