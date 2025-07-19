using System.Diagnostics;
using Microsoft.DotNet.PlatformAbstractions;

namespace Argentini.Sfumato.Tests.Benchmarks;

public class BenchmarkTests
{
    private AppRunner AppRunner { get; set; }
    private ITestOutputHelper? TestOutputHelper { get; }
    private bool IsDebug { get; set; }

    private const string BasicUtilityClass = "text-sm";
    private const string AverageUtilityClass = "tabp:hover:text-sm";
    private const string LargeUtilityClass = "dark:group-[.is-published]:[&.active]:[@supports(display:flex)]:tabp:max-desk:hover:text-[1rem]/6!";

    static readonly double NsPerTick = 1_000_000_000.0 / Stopwatch.Frequency;

    public BenchmarkTests(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;

#if DEBUG
        IsDebug = true;
#else
        IsDebug = false;
#endif

        AppRunner = new AppRunner(new AppState());
        
        TestOutputHelper?.WriteLine($"| {(IsDebug ? "Method (Debug)" : "Method (Release)"),-40} | Mean                | Measured       | Coverage       |");
        TestOutputHelper?.WriteLine("| ---------------------------------------- | -------------------:| --------------:| --------------:|");
    }

    private async ValueTask IterationSetup()
    {
        var basePath = ApplicationEnvironment.ApplicationBasePath;
        var root = basePath[..basePath.IndexOf("Developer", StringComparison.Ordinal)];

        AppRunner = new AppRunner(new AppState(), Path.GetFullPath(Path.Combine(root, "Developer/Sfumato-Web/UmbracoCms/wwwroot/stylesheets/source.css")));

        await Task.CompletedTask;
    }

    private async ValueTask BenchmarkMethod(string methodName, Func<Task> action)
    {
        await BenchmarkMethod(methodName, null, action);
    }

    private async ValueTask BenchmarkMethod(string methodName, Func<Task>? setupAction, Func<Task> action)
    {
        const int iterations = 1000;

        var log = new Dictionary<double, int>();
        var offsetTicks = 0d;

        for (var i = 0; i < iterations; i++)
        {
            var start = Stopwatch.GetTimestamp();
            var elapsedTicks = Stopwatch.GetTimestamp() - start;

            offsetTicks += elapsedTicks;
        }

        offsetTicks /= iterations;
        
        for (var i = 0; i < iterations; i++)
        {
            if (setupAction is not null)
                await setupAction();
            
            var start = Stopwatch.GetTimestamp();
            
            await action();
            
            var elapsedTicks = Stopwatch.GetTimestamp() - start;
            var nanoseconds  = (elapsedTicks - offsetTicks) * NsPerTick;

            if (log.TryAdd(nanoseconds, 1) == false)
                log[nanoseconds]++;
        }

        var sampleCut = log.Count > 4 ? 0.75d : log.Count > 2 ? log.Count * 0.667d : 1.0d;
        var windowCount = 0d;
        var weightedSumNs = 0d;
        var sampleCount = (int)(log.Count * sampleCut);

        foreach (var kvp in log.OrderByDescending(i => i.Value).ThenBy(i => i.Key).Take(sampleCount))
        {
            windowCount += kvp.Value;
            weightedSumNs += kvp.Key * kvp.Value;
        }
        
        var averageWindowNs = weightedSumNs / windowCount;

        TestOutputHelper?.WriteLine($"| {methodName,-40} | {averageWindowNs,16:N0} ns | {$"{sampleCount} / {log.Count}",14} | {$"{(windowCount == 0 ? 0 : 100 / (iterations / windowCount)):N1}",13}% |");
    }

    [Fact]
    public async Task UtilityClassCreationBenchmark()
    {
        await IterationSetup();

        await BenchmarkMethod("BasicUtilityClass",
            async () => {
                _ = new CssClass(AppRunner, BasicUtilityClass);
                await Task.CompletedTask;
            });

        await BenchmarkMethod("AverageUtilityClass",
            async () => {
                _ = new CssClass(AppRunner, AverageUtilityClass);
                await Task.CompletedTask;
            });

        await BenchmarkMethod("LargeUtilityClass",
            async () => {
                _ = new CssClass(AppRunner, LargeUtilityClass);
                await Task.CompletedTask;
            });
    }

