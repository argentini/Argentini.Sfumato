using BenchmarkDotNet.Attributes;
using Microsoft.DotNet.PlatformAbstractions;

namespace Argentini.Sfumato.Tests.Benchmarks;

public class CssGenerationBenchmarks
{
    private AppRunner AppRunner { get; set; } = new (new AppState());
    /*
    private StringBuilder Sb { get; set; } = new ();
    private StringBuilder WorkingSb { get; set; } = new ();
    private StringBuilder Step2Prep {get;} = new ();
    private StringBuilder Step3Prep {get;} = new ();
    private StringBuilder Step4Prep {get;} = new ();
    private StringBuilder Step5Prep {get;} = new ();
    private StringBuilder Step6Prep {get;} = new ();
    private StringBuilder Step7Prep {get;} = new ();
    private StringBuilder Step8Prep {get;} = new ();
    private StringBuilder Step9Prep {get;} = new ();
    private StringBuilder Step10Prep {get;} = new ();
    private StringBuilder Step11Prep {get;} = new ();
    private StringBuilder Step12Prep {get;} = new ();
    */
    
    /*
    [IterationSetup]
    public void IterationSetup()
    {
        Sb.Clear();
        WorkingSb.Clear();
        Step2Prep.Clear();
        Step3Prep.Clear();
        Step4Prep.Clear();
        Step5Prep.Clear();
        Step2Prep.Clear();
        Step6Prep.Clear();
        Step7Prep.Clear();
        Step8Prep.Clear();
        Step9Prep.Clear();
        Step10Prep.Clear();
        Step11Prep.Clear();
        Step12Prep.Clear();

        var cssPath = Path.GetFullPath(Path.Combine(ApplicationEnvironment.ApplicationBasePath[..ApplicationEnvironment.ApplicationBasePath.IndexOf("Developer", StringComparison.Ordinal)], "Developer/Fynydd-Website-2024/UmbracoCms/wwwroot/stylesheets/source.css"));

        AppRunner = new AppRunner(new AppState(), cssPath);

        AppRunner.LoadCssFileAsync().GetAwaiter().GetResult();
        AppRunner.PerformFileScanAsync().GetAwaiter().GetResult();

        AppRunner.ProcessScannedFileUtilityClassDependencies(AppRunner);
        
        Step2Prep
            .AppendResetCss(AppRunner);
        
        Step3Prep
            .AppendResetCss(AppRunner)
            .AppendFormsCss(AppRunner);

        Step4Prep
            .AppendResetCss(AppRunner)
            .AppendFormsCss(AppRunner)
            .AppendUtilityClassMarker(AppRunner);

        Step5Prep
            .AppendResetCss(AppRunner)
            .AppendFormsCss(AppRunner)
            .AppendUtilityClassMarker(AppRunner)
            .AppendProcessedSourceCss(AppRunner);

        Step6Prep
            .AppendResetCss(AppRunner)
            .AppendFormsCss(AppRunner)
            .AppendUtilityClassMarker(AppRunner)
            .AppendProcessedSourceCss(AppRunner)
            .ProcessAtApplyStatementsAndTrackDependencies(AppRunner);

        Step7Prep
            .AppendResetCss(AppRunner)
            .AppendFormsCss(AppRunner)
            .AppendUtilityClassMarker(AppRunner)
            .AppendProcessedSourceCss(AppRunner)
            .ProcessAtApplyStatementsAndTrackDependencies(AppRunner)
            .InjectUtilityClassesCss(AppRunner);

        Step8Prep
            .AppendResetCss(AppRunner)
            .AppendFormsCss(AppRunner)
            .AppendUtilityClassMarker(AppRunner)
            .AppendProcessedSourceCss(AppRunner)
            .ProcessAtApplyStatementsAndTrackDependencies(AppRunner)
            .InjectUtilityClassesCss(AppRunner)
            .ProcessAtVariants(AppRunner);
        
        Step9Prep
            .AppendResetCss(AppRunner)
            .AppendFormsCss(AppRunner)
            .AppendUtilityClassMarker(AppRunner)
            .AppendProcessedSourceCss(AppRunner)
            .ProcessAtApplyStatementsAndTrackDependencies(AppRunner)
            .InjectUtilityClassesCss(AppRunner)
            .ProcessAtVariants(AppRunner)
            .ProcessFunctionsAndTrackDependencies(AppRunner);

        Step10Prep
            .AppendResetCss(AppRunner)
            .AppendFormsCss(AppRunner)
            .AppendUtilityClassMarker(AppRunner)
            .AppendProcessedSourceCss(AppRunner)
            .ProcessAtApplyStatementsAndTrackDependencies(AppRunner)
            .InjectUtilityClassesCss(AppRunner)
            .ProcessAtVariants(AppRunner)
            .ProcessFunctionsAndTrackDependencies(AppRunner)
            .InjectRootDependenciesCss(AppRunner);
        
        Step11Prep
            .AppendResetCss(AppRunner)
            .AppendFormsCss(AppRunner)
            .AppendUtilityClassMarker(AppRunner)
            .AppendProcessedSourceCss(AppRunner)
            .ProcessAtApplyStatementsAndTrackDependencies(AppRunner)
            .InjectUtilityClassesCss(AppRunner)
            .ProcessAtVariants(AppRunner)
            .ProcessFunctionsAndTrackDependencies(AppRunner)
            .InjectRootDependenciesCss(AppRunner)
            .MoveComponentsLayer(AppRunner);
        
        Step12Prep
            .AppendResetCss(AppRunner)
            .AppendFormsCss(AppRunner)
            .AppendUtilityClassMarker(AppRunner)
            .AppendProcessedSourceCss(AppRunner)
            .ProcessAtApplyStatementsAndTrackDependencies(AppRunner)
            .InjectUtilityClassesCss(AppRunner)
            .ProcessAtVariants(AppRunner)
            .ProcessFunctionsAndTrackDependencies(AppRunner)
            .InjectRootDependenciesCss(AppRunner)
            .MoveComponentsLayer(AppRunner)
            .ProcessDarkThemeClasses(AppRunner);
    }
    */

