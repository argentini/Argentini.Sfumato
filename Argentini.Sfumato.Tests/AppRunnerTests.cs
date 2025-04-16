// ReSharper disable ConvertToPrimaryConstructor

using Argentini.Sfumato.Entities.Scanning;
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
    
    [Fact]
    public async Task BuildCssFile()
    {
        var appRunner = new AppRunner(new AppState(), "../../../sample.css", true);

        await appRunner.LoadCssFileAsync();

        appRunner.ScannedFiles.Add("test", new ScannedFile("")
        {
            UtilityClasses = ContentScanner.ScanFileForUtilityClasses(CssClassTests.Markup, appRunner)
        });

        var css = appRunner.BuildCss();
        
        var indexOfRoot = css.IndexOf(":root {", StringComparison.Ordinal);
        var indexOfFontSansClass = css.IndexOf(".font-sans {", StringComparison.Ordinal);
        var indexOfPartialTestClass = css.IndexOf(".partial-test {", StringComparison.Ordinal);
        var indexOfPartial2TestClass = css.IndexOf(".partial2-test {", StringComparison.Ordinal);
        var indexOfPartial3TestClass = css.IndexOf(".partial3-test {", StringComparison.Ordinal);
        var indexOfPartial4TestClass = css.IndexOf(".partial4-test {", StringComparison.Ordinal);
        var indexOfTestClass = css.IndexOf(".test {", StringComparison.Ordinal);
        
        Assert.Equal(0, indexOfRoot);
        Assert.True(indexOfFontSansClass > 0);
        Assert.True(indexOfTestClass > 0);
        Assert.True(indexOfPartialTestClass > 0);
        Assert.True(indexOfPartial2TestClass > 0);
        Assert.True(indexOfPartial3TestClass > 0);
        Assert.True(indexOfPartial4TestClass > 0);

        Assert.True(indexOfPartial4TestClass > indexOfFontSansClass);
        Assert.True(indexOfPartial2TestClass > indexOfPartial3TestClass);
        Assert.True(indexOfPartial3TestClass > indexOfPartial4TestClass);
        Assert.True(indexOfTestClass > indexOfPartial3TestClass);
    }
}