    [Fact]
    public async Task IsLikelyUtilityClassBenchmark()
    {
        await IterationSetup();

        await BenchmarkMethod("IsLikelyUtilityClass_Basic", async () => {
                _ = BasicUtilityClass.IsLikelyUtilityClass(AppRunner.Library.ScannerClassNamePrefixes);
                await Task.CompletedTask;
            });

        await BenchmarkMethod("IsLikelyUtilityClass_Average", async () => {
                _ = AverageUtilityClass.IsLikelyUtilityClass(AppRunner.Library.ScannerClassNamePrefixes);
                await Task.CompletedTask;
            });

        await BenchmarkMethod("IsLikelyUtilityClass_Large", async () => {
                _ = LargeUtilityClass.IsLikelyUtilityClass(AppRunner.Library.ScannerClassNamePrefixes);
                await Task.CompletedTask;
            });
    }
    
    [Fact]
    public async Task CssClassFlowBenchmark()
    {
        var cssClass = new CssClass(AppRunner);

        await IterationSetup();

        await BenchmarkMethod("Full_Flow", async () => {
            _ = new CssClass(AppRunner, AverageUtilityClass);
            await Task.CompletedTask;
        });

        await BenchmarkMethod("Initialize", async () => {
            cssClass = new CssClass(AppRunner, AverageUtilityClass);
            cssClass.Initialize(false);
            await Task.CompletedTask;
        });

        await BenchmarkMethod("Step1_SplitSegments", async () =>
        {
            cssClass = new CssClass(AppRunner, AverageUtilityClass);
            cssClass.Initialize(false);
            await Task.CompletedTask;
            
        }, async () => {
            foreach (var segment in cssClass.Selector.SplitByTopLevel(':'))
                cssClass.AllSegments.Add(segment.ToString());
            await Task.CompletedTask;
        });

        await BenchmarkMethod("Step2_ProcessArbitraryCss", async () =>
        {
            cssClass = new CssClass(AppRunner, AverageUtilityClass);
            cssClass.Initialize(false);
            await Task.CompletedTask;

        }, async () => {
            cssClass.ProcessArbitraryCss();
            await Task.CompletedTask;
        });

        await BenchmarkMethod("Step3_ProcessUtilityClasses", async () =>
        {
            cssClass = new CssClass(AppRunner, AverageUtilityClass);
            cssClass.Initialize(false);
            cssClass.ProcessArbitraryCss();
            await Task.CompletedTask;
            
        }, async () => {
            cssClass.ProcessUtilityClasses();
            await Task.CompletedTask;
        });

        await BenchmarkMethod("Step4_ProcessVariants", async () =>
        {
            cssClass = new CssClass(AppRunner, AverageUtilityClass);
            cssClass.Initialize(false);
            cssClass.ProcessArbitraryCss();
            cssClass.ProcessUtilityClasses();
            await Task.CompletedTask;
            
        }, async () => {
            cssClass.ProcessVariants();
            await Task.CompletedTask;
        });

        await BenchmarkMethod("Step5_GenerateSelector", async () =>
        {
            cssClass = new CssClass(AppRunner, AverageUtilityClass);
            cssClass.Initialize(false);
            cssClass.ProcessArbitraryCss();
            cssClass.ProcessUtilityClasses();
            cssClass.ProcessVariants();
            await Task.CompletedTask;
            
        }, async () => {
            cssClass.GenerateSelector();
            await Task.CompletedTask;
        });

        await BenchmarkMethod("Step6_GenerateWrappers", async () =>
        {
            cssClass = new CssClass(AppRunner, AverageUtilityClass);
            cssClass.Initialize(false);
            cssClass.ProcessArbitraryCss();
            cssClass.ProcessUtilityClasses();
            cssClass.ProcessVariants();
            cssClass.GenerateSelector();
            await Task.CompletedTask;
            
        }, async () => {
            cssClass.GenerateWrappers();
            await Task.CompletedTask;
        });

        await BenchmarkMethod("GenerateStyles", async () =>
        {
            cssClass = new CssClass(AppRunner, AverageUtilityClass);
            cssClass.Initialize(false);
            cssClass.ProcessArbitraryCss();
            cssClass.ProcessUtilityClasses();
            cssClass.ProcessVariants();
            cssClass.GenerateSelector();
            cssClass.GenerateWrappers();
            await Task.CompletedTask;
            
        }, async () => {
            cssClass.GenerateStyles();
            await Task.CompletedTask;
        });

        await BenchmarkMethod("CssSelectorEscape", async () =>
        {
            cssClass = new CssClass(AppRunner, AverageUtilityClass);
            cssClass.Initialize(false);
            cssClass.ProcessArbitraryCss();
            cssClass.ProcessUtilityClasses();
            cssClass.ProcessVariants();
            cssClass.GenerateSelector();
            cssClass.GenerateWrappers();
            cssClass.GenerateStyles();
            await Task.CompletedTask;

        }, async () => {
            _ = AverageUtilityClass.CssSelectorEscape();
            await Task.CompletedTask;
        });
    }
    
