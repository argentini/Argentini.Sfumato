using System.Reflection;
using Sfumato.Entities.Trie;
using Sfumato.Entities.UtilityClasses;

namespace Sfumato.Entities.Library;

internal sealed class UtilityClassRegistry
{
    internal static UtilityClassRegistry Instance { get; } = new();

    internal ClassDictionaryBase[] ThemeHandlers { get; }

    internal PrefixTrie<ClassDefinition> SimpleClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> AbstractClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> AngleHueClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> ColorClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> DurationClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> FlexClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> FloatNumberClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> FrequencyClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> IntegerClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> LengthClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> PercentageClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> RatioClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> ResolutionClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> StringClasses { get; } = new();
    internal PrefixTrie<ClassDefinition> UrlClasses { get; } = new();
    internal PrefixTrie<object?> ScannerClassNamePrefixes { get; } = new();

    private UtilityClassRegistry()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        var handlers = new List<ClassDictionaryBase>();

        for (var i = 0; i < types.Length; i++)
        {
            var type = types[i];

            if (type.IsClass == false || type.IsAbstract || typeof(ClassDictionaryBase).IsAssignableFrom(type) == false)
                continue;

            if (Activator.CreateInstance(type) is not ClassDictionaryBase handler)
                continue;

            handlers.Add(handler);
            AddDefinitions(handler);
        }

        ThemeHandlers = handlers.ToArray();
    }

    private void AddDefinitions(ClassDictionaryBase handler)
    {
        foreach (var item in handler.Data)
        {
            if (item.Key.EndsWith('(') || item.Key.EndsWith('['))
                continue;

            var definition = item.Value;

            if (definition.InAbstractValueCollection)
                AbstractClasses.Add(item.Key, definition);

            if (definition.InSimpleUtilityCollection)
                SimpleClasses.Add(item.Key, definition);

            if (definition.InFloatNumberCollection)
                FloatNumberClasses.Add(item.Key, definition);

            if (definition.InAngleHueCollection)
                AngleHueClasses.Add(item.Key, definition);

            if (definition.InColorCollection)
                ColorClasses.Add(item.Key, definition);

            if (definition.InLengthCollection)
                LengthClasses.Add(item.Key, definition);

            if (definition.InDurationCollection)
                DurationClasses.Add(item.Key, definition);

            if (definition.InFlexCollection)
                FlexClasses.Add(item.Key, definition);

            if (definition.InFrequencyCollection)
                FrequencyClasses.Add(item.Key, definition);

            if (definition.InUrlCollection)
                UrlClasses.Add(item.Key, definition);

            if (definition.InIntegerCollection)
                IntegerClasses.Add(item.Key, definition);

            if (definition.InPercentageCollection)
                PercentageClasses.Add(item.Key, definition);

            if (definition.InRatioCollection)
                RatioClasses.Add(item.Key, definition);

            if (definition.InResolutionCollection)
                ResolutionClasses.Add(item.Key, definition);

            if (definition.InStringCollection)
                StringClasses.Add(item.Key, definition);

            ScannerClassNamePrefixes.Insert(item.Key, null);
        }
    }
}
