using System.Text.Json;
using Sfumato.Entities.UtilityClasses;

namespace Sfumato.Tests.Export;

public sealed class UmbracoHelpExportContractTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public async Task UtilityHelpExportDoesNotMutateCompilerDefinitions()
    {
        var appRunner = await CreateLoadedRunnerAsync();
        var registryBefore = CaptureRegistryDocumentation();
        var libraryBefore = CaptureLibraryIndexes(appRunner);

        using var compilationBefore = new CssClass(appRunner, selector: "zoom-50");

        var json = appRunner.ExportUtilityClassDefinitions();

        using var compilationAfter = new CssClass(appRunner, selector: "zoom-50");

        Assert.NotEmpty(json);
        Assert.Equal(registryBefore, CaptureRegistryDocumentation());
        Assert.Equal(libraryBefore, CaptureLibraryIndexes(appRunner));
        Assert.True(compilationBefore.IsValid);
        Assert.True(compilationAfter.IsValid);
        Assert.Equal(compilationBefore.EscapedSelector, compilationAfter.EscapedSelector);
        Assert.Equal(compilationBefore.Styles, compilationAfter.Styles);
    }

    [Fact]
    public async Task HelpExportsAreDeterministicAcrossFreshRunners()
    {
        var first = GenerateExports(await CreateLoadedRunnerAsync());
        var second = GenerateExports(await CreateLoadedRunnerAsync());

        Assert.Equal(first.Keys.ToArray(), second.Keys.ToArray());

        foreach (var (fileName, json) in first)
            Assert.Equal(json, second[fileName]);
    }

    [Fact]
    public async Task HelpExportFilenamesAndJsonSchemasMatchUmbracoContract()
    {
        var exports = GenerateExports(await CreateLoadedRunnerAsync());
        using var contract = LoadContract();
        var root = contract.RootElement;
        var specifications = root.GetProperty("exports").EnumerateArray().ToArray();
        var expectedFileNames = specifications
            .Select(item => item.GetProperty("fileName").GetString())
            .ToArray();

        Assert.Equal(1, root.GetProperty("schemaVersion").GetInt32());
        Assert.Equal(expectedFileNames, exports.Keys.ToArray());

        for (var index = 0; index < specifications.Length; index++)
        {
            var specification = specifications[index];
            var fileName = Assert.IsType<string>(specification.GetProperty("fileName").GetString());

            using var payload = JsonDocument.Parse(exports[fileName]);

            AssertPayloadMatchesContract(payload.RootElement, specification);
        }
    }

    private async Task<AppRunner> CreateLoadedRunnerAsync()
    {
        var appRunner = new AppRunner(new StringBuilderPool(), ExportCssFilePath);

        Assert.True(await appRunner.LoadCssFileAsync());

        return appRunner;
    }

    private static Dictionary<string, string> GenerateExports(AppRunner appRunner)
    {
        return new Dictionary<string, string>(StringComparer.Ordinal)
        {
            [UmbracoHelpExportNames.UtilityClasses] = appRunner.ExportUtilityClassDefinitions(),
            [UmbracoHelpExportNames.Colors] = appRunner.ExportColorDefinitions(),
            [UmbracoHelpExportNames.CssCustomProperties] = appRunner.ExportCssCustomProperties(),
            [UmbracoHelpExportNames.Variants] = appRunner.ExportVariants()
        };
    }

    private static JsonDocument LoadContract()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Fixtures", "Umbraco", "v1", "help-export-contract.json");

        return JsonDocument.Parse(File.ReadAllText(path));
    }

    private static string CaptureRegistryDocumentation()
    {
        return JsonSerializer.Serialize(BuiltInUtilityRegistry.All.Select(utility => new
        {
            Type = utility.GetType().FullName,
            Definitions = utility.Data.Select(definition => new
            {
                definition.Key,
                definition.Value.DocDefinitions,
                definition.Value.DocExamples
            }).ToArray()
        }).ToArray());
    }

    private static string CaptureLibraryIndexes(AppRunner appRunner)
    {
        return JsonSerializer.Serialize(new
        {
            ScannerPrefixes = appRunner.Library.ScannerClassNamePrefixes.Count,
            Simple = appRunner.Library.SimpleClasses.Count,
            Abstract = appRunner.Library.AbstractClasses.Count,
            AngleHue = appRunner.Library.AngleHueClasses.Count,
            Color = appRunner.Library.ColorClasses.Count,
            Duration = appRunner.Library.DurationClasses.Count,
            Flex = appRunner.Library.FlexClasses.Count,
            FloatNumber = appRunner.Library.FloatNumberClasses.Count,
            Frequency = appRunner.Library.FrequencyClasses.Count,
            Integer = appRunner.Library.IntegerClasses.Count,
            Length = appRunner.Library.LengthClasses.Count,
            Percentage = appRunner.Library.PercentageClasses.Count,
            Ratio = appRunner.Library.RatioClasses.Count,
            Resolution = appRunner.Library.ResolutionClasses.Count,
            String = appRunner.Library.StringClasses.Count,
            Url = appRunner.Library.UrlClasses.Count
        });
    }

    private static void AssertPayloadMatchesContract(JsonElement payload, JsonElement specification)
    {
        var expectedKind = ParseKind(specification.GetProperty("rootKind").GetString());
        var minimumItems = specification.GetProperty("minimumItems").GetInt32();

        Assert.Equal(expectedKind, payload.ValueKind);
        Assert.True(GetCount(payload) >= minimumItems);

        if (specification.TryGetProperty("itemSchema", out var itemSchema))
            AssertArraySchema(payload, specification, itemSchema);

        if (specification.TryGetProperty("dictionaryValueKind", out var dictionaryValueKind))
            AssertDictionaryValueKinds(payload, ParseKind(dictionaryValueKind.GetString()));

        if (specification.TryGetProperty("dictionaryValueSchema", out var dictionaryValueSchema))
            foreach (var item in payload.EnumerateObject())
                AssertObjectSchema(item.Value, dictionaryValueSchema);

        if (specification.TryGetProperty("requiredKeys", out var requiredKeys))
            foreach (var requiredKey in requiredKeys.EnumerateArray())
                Assert.True(payload.TryGetProperty(requiredKey.GetString()!, out _), requiredKey.GetString());
    }

    private static void AssertArraySchema(JsonElement payload, JsonElement specification, JsonElement itemSchema)
    {
        var requiredNames = specification.TryGetProperty("requiredNames", out var names)
            ? names.EnumerateArray().Select(item => item.GetString()!).ToHashSet(StringComparer.Ordinal)
            : [];
        var seenNames = new HashSet<string>(StringComparer.Ordinal);
        var nestedDictionaryProperty = specification.TryGetProperty("nestedDictionaryProperty", out var nestedProperty)
            ? nestedProperty.GetString()
            : null;
        var hasNestedSchema = specification.TryGetProperty("nestedValueSchema", out var nestedValueSchema);
        var nestedValues = 0;

        foreach (var item in payload.EnumerateArray())
        {
            AssertObjectSchema(item, itemSchema);

            if (item.TryGetProperty("name", out var name))
                seenNames.Add(name.GetString()!);

            if (hasNestedSchema == false || string.IsNullOrEmpty(nestedDictionaryProperty))
                continue;

            foreach (var nestedValue in item.GetProperty(nestedDictionaryProperty).EnumerateObject())
            {
                AssertObjectSchema(nestedValue.Value, nestedValueSchema);
                AssertStringDictionary(nestedValue.Value.GetProperty("docDefinitions"));
                AssertStringDictionary(nestedValue.Value.GetProperty("docExamples"));
                nestedValues++;
            }
        }

        Assert.True(nestedValues > 0);

        foreach (var requiredName in requiredNames)
            Assert.Contains(requiredName, seenNames);
    }

    private static void AssertObjectSchema(JsonElement value, JsonElement schema)
    {
        Assert.Equal(JsonValueKind.Object, value.ValueKind);

        var expectedNames = schema.EnumerateObject()
            .Select(item => item.Name)
            .OrderBy(item => item, StringComparer.Ordinal)
            .ToArray();
        var actualNames = value.EnumerateObject()
            .Select(item => item.Name)
            .OrderBy(item => item, StringComparer.Ordinal)
            .ToArray();

        Assert.Equal(expectedNames, actualNames);

        foreach (var expectedProperty in schema.EnumerateObject())
            AssertKind(expectedProperty.Value.GetString(), value.GetProperty(expectedProperty.Name).ValueKind);
    }

    private static void AssertDictionaryValueKinds(JsonElement value, JsonValueKind expectedKind)
    {
        foreach (var property in value.EnumerateObject())
            Assert.Equal(expectedKind, property.Value.ValueKind);
    }

    private static void AssertStringDictionary(JsonElement value)
    {
        Assert.Equal(JsonValueKind.Object, value.ValueKind);

        foreach (var property in value.EnumerateObject())
            Assert.Equal(JsonValueKind.String, property.Value.ValueKind);
    }

    private static int GetCount(JsonElement value)
    {
        return value.ValueKind switch
        {
            JsonValueKind.Array => value.GetArrayLength(),
            JsonValueKind.Object => value.EnumerateObject().Count(),
            _ => 0
        };
    }

    private static JsonValueKind ParseKind(string? value)
    {
        return value switch
        {
            "array" => JsonValueKind.Array,
            "number" => JsonValueKind.Number,
            "object" => JsonValueKind.Object,
            "string" => JsonValueKind.String,
            _ => throw new InvalidDataException($"Unknown JSON kind '{value}'.")
        };
    }

    private static void AssertKind(string? expected, JsonValueKind actual)
    {
        if (expected == "boolean")
        {
            Assert.True(actual is JsonValueKind.True or JsonValueKind.False);
            return;
        }

        Assert.Equal(ParseKind(expected), actual);
    }
}