    [Fact]
    public async Task CssGenerationBenchmark()
    {
        await BenchmarkMethod("FullBuild", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
        }, async () => {
            await AppRunnerExtensions.FullBuildCssAsync(AppRunner);
        });
        
        await BenchmarkMethod("RunSfumatoExtract", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
        }, async () =>
        {
            RunSfumatoExtract();
            await Task.CompletedTask;
        });
        
        await BenchmarkMethod("RunCssImports", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
        }, async () =>
        {
            RunCssImports();
            await Task.CompletedTask;
        });
        
        await BenchmarkMethod("ProcessAllComponentsLayersAndCss", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
        }, async () => {
            AppRunner.ProcessAllComponentsLayersAndCss();
            await Task.CompletedTask;
        });
        
        await BenchmarkMethod("GenerateUtilityClasses", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
            AppRunner.ProcessAllComponentsLayersAndCss();
        }, async () => {
            AppRunner.GenerateUtilityClasses();
            await Task.CompletedTask;
        });
        
        await BenchmarkMethod("RunAtApplyStatements", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
            AppRunner.ProcessAllComponentsLayersAndCss();
            AppRunner.GenerateUtilityClasses();
        }, async () => {
            await RunAtApplyStatements();
        });
        
        await BenchmarkMethod("RunFunctions", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
            AppRunner.ProcessAllComponentsLayersAndCss();
            AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
        }, async () => {
            await RunFunctions();
        });
        
        await BenchmarkMethod("RunAtVariantStatements", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
            AppRunner.ProcessAllComponentsLayersAndCss();
            AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
        }, async () => {
            await RunAtVariantStatements();
        });

        await BenchmarkMethod("RunProcessDarkThemeClasses", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
            AppRunner.ProcessAllComponentsLayersAndCss();
            AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
        }, async () => {
            await RunProcessDarkThemeClasses();
        });

        await BenchmarkMethod("RunGatherCssCustomPropRefs", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
            AppRunner.ProcessAllComponentsLayersAndCss();
            AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
            await RunProcessDarkThemeClasses();
        }, async () =>
        {
            RunGatherCssCustomPropRefs();
            await Task.CompletedTask;
        });
        
        await BenchmarkMethod("RunGatherUsedCssRefs", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
            AppRunner.ProcessAllComponentsLayersAndCss();
            AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
            await RunProcessDarkThemeClasses();
            RunGatherCssCustomPropRefs();
        }, async () => {
            await RunGatherUsedCssRefs();
        });
        
        await BenchmarkMethod("MergeUsedDependencies", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
            AppRunner.ProcessAllComponentsLayersAndCss();
            AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
            await RunProcessDarkThemeClasses();
            RunGatherCssCustomPropRefs();
            await RunGatherUsedCssRefs();
        }, async () => {
            AppRunner.MergeUsedDependencies();
            await Task.CompletedTask;
        });
        
        await BenchmarkMethod("GeneratePropertiesAndThemeLayers", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
            AppRunner.ProcessAllComponentsLayersAndCss();
            AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
            await RunProcessDarkThemeClasses();
            RunGatherCssCustomPropRefs();
            await RunGatherUsedCssRefs();
            AppRunner.MergeUsedDependencies();
        }, async () => {
            AppRunner.GeneratePropertiesAndThemeLayers();
            await Task.CompletedTask;
        });
        
        await BenchmarkMethod("RunFinalCssAssembly", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
            AppRunner.ProcessAllComponentsLayersAndCss();
            AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
            await RunProcessDarkThemeClasses();
            RunGatherCssCustomPropRefs();
            await RunGatherUsedCssRefs();
            AppRunner.MergeUsedDependencies();
            AppRunner.GeneratePropertiesAndThemeLayers();
        }, async () =>
        {
            AppRunner.FinalCssAssembly();
            await Task.CompletedTask;
        });
        
        await BenchmarkMethod("RunGenerateCss", async () => {
            await IterationSetup();
            await AppRunner.LoadCssFileAsync();
            await AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
            AppRunner.ProcessAllComponentsLayersAndCss();
            AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
            await RunProcessDarkThemeClasses();
            RunGatherCssCustomPropRefs();
            await RunGatherUsedCssRefs();
            AppRunner.MergeUsedDependencies();
            AppRunner.GeneratePropertiesAndThemeLayers();
            AppRunner.FinalCssAssembly();
        }, async () =>
        {
            AppRunner.GenerateFinalCss();
            await Task.CompletedTask;
        });
    }
    
    private void RunSfumatoExtract()
    {
        var (i, len) = AppRunner.CustomCssSegment.Content.ExtractSfumatoBlock(AppRunner);
        if (i > -1) AppRunner.CustomCssSegment.Content.Remove(i, len);
    }

    private void RunCssImports()
    {
	    var (i, len) = AppRunner.CustomCssSegment.Content.ExtractCssImportStatements(AppRunner, true);
	    if (i > -1) AppRunner.CustomCssSegment.Content.Remove(i, len);
    }

    private async ValueTask RunAtApplyStatements()
    {
	    await AppRunner.ProcessSegmentAtApplyStatementsAsync(AppRunner.ImportsCssSegment);
	    await AppRunner.ProcessSegmentAtApplyStatementsAsync(AppRunner.ComponentsCssSegment);
	    await AppRunner.ProcessSegmentAtApplyStatementsAsync(AppRunner.CustomCssSegment);
    }

    private async ValueTask RunFunctions()
    {
        await AppRunner.ProcessSegmentFunctionsAsync(AppRunner.BrowserResetCssSegment);
        await AppRunner.ProcessSegmentFunctionsAsync(AppRunner.FormsCssSegment);
        await AppRunner.ProcessSegmentFunctionsAsync(AppRunner.UtilitiesCssSegment);
        await AppRunner.ProcessSegmentFunctionsAsync(AppRunner.ImportsCssSegment);
        await AppRunner.ProcessSegmentFunctionsAsync(AppRunner.ComponentsCssSegment);
	    await AppRunner.ProcessSegmentFunctionsAsync(AppRunner.CustomCssSegment);
    }

    private async ValueTask RunAtVariantStatements()
    {
        await AppRunner.ProcessSegmentAtVariantStatementsAsync(AppRunner.UtilitiesCssSegment);
        await AppRunner.ProcessSegmentAtVariantStatementsAsync(AppRunner.ImportsCssSegment);
        await AppRunner.ProcessSegmentAtVariantStatementsAsync(AppRunner.ComponentsCssSegment);
        await AppRunner.ProcessSegmentAtVariantStatementsAsync(AppRunner.CustomCssSegment);
    }

    private async ValueTask RunProcessDarkThemeClasses()
    {
        if (AppRunner.AppRunnerSettings.UseDarkThemeClasses == false)
            return;

        foreach (var seg in new[]{ 
            AppRunner.UtilitiesCssSegment,
            AppRunner.ImportsCssSegment,
            AppRunner.ComponentsCssSegment,
            AppRunner.CustomCssSegment })
        {
            await AppRunner.ProcessDarkThemeClassesAsync(seg);
        }
    }

    private void RunGatherCssCustomPropRefs()
    {
	    AppRunner.PropertiesCssSegment.Content.Clear();
	    AppRunner.PropertyListCssSegment.Content.Clear();
	    AppRunner.ThemeCssSegment.Content.Clear();

	    AppRunner.GatherSegmentCssCustomPropertyRefs(AppRunner.BrowserResetCssSegment);
	    AppRunner.GatherSegmentCssCustomPropertyRefs(AppRunner.FormsCssSegment);
	    AppRunner.GatherSegmentCssCustomPropertyRefs(AppRunner.UtilitiesCssSegment);
	    AppRunner.GatherSegmentCssCustomPropertyRefs(AppRunner.ImportsCssSegment);
	    AppRunner.GatherSegmentCssCustomPropertyRefs(AppRunner.ComponentsCssSegment);
	    AppRunner.GatherSegmentCssCustomPropertyRefs(AppRunner.CustomCssSegment);
    }
    
    private async ValueTask RunGatherUsedCssRefs()
    {
	    await AppRunner.GatherSegmentUsedCssRefsAsync(AppRunner.BrowserResetCssSegment);
        await AppRunner.GatherSegmentUsedCssRefsAsync(AppRunner.FormsCssSegment);
        await AppRunner.GatherSegmentUsedCssRefsAsync(AppRunner.UtilitiesCssSegment);
        await AppRunner.GatherSegmentUsedCssRefsAsync(AppRunner.ImportsCssSegment);
        await AppRunner.GatherSegmentUsedCssRefsAsync(AppRunner.ComponentsCssSegment);
        await AppRunner.GatherSegmentUsedCssRefsAsync(AppRunner.CustomCssSegment);
    }
}