    [IterationSetup]
    public void IterationSetup()
    {
        var cssPath = Path.GetFullPath(Path.Combine(ApplicationEnvironment.ApplicationBasePath[..ApplicationEnvironment.ApplicationBasePath.IndexOf("Developer", StringComparison.Ordinal)], "Developer/Fynydd-Website-2024/UmbracoCms/wwwroot/stylesheets/source.css"));

        AppRunner = new AppRunner(new AppState(), cssPath);

        AppRunner.LoadCssFileAsync().GetAwaiter().GetResult();
        AppRunner.PerformFileScanAsync().GetAwaiter().GetResult();
    }

    [Benchmark(Baseline = true)]
    public void FullBuild()
    {
        var css = AppRunner.AppRunnerSettings.CssContent.BuildCss(AppRunner);
    }
    
    /*
    [Benchmark(Baseline = true)]
    public void FullProcess()
    {
        var workingSb = new StringBuilder();
        var sb = new StringBuilder()
            .AppendResetCss(AppRunner)
            .AppendFormsCss(AppRunner)
            .AppendUtilityClassMarker(AppRunner)

            .AppendProcessedSourceCss(AppRunner)

            .ProcessAtApplyStatementsAndTrackDependencies(AppRunner)

            .InjectUtilityClassesCss(AppRunner)
            .ProcessAtVariants(AppRunner)

            .ProcessFunctionsAndTrackDependencies(AppRunner)
            .InjectRootDependenciesCss(AppRunner)
            .MoveComponentsLayer(AppRunner);

        if (AppRunner.AppRunnerSettings.UseDarkThemeClasses)
            sb.ProcessDarkThemeClasses(AppRunner);

        _ = AppRunner.AppRunnerSettings.UseMinify ? sb.ToString().CompactCss(workingSb) : sb.ReformatCss(workingSb).ToString().NormalizeLinebreaks(AppRunner.AppRunnerSettings.LineBreak);
    }

    [Benchmark]
    public void Step1_AppendResetCss()
    {
        Sb.AppendResetCss(AppRunner);
    }

    [Benchmark]
    public void Step2_AppendFormsCss()
    {
        Step2Prep.AppendFormsCss(AppRunner);
    }

    [Benchmark]
    public void Step3_AppendUtilityClassMarker()
    {
        Step3Prep.AppendUtilityClassMarker(AppRunner);
    }

    [Benchmark]
    public void Step4_AppendProcessedSourceCss()
    {
        Step4Prep.AppendProcessedSourceCss(AppRunner);
    }

    [Benchmark]
    public void Step5_ProcessAtApplyStatementsAndTrackDependencies()
    {
        Step5Prep.ProcessAtApplyStatementsAndTrackDependencies(AppRunner);
    }

    [Benchmark]
    public void Step6_InjectUtilityClassesCss()
    {
        Step6Prep.InjectUtilityClassesCss(AppRunner);
    }

    [Benchmark]
    public void Step7_ProcessAtVariantStatements()
    {
        Step7Prep.ProcessAtVariants(AppRunner);
    }

    [Benchmark]
    public void Step8_ProcessFunctionsAndTrackDependencies()
    {
        Step8Prep.ProcessFunctionsAndTrackDependencies(AppRunner);
    }

    [Benchmark]
    public void Step9_InjectRootDependenciesCss()
    {
        Step9Prep.InjectRootDependenciesCss(AppRunner);
    }

    [Benchmark]
    public void Step10_MoveComponentsLayer()
    {
        Step10Prep.MoveComponentsLayer(AppRunner);
    }

    [Benchmark]
    public void Step11_ProcessDarkThemeClasses()
    {
        Step11Prep.ProcessDarkThemeClasses(AppRunner);
    }

    [Benchmark]
    public void Step12_Generate()
    {
        _ = Step12Prep.ReformatCss(WorkingSb).ToString().NormalizeLinebreaks(AppRunner.AppRunnerSettings.LineBreak);
    }
*/
}