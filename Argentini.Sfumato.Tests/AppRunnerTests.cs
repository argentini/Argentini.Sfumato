namespace Argentini.Sfumato.Tests;

public class AppRunnerTests
{
    [Fact]
    public async Task ProcessScannedFileUtilityClassDependencies()
    {
        var appRunner = new AppRunner(new AppState());
        var scannedFile = new ScannedFile("../../../SampleWebsite/wwwroot/index.html");

        await scannedFile.LoadAndScanFileAsync(appRunner);
        
        Assert.Equal(92, scannedFile.UtilityClasses.Count);
    }

    [Fact]
    public async Task ProcessAtApplyStatementsAndTrackDependencies()
    {
        var appRunner = new AppRunner(new AppState(), "../../../SampleCss/sample.css", true);

        await appRunner.LoadCssFileAsync();

        var sb = new StringBuilder();

        sb.AppendProcessedSourceCss(appRunner);
        sb.ProcessAtApplyStatementsAndTrackDependencies(appRunner);

        Assert.False(sb.Contains("@apply"));
        
        Assert.True(sb.Contains("tab-size: 4;"));
        Assert.True(sb.Contains("font-size: var(--text-base);"));
        Assert.True(sb.Contains("line-height: calc(var(--spacing) * 6) !important;"));
    }

    [Fact]
    public void CssImportStatements()
    {
        var appRunner = new AppRunner(new AppState(), "../../../SampleCss/sample.css")
        {
            AppRunnerSettings =
            {
                CssFilePath = "../../../SampleCss/sample.css"
            }
        };

        appRunner.AppRunnerSettings.LoadAndExtractCssContent();
        appRunner.AppRunnerSettings.ImportPartials();

        var indexOfPartialTestClass = appRunner.AppRunnerSettings.ProcessedCssContent.IndexOf(".partial-test", StringComparison.Ordinal);
        var indexOfPartial2TestClass = appRunner.AppRunnerSettings.ProcessedCssContent.IndexOf(".partial2-test", StringComparison.Ordinal);
        var indexOfPartial3TestClass = appRunner.AppRunnerSettings.ProcessedCssContent.IndexOf(".partial3-test", StringComparison.Ordinal);
        var indexOfPartial4TestClass = appRunner.AppRunnerSettings.ProcessedCssContent.IndexOf(".partial4-test", StringComparison.Ordinal);
        
        Assert.False(appRunner.AppRunnerSettings.ProcessedCssContent.Contains("@import", StringComparison.Ordinal));
        
        Assert.True(indexOfPartialTestClass > 0);
        Assert.True(indexOfPartial2TestClass > 0);
        Assert.True(indexOfPartial3TestClass > 0);
        Assert.Equal(0, indexOfPartial4TestClass);
        Assert.True(indexOfPartial2TestClass > indexOfPartial3TestClass);
    }

    [Fact]
    public void ProcessSfumatoBlock()
    {
        var appRunner = new AppRunner(new AppState(), "../../../SampleCss/sample.css")
        {
            AppRunnerSettings =
            {
                CssFilePath = "../../../SampleCss/sample.css"
            }
        };

        appRunner.AppRunnerSettings.LoadAndExtractCssContent();
        appRunner.AppRunnerSettings.ExtractSfumatoItems();
        appRunner.AppRunnerSettings.ProcessProjectSettings();

        Assert.True(appRunner.AppRunnerSettings.SfumatoBlockItems.ContainsKey("--spacing"));
        Assert.True(appRunner.AppRunnerSettings.SfumatoBlockItems.ContainsKey("--paths"));
        Assert.True(appRunner.AppRunnerSettings.SfumatoBlockItems.ContainsKey("@utility tab-4"));
        Assert.True(appRunner.AppRunnerSettings.SfumatoBlockItems.ContainsKey("@custom-variant phablet"));
        
        Assert.Equal("0.25rem", appRunner.AppRunnerSettings.SfumatoBlockItems["--spacing"]);
        Assert.Single(appRunner.AppRunnerSettings.Paths);
        Assert.True(appRunner.AppRunnerSettings.UseForms);
        Assert.True(appRunner.AppRunnerSettings.UseReset);
        Assert.False(appRunner.AppRunnerSettings.UseMinify);
    }

    [Fact]
    public async Task BuildCssFile()
    {
        var appRunner = new AppRunner(new AppState(), "../../../SampleCss/sample.css");

        await appRunner.LoadCssFileAsync();

        appRunner.ScannedFiles.TryAdd("test", new ScannedFile("")
        {
            UtilityClasses = ContentScanner.ScanFileForUtilityClasses(CssClassTests.Markup, appRunner)
        });

        var css = appRunner.BuildCss();
        
        var indexOfRoot = css.IndexOf(":root", StringComparison.Ordinal);
        var indexOfFontSansClass = css.IndexOf(".font-sans", StringComparison.Ordinal);
        var indexOfTestClass = css.IndexOf(".test", StringComparison.Ordinal);
        
        Assert.Equal(0, indexOfRoot);
        Assert.True(indexOfFontSansClass > 0);
        Assert.True(indexOfTestClass > 0);
    }
}
