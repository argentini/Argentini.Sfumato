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
            border-width: {0};
            """
        },
        {
            "border-x",
            """
            border-inline-width: {0};
            """
        },
        {
            "border-y",
            """
            border-block-width: {0};
            """
        },
        {
            "border-s",
            """
            border-inline-start-width: {0};
            """
        },
        {
            "border-e",
            """
            border-inline-end-width: {0};
            """
        },
        {
            "border-t",
            """
            border-top-width: {0};
            """
        },
        {
            "border-r",
            """
            border-right-width: {0};
            """
        },
        {
            "border-b",
            """
            border-bottom-width: {0};
            """
        },
        {
            "border-l",
            """
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
                IsSimpleUtility = true,
                Template = border.Value.Replace("{0}", "1px"),
            };

            if (appRunner.Library.SimpleClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.SimpleClasses[key] = value;
            
            key = $"{border.Key}-";
            value = new ClassDefinition
            {
                UsesInteger = true,
                UsesDimensionLength = true,
                Template = border.Value.Replace("{0}", "{0}px"),
                ArbitraryCssValueTemplate = border.Value,
            };

            if (appRunner.Library.IntegerClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.IntegerClasses[key] = value;

            if (appRunner.Library.DimensionLengthClasses.TryAdd(key, value))
                appRunner.Library.ScannerClassNamePrefixes.Insert(key);
            else
                appRunner.Library.DimensionLengthClasses[key] = value;
        }
    }
}
