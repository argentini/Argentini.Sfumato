using Xunit.Abstractions;

namespace Argentini.Sfumato.Tests;

public class ParsingTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ParsingTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task ParsingTest()
    {
        _testOutputHelper.WriteLine("ParsingTest");
        
        var runner = new SfumatoRunner(Array.Empty<string>());

        await runner.InitializeAsync();
        await runner.GatherUsedClassesAsync();

        var scssPath = runner.UsedClasses.FirstOrDefault()?.FilePath ?? string.Empty;

        scssPath = scssPath[..scssPath.LastIndexOf(Path.DirectorySeparatorChar)];
        
        runner.UsedClasses.Clear();
        runner.UsedClasses.Add(new ScssClass
        {
            ClassName = "dark:tabp:bg-fuchsia-200",
            FilePath = Path.Combine(scssPath, "bg-color.scss")
        });
        
        var scssClass = runner.UsedClasses.FirstOrDefault(c => c.ClassName == "dark:tabp:bg-fuchsia-200");

        Assert.NotNull(scssClass);
        
        var result = await runner.GenerateScssAsync();
        
        Assert.Equal(@"@media (prefers-color-scheme: dark) {
    @include sf-media($from: tabp) {
        .dark\:tabp\:bg-fuchsia-200 {
            background-color: rgb(245 208 254);
        }
    }
}", result.Trim());
    }
}
