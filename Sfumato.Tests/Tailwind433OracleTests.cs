using System.Text.Json;
using System.Security.Cryptography;

namespace Sfumato.Tests;

public sealed class Tailwind433OracleTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    private const string ExpectedVersion = "4.3.3";
    private const string ExpectedCommit = "c2b24dd15fed1c59dd521bd86082f520c9f5ad0d";

    [Fact]
    public void OracleMetadataPinsOfficialTailwind433Checkout()
    {
        var fixture = LoadFixture();

        Assert.Equal(ExpectedVersion, fixture.TailwindVersion);
        Assert.Equal(ExpectedCommit, fixture.TailwindCommit);
        Assert.Equal(["line-endings", "custom-property-prefix"], fixture.Normalizations);
        Assert.NotEmpty(fixture.Cases);
        Assert.Equal(fixture.Cases.Count, fixture.Cases.Select(item => item.Candidate).Distinct(StringComparer.Ordinal).Count());

        for (var index = 0; index < fixture.Cases.Count; index++)
        {
            var oracleCase = fixture.Cases[index];

            Assert.False(Path.IsPathRooted(oracleCase.SourceFile), oracleCase.Candidate);
            Assert.StartsWith("packages/tailwindcss/src/", oracleCase.SourceFile, StringComparison.Ordinal);
            Assert.NotEmpty(oracleCase.SourceTest);
        }
    }

    [Fact]
    public void CandidatesMatchPinnedTailwind433Oracle()
    {
        var fixture = LoadFixture();

        for (var index = 0; index < fixture.Cases.Count; index++)
        {
            var oracleCase = fixture.Cases[index];

            using var cssClass = new CssClass(AppRunner, selector: oracleCase.Candidate);

            Assert.Equal(oracleCase.Accepted, cssClass.IsValid);
            Assert.Equal(oracleCase.Selector, Normalize(cssClass.EscapedSelector));
            Assert.Equal(oracleCase.Declarations, GetDeclarations(cssClass.Styles));
            Assert.Equal(oracleCase.Wrappers, cssClass.Wrappers.Values.Select(Normalize).ToArray());

            if (oracleCase.DivergenceIds.Length == 0)
                Assert.Equal(oracleCase.OfficialDeclarations.Select(Normalize), oracleCase.Declarations.Select(Normalize));
        }
    }

    [Fact]
    public void RawOracleFilesMatchPinnedOfficialOutput()
    {
        var fixture = LoadFixture();
        var inputPath = GetFixturePath(fixture.InputFile);
        var outputPath = GetFixturePath(fixture.OutputFile);
        var output = File.ReadAllText(outputPath);

        Assert.Equal(fixture.InputSha256, GetSha256(inputPath));
        Assert.Equal(fixture.OutputSha256, GetSha256(outputPath));
        Assert.StartsWith("/*! tailwindcss v4.3.3", output, StringComparison.Ordinal);
        Assert.Contains(".\\@container-size\\/sidebar", output, StringComparison.Ordinal);
        Assert.DoesNotContain(".zoom-1\\.5", output, StringComparison.Ordinal);
    }

    [Fact]
    public void StructuralCasesMatchPinnedOfficialOutput()
    {
        var fixture = LoadFixture();
        var output = File.ReadAllText(GetFixturePath(fixture.OutputFile)).Replace("\r\n", "\n", StringComparison.Ordinal);

        for (var index = 0; index < fixture.Cases.Count; index++)
        {
            var oracleCase = fixture.Cases[index];
            var selector = oracleCase.Accepted ? oracleCase.Selector : EscapeCandidate(oracleCase.Candidate);

            if (oracleCase.Accepted == false)
            {
                Assert.DoesNotContain($"{selector} {{", output, StringComparison.Ordinal);
                continue;
            }

            Assert.Equal(oracleCase.OfficialDeclarations, GetOfficialDeclarations(output, selector));

            var selectorPosition = output.IndexOf($"{selector} {{", StringComparison.Ordinal);

            for (var wrapperIndex = 0; wrapperIndex < oracleCase.Wrappers.Length; wrapperIndex++)
                Assert.True(output.LastIndexOf(oracleCase.Wrappers[wrapperIndex], selectorPosition, StringComparison.Ordinal) >= 0);
        }

        var previousPosition = -1;

        for (var index = 0; index < fixture.OfficialOrder.Length; index++)
        {
            var oracleCase = fixture.Cases.Single(item => item.Candidate == fixture.OfficialOrder[index]);
            var position = output.IndexOf($"{oracleCase.Selector} {{", StringComparison.Ordinal);

            Assert.True(position > previousPosition, fixture.OfficialOrder[index]);
            previousPosition = position;
        }
    }

    [Fact]
    public void IntentionalDivergencesAreDeclaredAndReferenced()
    {
        var fixture = LoadFixture();
        using var document = JsonDocument.Parse(File.ReadAllText(GetFixturePath(fixture.DivergenceFile)));
        var divergences = document.RootElement.GetProperty("divergences").EnumerateArray().ToArray();
        var identifiers = divergences
            .Select(item => item.GetProperty("id").GetString())
            .OfType<string>()
            .ToHashSet(StringComparer.Ordinal);
        var referenced = fixture.Cases
            .SelectMany(item => item.DivergenceIds)
            .Concat(fixture.DivergenceIds)
            .ToHashSet(StringComparer.Ordinal);

        Assert.Equal(ExpectedVersion, document.RootElement.GetProperty("tailwindVersion").GetString());
        Assert.Equal(divergences.Length, identifiers.Count);
        Assert.Contains("custom-property-prefix", identifiers);

        foreach (var divergence in divergences)
        {
            var identifier = Assert.IsType<string>(divergence.GetProperty("id").GetString());

            Assert.NotEmpty(divergence.GetProperty("scope").GetString() ?? string.Empty);
            Assert.NotEmpty(divergence.GetProperty("reason").GetString() ?? string.Empty);

            if (divergence.GetProperty("compatibility").GetString() != "additive" && identifier != "custom-property-prefix")
                Assert.Contains(identifier, referenced);
        }
    }

    [Fact]
    public void GeneratedUtilityOrderingIsPinnedAgainstOfficialOracle()
    {
        var fixture = LoadFixture();
        var classes = new List<CssClass>(fixture.OfficialOrder.Length);

        try
        {
            for (var index = fixture.OfficialOrder.Length - 1; index >= 0; index--)
            {
                var candidate = fixture.OfficialOrder[index];
                var cssClass = new CssClass(AppRunner, selector: candidate);

                Assert.True(cssClass.IsValid, candidate);
                classes.Add(cssClass);
                AppRunner.UtilityClasses.Add(candidate, cssClass);
            }

            AppRunner.GenerateUtilityClasses();

            var output = AppRunner.UtilitiesCssSegment.Content.ToString();
            var generatedOrder = classes
                .OrderBy(item => output.IndexOf(item.EscapedSelector, StringComparison.Ordinal))
                .Select(item => item.Selector)
                .ToArray();

            Assert.Equal(fixture.SfumatoOrder, generatedOrder);
            Assert.NotEqual(fixture.OfficialOrder, generatedOrder);
            Assert.Contains("deterministic-utility-order", fixture.DivergenceIds);
        }
        finally
        {
            for (var index = 0; index < classes.Count; index++)
                classes[index].Dispose();
        }
    }

    private static OracleFixture LoadFixture()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Fixtures", "Tailwind", ExpectedVersion, "oracle.json");
        var json = File.ReadAllText(path);
        var fixture = JsonSerializer.Deserialize<OracleFixture>(json, JsonOptions);

        return Assert.IsType<OracleFixture>(fixture);
    }

    private static string GetFixturePath(string fileName)
    {
        return Path.Combine(AppContext.BaseDirectory, "Fixtures", "Tailwind", ExpectedVersion, fileName);
    }

    private static string GetSha256(string path)
    {
        return Convert.ToHexStringLower(SHA256.HashData(File.ReadAllBytes(path)));
    }

    private static string[] GetDeclarations(string styles)
    {
        if (string.IsNullOrEmpty(styles))
            return [];

        return Normalize(styles)
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    private static string[] GetOfficialDeclarations(string output, string selector)
    {
        var selectorPosition = output.IndexOf($"{selector} {{", StringComparison.Ordinal);

        Assert.True(selectorPosition >= 0, selector);

        var openBrace = output.IndexOf('{', selectorPosition);
        var closeBrace = output.IndexOf('}', openBrace + 1);

        Assert.True(closeBrace > openBrace, selector);

        return output[(openBrace + 1)..closeBrace]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    private static string EscapeCandidate(string candidate)
    {
        return $".{candidate
            .Replace("@", "\\@", StringComparison.Ordinal)
            .Replace(".", "\\.", StringComparison.Ordinal)
            .Replace("/", "\\/", StringComparison.Ordinal)
            .Replace(":", "\\:", StringComparison.Ordinal)
            .Replace("[", "\\[", StringComparison.Ordinal)
            .Replace("]", "\\]", StringComparison.Ordinal)
            .Replace("\"", "\\\"", StringComparison.Ordinal)}";
    }

    private static string Normalize(string value)
    {
        return value
            .Replace("\r\n", "\n", StringComparison.Ordinal)
            .Replace("--sf-", "--tw-", StringComparison.Ordinal);
    }

    private static JsonSerializerOptions JsonOptions { get; } = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private sealed class OracleFixture
    {
        public string TailwindVersion { get; init; } = string.Empty;
        public string TailwindCommit { get; init; } = string.Empty;
        public string InputFile { get; init; } = string.Empty;
        public string InputSha256 { get; init; } = string.Empty;
        public string OutputFile { get; init; } = string.Empty;
        public string OutputSha256 { get; init; } = string.Empty;
        public string DivergenceFile { get; init; } = string.Empty;
        public string[] Normalizations { get; init; } = [];
        public string[] DivergenceIds { get; init; } = [];
        public string[] OfficialOrder { get; init; } = [];
        public string[] SfumatoOrder { get; init; } = [];
        public List<OracleCase> Cases { get; init; } = [];
    }

    private sealed class OracleCase
    {
        public string Candidate { get; init; } = string.Empty;
        public bool Accepted { get; init; }
        public string Selector { get; init; } = string.Empty;
        public string[] OfficialDeclarations { get; init; } = [];
        public string[] Declarations { get; init; } = [];
        public string[] Wrappers { get; init; } = [];
        public string SourceFile { get; init; } = string.Empty;
        public string SourceTest { get; init; } = string.Empty;
        public string[] DivergenceIds { get; init; } = [];
    }
}
