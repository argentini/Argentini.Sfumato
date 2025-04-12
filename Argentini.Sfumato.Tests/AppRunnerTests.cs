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

        await appRunner.LoadCssFileAsync();
        
        Assert.Equal(6, appRunner.AppRunnerSettings.SfumatoBlockItems.Count);
        Assert.True(appRunner.AppRunnerSettings.ProcessedCssContent.Contains(".partial-test", StringComparison.Ordinal));
        Assert.True(appRunner.AppRunnerSettings.ProcessedCssContent.Contains(".partial2-test", StringComparison.Ordinal));
        Assert.True(appRunner.AppRunnerSettings.ProcessedCssContent.Contains(".partial3-test", StringComparison.Ordinal));
        Assert.True(appRunner.AppRunnerSettings.ProcessedCssContent.Contains(".partial4-test", StringComparison.Ordinal));
        Assert.False(appRunner.AppRunnerSettings.ProcessedCssContent.Contains("@import", StringComparison.Ordinal));
    }
}
