using System.Reflection;
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

        runner.AppState.UsedClasses.Clear();
        runner.AppState.UsedClasses.Add(new ScssClass
        {
            ClassName = "dark:tabp:bg-fuchsia-200",
            FilePath = Path.Combine(Assembly.GetExecutingAssembly().Location[..Assembly.GetExecutingAssembly().Location.LastIndexOf(Path.DirectorySeparatorChar)], "scss", "bg-color.scss")
        });
        
        var scssClass = runner.AppState.UsedClasses.FirstOrDefault(c => c.ClassName == "dark:tabp:bg-fuchsia-200");

        Assert.NotNull(scssClass);
        
        var result = await runner.GenerateScssAsync();
        
        Assert.Equal("""
@media (prefers-color-scheme: dark) {
    @include sf-media($from: tabp) {
        .dark\:tabp\:bg-fuchsia-200 {
            background-color: rgb(245 208 254);
        }
    }
}
""", result.Trim());
    }
}
