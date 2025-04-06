// ReSharper disable ConvertToPrimaryConstructor

using Xunit.Abstractions;

namespace Argentini.Sfumato.Tests;

public class AppRunnerTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public AppRunnerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task SfumatoBlockParsing()
    {
        var appRunner = new AppRunner(new AppState(), "../../../sample.css", true);
        
        await appRunner.LoadCssSettingsAsync();
        
        Assert.Equal(6, appRunner.AppRunnerSettings.SfumatoBlockItems.Count);
    }
}
