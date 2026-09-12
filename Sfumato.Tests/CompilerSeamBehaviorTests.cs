using System.Text.Json;
using Sfumato.Entities.UtilityClasses;

namespace Sfumato.Tests;

public sealed class CompilerSeamBehaviorTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    [Fact]
    public void CapabilityPoliciesPreserveLegacyCapabilityViews()
    {
        var definition = new ClassDefinition
        {
            InColorCollection = true,
            InLengthCollection = true,
            UsesSlashModifier = true,
            ModifierIsOpacity = true
        };

        Assert.Equal(UtilityValueKinds.Color | UtilityValueKinds.Length, definition.ValueKinds);
        Assert.Equal(UtilityBehaviors.SlashModifier | UtilityBehaviors.OpacityModifier, definition.Behaviors);
        Assert.True(definition.Accepts(UtilityValueKinds.Color));
        Assert.True(definition.Accepts(UtilityValueKinds.Length));
        Assert.False(definition.Accepts(UtilityValueKinds.Integer));
        Assert.True(definition.InColorCollection);
        Assert.True(definition.InLengthCollection);
        Assert.True(definition.UsesSlashModifier);
        Assert.True(definition.ModifierIsOpacity);
        Assert.False(definition.DisallowsFractions);
    }

    [Fact]
    public void DocumentationClonePreservesPoliciesWithoutSharingMutableState()
    {
        var definition = new ClassDefinition
        {
            ValueKinds = UtilityValueKinds.Color | UtilityValueKinds.Abstract,
            Behaviors = UtilityBehaviors.SlashModifier | UtilityBehaviors.RazorSyntax,
            Template = "color: {0};",
            ModifierTemplate = "color: {0} / {1};",
            SelectorSort = 42,
            DocDefinitions = new Dictionary<string, string> { ["text-<color>"] = "color: <color>;" },
            DocExamples = new Dictionary<string, string> { ["text-red-500"] = "color: red;" }
        };

        var clone = definition.CloneForDocumentation();

        Assert.NotSame(definition, clone);
        Assert.Equal(definition.ValueKinds, clone.ValueKinds);
        Assert.Equal(definition.Behaviors, clone.Behaviors);
        Assert.Equal(definition.Template, clone.Template);
        Assert.Equal(definition.ModifierTemplate, clone.ModifierTemplate);
        Assert.Equal(definition.SelectorSort, clone.SelectorSort);
        Assert.Equal(definition.DocDefinitions, clone.DocDefinitions);
        Assert.Equal(definition.DocExamples, clone.DocExamples);
        Assert.NotSame(definition.DocDefinitions, clone.DocDefinitions);
        Assert.NotSame(definition.DocExamples, clone.DocExamples);

        clone.DocDefinitions["added"] = "added";
        clone.DocExamples.Clear();

        Assert.DoesNotContain("added", definition.DocDefinitions);
        Assert.Single(definition.DocExamples);
    }

    [Fact]
    public void BuiltInRegistryContainsEveryConcreteUtilityDictionary()
    {
        var expectedTypes = typeof(ClassDictionaryBase).Assembly
            .GetTypes()
            .Where(type => typeof(ClassDictionaryBase).IsAssignableFrom(type) && type is { IsClass: true, IsAbstract: false })
            .OrderBy(type => type.FullName, StringComparer.Ordinal)
            .ToArray();
        var registeredTypes = BuiltInUtilityRegistry.All
            .Select(item => item.GetType())
            .OrderBy(type => type.FullName, StringComparer.Ordinal)
            .ToArray();

        Assert.Equal(expectedTypes, registeredTypes);
        Assert.Equal(registeredTypes.Length, registeredTypes.Distinct().Count());
    }

    [Fact]
    public void BuiltInRegistryFeedsEveryCompilerValueIndex()
    {
        var library = new Sfumato.Entities.Library.Library();
        var indexedDefinitions = 0;

        foreach (var utility in BuiltInUtilityRegistry.All)
        {
            foreach (var (prefix, definition) in utility.Data)
            {
                if (prefix.EndsWith('(') || prefix.EndsWith('['))
                    continue;

                Assert.True(library.ScannerClassNamePrefixes.ContainsKey(prefix), prefix);

                if (definition.Accepts(UtilityValueKinds.Simple))
                    Assert.True(library.SimpleClasses.ContainsKey(prefix), prefix);

                foreach (var valueKind in Enum.GetValues<UtilityValueKinds>())
                {
                    if (valueKind is UtilityValueKinds.None or UtilityValueKinds.Simple || definition.Accepts(valueKind) == false)
                        continue;

                    Assert.True(library.TryGetClassDefinition(prefix, valueKind, out var resolved), $"{prefix}: {valueKind}");
                    Assert.NotNull(resolved);
                    indexedDefinitions++;
                }
            }
        }

        Assert.True(indexedDefinitions > 0);
    }

    [Fact]
    public void MinimalLibraryOmitsUtilitiesButRetainsVariantSyntax()
    {
        var library = new Sfumato.Entities.Library.Library(false);

        Assert.Equal(0, library.ScannerClassNamePrefixes.Count);
        Assert.Equal(0, library.SimpleClasses.Count);
        Assert.Equal(0, library.AbstractClasses.Count);
        Assert.Equal(0, library.ColorClasses.Count);
        Assert.Equal(0, library.LengthClasses.Count);
        Assert.True(library.CssPropertyNamesWithColons.Count > 0);
        Assert.True(library.PseudoclassPrefixes.Count > 0);
        Assert.True(library.MediaQueryPrefixes.Count > 0);
    }

    [Fact]
    public void DefaultRunnerRetainsBuiltInUtilityDefinitions()
    {
        var appRunner = new AppRunner(new StringBuilderPool());

        Assert.True(appRunner.Library.ScannerClassNamePrefixes.Count > 0);
        Assert.True(appRunner.Library.SimpleClasses.ContainsKey("flex"));
        Assert.True(appRunner.Library.LengthClasses.ContainsKey("pbs-"));
    }

    [Fact]
    public void ClearingUtilityDefinitionsRemovesScannerAndValueIndexes()
    {
        var library = new Sfumato.Entities.Library.Library();

        library.ClearUtilityDefinitions();

        Assert.Equal(0, library.ScannerClassNamePrefixes.Count);
        Assert.Equal(0, library.SimpleClasses.Count);
        Assert.Equal(0, library.ColorClasses.Count);
        Assert.Equal(0, library.LengthClasses.Count);
        Assert.True(library.PseudoclassPrefixes.Count > 0);
    }

    [Fact]
    public void ConstructorAndExplicitPipelinePreserveEquivalentBehavior()
    {
        const string candidate = "dark:focus:border-bs-4";

        using var constructorResult = new CssClass(AppRunner, selector: candidate);
        using var explicitResult = new CssClass(AppRunner) { Selector = candidate };

        explicitResult.Initialize();

        Assert.True(constructorResult.IsValid);
        Assert.Equal(".dark\\:focus\\:border-bs-4:focus", constructorResult.EscapedSelector);
        Assert.Equal(
            "border-block-start-style: var(--sf-border-style);\nborder-block-start-width: 4px;",
            NormalizeNewlines(constructorResult.Styles));
        Assert.Equal(["@media (prefers-color-scheme: dark) {"], constructorResult.Wrappers.Values);
        Assert.Equal(Capture(constructorResult), Capture(explicitResult));
    }

    [Fact]
    public void InvalidCandidateStopsBeforeSelectorWrapperAndStyleEmission()
    {
        using var cssClass = new CssClass(AppRunner, selector: "dark:zoom-1.5");

        Assert.False(cssClass.IsValid);
        Assert.Empty(cssClass.EscapedSelector);
        Assert.Empty(cssClass.Styles);
        Assert.Empty(cssClass.Wrappers);
    }

    [Fact]
    public void EmptyCandidateIsRejectedWithoutThrowing()
    {
        using var cssClass = new CssClass(AppRunner, selector: string.Empty);

        Assert.False(cssClass.IsValid);
        Assert.Empty(cssClass.EscapedSelector);
        Assert.Empty(cssClass.Styles);
        Assert.Empty(cssClass.Wrappers);
    }

    [Fact]
    public void ThemeAndExtensionOverlaysRemainRunnerLocal()
    {
        var left = new AppRunner(new StringBuilderPool(), SampleCssFilePath);
        var right = new AppRunner(new StringBuilderPool(), SampleCssFilePath);
        const string css =
            """
            @layer sfumato {
                :root {
                    --paths: ["./"];
                    --output-path: "sfumato.css";
                    --spacing-seam: 17px;
                }

                @custom-variant seam-active (&:hover);

                @utility seam-* {
                    width: --value([length]);
                }
            }
            """;

        Assert.True(css.LoadSfumatoSettings(left));

        using var leftTheme = new CssClass(left, selector: "pbs-seam");
        using var rightTheme = new CssClass(right, selector: "pbs-seam");
        using var leftUtility = new CssClass(left, selector: "seam-[3px]");
        using var rightUtility = new CssClass(right, selector: "seam-[3px]");
        using var leftVariant = new CssClass(left, selector: "seam-active:flex");
        using var rightVariant = new CssClass(right, selector: "seam-active:flex");
        using var leftBuiltIn = new CssClass(left, selector: "zoom-50");
        using var rightBuiltIn = new CssClass(right, selector: "zoom-50");

        Assert.True(leftTheme.IsValid);
        Assert.Equal("padding-block-start: var(--spacing-seam);", leftTheme.Styles);
        Assert.False(rightTheme.IsValid);
        Assert.True(leftUtility.IsValid);
        Assert.Equal("width: 3px;", leftUtility.Styles);
        Assert.False(rightUtility.IsValid);
        Assert.True(leftVariant.IsValid);
        Assert.Equal(".seam-active\\:flex:hover", leftVariant.EscapedSelector);
        Assert.False(rightVariant.IsValid);
        Assert.Equal(Capture(leftBuiltIn), Capture(rightBuiltIn));
    }

    [Fact]
    public void RepeatedCompilationPreservesExactOutputs()
    {
        string[] candidates =
        [
            "flex",
            "border-bs",
            "aspect-8.5/11",
            "font-features-[\"smcp\"]",
            "@container-size/sidebar",
            "dark:focus:zoom-50",
            "scrollbar-thumb-red-500",
            "zoom-1.5"
        ];
        var expected = CompileAll(AppRunner, candidates);

        for (var iteration = 0; iteration < 32; iteration++)
            Assert.Equal(expected, CompileAll(AppRunner, candidates));
    }

    [Fact]
    public void UtilityEmissionPreservesOrderingAcrossInsertionOrders()
    {
        string[] candidates = ["focus:flex", "zoom-50", "dark:flex", "border-bs"];
        var forward = GenerateUtilities(candidates);
        var reverse = GenerateUtilities(candidates.Reverse());

        Assert.Equal(forward, reverse);
        Assert.Contains(".focus\\:flex:focus", forward, StringComparison.Ordinal);
        Assert.Contains(".zoom-50", forward, StringComparison.Ordinal);
        Assert.Contains("@media (prefers-color-scheme: dark) {", forward, StringComparison.Ordinal);
        Assert.Contains(".border-bs", forward, StringComparison.Ordinal);
    }

    private static string[] CompileAll(AppRunner appRunner, string[] candidates)
    {
        var output = new string[candidates.Length];

        for (var index = 0; index < candidates.Length; index++)
        {
            using var cssClass = new CssClass(appRunner, selector: candidates[index]);

            output[index] = Capture(cssClass);
        }

        return output;
    }

    private string GenerateUtilities(IEnumerable<string> candidates)
    {
        var appRunner = new AppRunner(new StringBuilderPool(), SampleCssFilePath);
        var cssClasses = new List<CssClass>();

        try
        {
            foreach (var candidate in candidates)
            {
                var cssClass = new CssClass(appRunner, selector: candidate);

                Assert.True(cssClass.IsValid, candidate);
                cssClasses.Add(cssClass);
                appRunner.UtilityClasses.Add(candidate, cssClass);
            }

            appRunner.GenerateUtilityClasses();

            return appRunner.UtilitiesCssSegment.Content.ToString();
        }
        finally
        {
            for (var index = 0; index < cssClasses.Count; index++)
                cssClasses[index].Dispose();
        }
    }

    private static string Capture(CssClass cssClass)
    {
        return JsonSerializer.Serialize(new
        {
            cssClass.IsValid,
            cssClass.IsImportant,
            cssClass.IsArbitraryCss,
            cssClass.IsCssCustomPropertyAssignment,
            cssClass.HasModifierValue,
            cssClass.HasArbitraryModifierValue,
            cssClass.HasArbitraryValue,
            cssClass.HasArbitraryValueWithCssCustomProperty,
            cssClass.UsesDarkTheme,
            cssClass.HasRazorSyntax,
            cssClass.EscapedSelector,
            Styles = NormalizeNewlines(cssClass.Styles),
            Segments = cssClass.AllSegments.ToArray(),
            Variants = cssClass.VariantSegments.Keys.ToArray(),
            Wrappers = cssClass.Wrappers.Values.ToArray(),
            cssClass.SelectorSort,
            cssClass.WrapperSort
        });
    }

    private static string NormalizeNewlines(string value)
    {
        return value.Replace("\r\n", "\n", StringComparison.Ordinal);
    }
}
