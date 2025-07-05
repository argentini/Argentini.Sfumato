using BenchmarkDotNet.Attributes;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.ObjectPool;

namespace Argentini.Sfumato.Tests;

public class AppRunnerTests
{
    [Fact]
    public async Task ProcessScannedFileUtilityClassDependencies()
    {
        var appRunner = new AppRunner(new AppState());
        var scannedFile = new ScannedFile("../../../SampleWebsite/wwwroot/index.html");

        await scannedFile.LoadAndScanFileAsync(appRunner);
        
        Assert.Equal(248, scannedFile.UtilityClasses.Count);
    }

    [Fact]
    public async Task ProcessAtApplyStatementsAndTrackDependencies()
    {
        var appRunner = new AppRunner(new AppState(), "../../../SampleCss/sample.css");

        await appRunner.LoadCssFileAsync();
        await appRunner.PerformFileScanAsync();
        appRunner.ProcessScannedFileUtilityClassDependencies(appRunner);

        var sb = new StringBuilder(appRunner.AppRunnerSettings.CssContent.BuildCss(appRunner));

        Assert.False(sb.Contains("@apply"));
        
        Assert.True(sb.Contains("tab-size: 4;"));
        Assert.True(sb.Contains("font-size: var(--text-base);"));
        Assert.True(sb.Contains("--sf-leading: calc(var(--spacing) * 6) !important;"));
        
        Assert.False(sb.Contains("@variant"));
        Assert.True(sb.Contains("@media (width >= 475px) {"));
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

        appRunner.AppRunnerSettings.LoadCssAndExtractSfumatoBlock();

        var sb = new StringBuilder(appRunner.AppRunnerSettings.CssContent);
        var (index, length) = sb.ProcessCssImportStatements(appRunner, true);
        
        var indexOfPartialTestClass = appRunner.ImportsCssSegment.IndexOf(".partial-test");
        var indexOfPartial2TestClass = appRunner.ImportsCssSegment.IndexOf(".partial2-test");
        var indexOfPartial3TestClass = appRunner.ImportsCssSegment.IndexOf(".partial3-test");
        var indexOfPartial4TestClass = appRunner.ImportsCssSegment.IndexOf(".partial4-test");
        
        Assert.False(appRunner.ImportsCssSegment.Contains("@import"));

        Assert.Equal(0, index);
        Assert.Equal(22, length);

        Assert.True(indexOfPartialTestClass > 0);
        Assert.True(indexOfPartial2TestClass > 0);
        Assert.True(indexOfPartial3TestClass > 0);

        Assert.Equal(0, indexOfPartial4TestClass);

        Assert.True(indexOfPartial3TestClass > indexOfPartial4TestClass);
        Assert.True(indexOfPartial2TestClass > indexOfPartial3TestClass);
        Assert.True(indexOfPartialTestClass > indexOfPartial2TestClass);

        Assert.Equal(4, appRunner.AppRunnerSettings.CssImports.Count);

        appRunner.AppRunnerSettings.CssImports.TryAdd("test", new CssImportFile
        {
            CssContent = new StringBuilder(),
            FileInfo = new FileInfo("test.css"),
            Pool = new DefaultObjectPoolProvider().CreateStringBuilderPool(),
            Touched = true
        });
        
        Assert.Equal(5, appRunner.AppRunnerSettings.CssImports.Count);

        _ = sb.ProcessCssImportStatements(appRunner, true);

        Assert.Equal(4, appRunner.AppRunnerSettings.CssImports.Count);
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

        appRunner.AppRunnerSettings.LoadCssAndExtractSfumatoBlock();
        appRunner.AppRunnerSettings.ExtractSfumatoItems();
        AppRunnerExtensions.ProcessSfumatoBlockSettings(appRunner);

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
        /*
        var cssPath = Path.GetFullPath(Path.Combine(ApplicationEnvironment.ApplicationBasePath[..ApplicationEnvironment.ApplicationBasePath.IndexOf("Developer", StringComparison.Ordinal)], "Developer/Fynydd-Website-2024/UmbracoCms/wwwroot/stylesheets/source.css"));

        var appRunner = new AppRunner(new AppState(), cssPath);

        await appRunner.LoadCssFileAsync();
        await appRunner.PerformFileScanAsync();

        var css = appRunner.AppRunnerSettings.CssContent.BuildCss(appRunner);
        */
        
        var appRunner = new AppRunner(new AppState(), "../../../SampleCss/sample.css");

        await appRunner.LoadCssFileAsync();

        appRunner.ScannedFiles.TryAdd("test", new ScannedFile("")
        {
            UtilityClasses = ContentScanner.ScanFileForUtilityClasses(CssClassTests.Markup, appRunner)
        });

        appRunner.ProcessScannedFileUtilityClassDependencies(appRunner);
        
        var css = appRunner.AppRunnerSettings.CssContent.BuildCss(appRunner);
        
        var indexOfRoot = css.IndexOf(":root", StringComparison.Ordinal);
        var indexOfFontSansClass = css.IndexOf(".font-sans", StringComparison.Ordinal);
        var indexOfTestClass = css.IndexOf(".test", StringComparison.Ordinal);
        
        Assert.Equal(81, indexOfRoot);
        Assert.True(indexOfFontSansClass > 0);
        Assert.True(indexOfTestClass > 0);
    }
}
