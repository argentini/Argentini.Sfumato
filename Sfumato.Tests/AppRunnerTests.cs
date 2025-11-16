namespace Sfumato.Tests;

public class AppRunnerTests : SharedTestBase
{
    public AppRunnerTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        AppRunner = new AppRunner(StringBuilderPool, SampleCssFilePath);
    }
    
    [Fact]
    public async Task ProcessScannedFileUtilityClassDependencies()
    {
        Assert.True(File.Exists(ScanSampleFilePath));
      
        AppRunner = new AppRunner(StringBuilderPool, ScanSampleFilePath);
        
        var scannedFile = new ScannedFile(ScanSampleFilePath);

        await scannedFile.LoadAndScanFileAsync(AppRunner);
        
        Assert.Equal(248, scannedFile.UtilityClasses.Count);
    }

    [Fact]
    public async Task ProcessAtApplyStatementsAndTrackDependencies()
    {
        Assert.True(File.Exists(SampleCssFilePath));
        
        await AppRunner.LoadCssFileAsync();
        await AppRunner.PerformFileScanAsync();

        AppRunner.ProcessScannedFileUtilityClassDependencies(AppRunner);

        var sb = new StringBuilder(await AppRunnerExtensions.FullBuildCssAsync(AppRunner));

        Assert.False(sb.Contains("@apply"));
        
        Assert.True(sb.Contains("tab-size: 4;"));
        Assert.True(sb.Contains("font-size: var(--text-base);"));
        Assert.True(sb.Contains("--sf-leading: calc(var(--spacing) * 6) !important;"));
        
        Assert.False(sb.Contains("@variant"));
        Assert.True(sb.Contains("@media (width >= 475px) {"));
    }

    [Fact]
    public async Task CssImportStatements()
    {
        Assert.True(File.Exists(SampleCssFilePath));

        await AppRunner.LoadCssFileAsync();

        AppRunner.AppRunnerSettings.CssContent.LoadSfumatoSettings(AppRunner);

        var sb = new StringBuilder(AppRunner.AppRunnerSettings.CssContent);
        var (index, length) = sb.ExtractCssImportStatements(AppRunner, true);
        
        var indexOfPartialTestClass = AppRunner.ImportsCssSegment.Content.IndexOf(".partial-test");
        var indexOfPartial2TestClass = AppRunner.ImportsCssSegment.Content.IndexOf(".partial2-test");
        var indexOfPartial3TestClass = AppRunner.ImportsCssSegment.Content.IndexOf(".partial3-test");
        var indexOfPartial4TestClass = AppRunner.ImportsCssSegment.Content.IndexOf(".partial4-test");
        
        Assert.False(AppRunner.ImportsCssSegment.Content.Contains("@import"));

        Assert.Equal(0, index);
        Assert.Equal(22, length);

        Assert.True(indexOfPartialTestClass > 0);
        Assert.True(indexOfPartial2TestClass > 0);
        Assert.True(indexOfPartial3TestClass > 0);

        Assert.Equal(0, indexOfPartial4TestClass);

        Assert.True(indexOfPartial3TestClass > indexOfPartial4TestClass);
        Assert.True(indexOfPartial2TestClass > indexOfPartial3TestClass);
        Assert.True(indexOfPartialTestClass > indexOfPartial2TestClass);

        Assert.Equal(4, AppRunner.AppRunnerSettings.CssImports.Count);

        AppRunner.AppRunnerSettings.CssImports.TryAdd("test", new CssImportFile
        {
            CssContent = new StringBuilder(),
            FileInfo = new FileInfo("test.css"),
            Pool = new StringBuilderPool(),
            Touched = true
        });
        
        Assert.Equal(5, AppRunner.AppRunnerSettings.CssImports.Count);

        _ = sb.ExtractCssImportStatements(AppRunner, true);

        Assert.Equal(4, AppRunner.AppRunnerSettings.CssImports.Count);
    }

    [Fact]
    public async Task ProcessSfumatoBlock()
    {
        Assert.True(File.Exists(SampleCssFilePath));

        await AppRunner.LoadCssFileAsync();

        AppRunner.AppRunnerSettings.CssContent.LoadSfumatoSettings(AppRunner);

        Assert.True(AppRunner.AppRunnerSettings.SfumatoBlockItems.ContainsKey("--spacing"));
        Assert.True(AppRunner.AppRunnerSettings.SfumatoBlockItems.ContainsKey("--paths"));
        Assert.True(AppRunner.AppRunnerSettings.SfumatoBlockItems.ContainsKey("@utility tab-4"));
        Assert.True(AppRunner.AppRunnerSettings.SfumatoBlockItems.ContainsKey("@custom-variant phablet"));
        
        Assert.Equal("0.25rem", AppRunner.AppRunnerSettings.SfumatoBlockItems["--spacing"]);
        Assert.Single(AppRunner.AppRunnerSettings.Paths);
        Assert.True(AppRunner.AppRunnerSettings.UseForms);
        Assert.True(AppRunner.AppRunnerSettings.UseReset);
        Assert.False(AppRunner.AppRunnerSettings.UseMinify);
    }

    [Fact]
    public async Task BuildCssFile()
    {
        Assert.True(File.Exists(SampleCssFilePath));

        await AppRunner.LoadCssFileAsync();

        AppRunner.AppRunnerSettings.CssContent.LoadSfumatoSettings(AppRunner);

        AppRunner.ScannedFiles.TryAdd("test", new ScannedFile("")
        {
            UtilityClasses = ContentScanner.ScanFileForUtilityClasses(CssClassTests.Markup, AppRunner)
        });

        AppRunner.ProcessScannedFileUtilityClassDependencies(AppRunner);
        
        var css = await AppRunnerExtensions.FullBuildCssAsync(AppRunner);
        
        var indexOfRoot = css.IndexOf(":root", StringComparison.Ordinal);
        var indexOfFontSansClass = css.IndexOf(".font-sans", StringComparison.Ordinal);
        var indexOfTestClass = css.IndexOf(".test", StringComparison.Ordinal);
        
        Assert.Equal(81, indexOfRoot);
        Assert.True(indexOfFontSansClass > 0);
        Assert.True(indexOfTestClass > 0);
    }
}
