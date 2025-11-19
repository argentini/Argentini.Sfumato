namespace Sfumato.Tests.Benchmarks;

public class CssGenerationBenchmarkTests(ITestOutputHelper testOutputHelper) : SharedTestBase(testOutputHelper)
{
    private SharedBenchmarkCode Shared { get; } = new(testOutputHelper);
    
    [Fact]
    public async Task CssGenerationBenchmark()
    {
        await  Shared.BenchmarkMethod("FullBuild", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
        }, async () => {
            await  AppRunnerExtensions.FullBuildCssAsync( Shared.AppRunner);
        });
        
        await  Shared.BenchmarkMethod("RunSfumatoExtract", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
        }, async () =>
        {
            RunSfumatoExtract();
            await Task.CompletedTask;
        });
        
        await  Shared.BenchmarkMethod("RunCssImports", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
        }, async () =>
        {
            RunCssImports();
            await Task.CompletedTask;
        });
        
        await  Shared.BenchmarkMethod("ProcessAllComponentsLayersAndCss", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
        }, async () => {
             Shared.AppRunner.ProcessAllComponentsLayersAndCss();
            await Task.CompletedTask;
        });
        
        await  Shared.BenchmarkMethod("GenerateUtilityClasses", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
             Shared.AppRunner.ProcessAllComponentsLayersAndCss();
        }, async () => {
             Shared.AppRunner.GenerateUtilityClasses();
            await Task.CompletedTask;
        });
        
        await  Shared.BenchmarkMethod("RunAtApplyStatements", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
             Shared.AppRunner.ProcessAllComponentsLayersAndCss();
             Shared.AppRunner.GenerateUtilityClasses();
        }, async () => {
            await RunAtApplyStatements();
        });
        
        await  Shared.BenchmarkMethod("RunFunctions", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
             Shared.AppRunner.ProcessAllComponentsLayersAndCss();
             Shared.AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
        }, async () => {
            await RunFunctions();
        });
        
        await  Shared.BenchmarkMethod("RunAtVariantStatements", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
             Shared.AppRunner.ProcessAllComponentsLayersAndCss();
             Shared.AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
        }, async () => {
            await RunAtVariantStatements();
        });

        await  Shared.BenchmarkMethod("RunProcessDarkThemeClasses", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
             Shared.AppRunner.ProcessAllComponentsLayersAndCss();
             Shared.AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
        }, async () => {
            await RunProcessDarkThemeClasses();
        });

        await  Shared.BenchmarkMethod("RunGatherCssCustomPropRefs", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
             Shared.AppRunner.ProcessAllComponentsLayersAndCss();
             Shared.AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
            await RunProcessDarkThemeClasses();
        }, async () =>
        {
            RunGatherCssCustomPropRefs();
            await Task.CompletedTask;
        });
        
        await  Shared.BenchmarkMethod("RunGatherUsedCssRefs", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
             Shared.AppRunner.ProcessAllComponentsLayersAndCss();
             Shared.AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
            await RunProcessDarkThemeClasses();
            RunGatherCssCustomPropRefs();
        }, async () => {
            await RunGatherUsedCssRefs();
        });
        
        await  Shared.BenchmarkMethod("MergeUsedDependencies", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
             Shared.AppRunner.ProcessAllComponentsLayersAndCss();
             Shared.AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
            await RunProcessDarkThemeClasses();
            RunGatherCssCustomPropRefs();
            await RunGatherUsedCssRefs();
        }, async () => {
             Shared.AppRunner.MergeUsedDependencies();
            await Task.CompletedTask;
        });
        
        await  Shared.BenchmarkMethod("GeneratePropertiesAndThemeLayers", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
             Shared.AppRunner.ProcessAllComponentsLayersAndCss();
             Shared.AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
            await RunProcessDarkThemeClasses();
            RunGatherCssCustomPropRefs();
            await RunGatherUsedCssRefs();
             Shared.AppRunner.MergeUsedDependencies();
        }, async () => {
             Shared.AppRunner.GeneratePropertiesAndThemeLayers();
            await Task.CompletedTask;
        });
        
        await  Shared.BenchmarkMethod("RunFinalCssAssembly", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
             Shared.AppRunner.ProcessAllComponentsLayersAndCss();
             Shared.AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
            await RunProcessDarkThemeClasses();
            RunGatherCssCustomPropRefs();
            await RunGatherUsedCssRefs();
             Shared.AppRunner.MergeUsedDependencies();
             Shared.AppRunner.GeneratePropertiesAndThemeLayers();
        }, async () =>
        {
             Shared.AppRunner.FinalCssAssembly();
            await Task.CompletedTask;
        });
        
        await  Shared.BenchmarkMethod("RunGenerateCss", async () => {
            await  Shared.IterationSetup();
            await  Shared.AppRunner.LoadCssFileAsync();
            await  Shared.AppRunner.PerformFileScanAsync();
            RunSfumatoExtract();
            RunCssImports();
             Shared.AppRunner.ProcessAllComponentsLayersAndCss();
             Shared.AppRunner.GenerateUtilityClasses();
            await RunAtApplyStatements();
            await RunFunctions();
            await RunAtVariantStatements();
            await RunProcessDarkThemeClasses();
            RunGatherCssCustomPropRefs();
            await RunGatherUsedCssRefs();
             Shared.AppRunner.MergeUsedDependencies();
             Shared.AppRunner.GeneratePropertiesAndThemeLayers();
             Shared.AppRunner.FinalCssAssembly();
        }, async () =>
        {
             Shared.AppRunner.GenerateFinalCss();
            await Task.CompletedTask;
        });

        Shared.OutputTotalTime();
    }
    
    private void RunSfumatoExtract()
    {
        var (i, len) =  Shared.AppRunner.CustomCssSegment.Content.ExtractSfumatoBlock( Shared.AppRunner);
        if (i > -1)  Shared.AppRunner.CustomCssSegment.Content.Remove(i, len);
    }

    private void RunCssImports()
    {
	    var (i, len) =  Shared.AppRunner.CustomCssSegment.Content.ExtractCssImportStatements( Shared.AppRunner, true);
	    if (i > -1)  Shared.AppRunner.CustomCssSegment.Content.Remove(i, len);
    }

    private async ValueTask RunAtApplyStatements()
    {
	    await  Shared.AppRunner.ProcessSegmentAtApplyStatementsAsync( Shared.AppRunner.ImportsCssSegment);
	    await  Shared.AppRunner.ProcessSegmentAtApplyStatementsAsync( Shared.AppRunner.ComponentsCssSegment);
	    await  Shared.AppRunner.ProcessSegmentAtApplyStatementsAsync( Shared.AppRunner.CustomCssSegment);
    }

    private async ValueTask RunFunctions()
    {
        await  Shared.AppRunner.ProcessSegmentFunctionsAsync(Shared.AppRunner.BrowserResetCssSegment);
        await  Shared.AppRunner.ProcessSegmentFunctionsAsync(Shared.AppRunner.FormsCssSegment);
        await  Shared.AppRunner.ProcessSegmentFunctionsAsync(Shared.AppRunner.UtilitiesCssSegment);
        await  Shared.AppRunner.ProcessSegmentFunctionsAsync(Shared.AppRunner.ImportsCssSegment);
        await  Shared.AppRunner.ProcessSegmentFunctionsAsync(Shared.AppRunner.ComponentsCssSegment);
	    await  Shared.AppRunner.ProcessSegmentFunctionsAsync(Shared.AppRunner.CustomCssSegment);
    }

    private async ValueTask RunAtVariantStatements()
    {
        await  Shared.AppRunner.ProcessSegmentAtVariantStatementsAsync( Shared.AppRunner.UtilitiesCssSegment);
        await  Shared.AppRunner.ProcessSegmentAtVariantStatementsAsync( Shared.AppRunner.ImportsCssSegment);
        await  Shared.AppRunner.ProcessSegmentAtVariantStatementsAsync( Shared.AppRunner.ComponentsCssSegment);
        await  Shared.AppRunner.ProcessSegmentAtVariantStatementsAsync( Shared.AppRunner.CustomCssSegment);
    }

    private async ValueTask RunProcessDarkThemeClasses()
    {
        if ( Shared.AppRunner.AppRunnerSettings.UseDarkThemeClasses == false)
            return;

        foreach (var seg in new[]{ 
             Shared.AppRunner.UtilitiesCssSegment,
             Shared.AppRunner.ImportsCssSegment,
             Shared.AppRunner.ComponentsCssSegment,
             Shared.AppRunner.CustomCssSegment })
        {
            await  Shared.AppRunner.ProcessDarkThemeClassesAsync(seg);
        }
    }

    private void RunGatherCssCustomPropRefs()
    {
	     Shared.AppRunner.PropertiesCssSegment.Content.Clear();
	     Shared.AppRunner.PropertyListCssSegment.Content.Clear();
	     Shared.AppRunner.ThemeCssSegment.Content.Clear();

	     Shared.AppRunner.GatherSegmentCssCustomPropertyRefs( Shared.AppRunner.BrowserResetCssSegment);
	     Shared.AppRunner.GatherSegmentCssCustomPropertyRefs( Shared.AppRunner.FormsCssSegment);
	     Shared.AppRunner.GatherSegmentCssCustomPropertyRefs( Shared.AppRunner.UtilitiesCssSegment);
	     Shared.AppRunner.GatherSegmentCssCustomPropertyRefs( Shared.AppRunner.ImportsCssSegment);
	     Shared.AppRunner.GatherSegmentCssCustomPropertyRefs( Shared.AppRunner.ComponentsCssSegment);
	     Shared.AppRunner.GatherSegmentCssCustomPropertyRefs( Shared.AppRunner.CustomCssSegment);
    }
    
    private async ValueTask RunGatherUsedCssRefs()
    {
	    await  Shared.AppRunner.GatherSegmentUsedCssRefsAsync( Shared.AppRunner.BrowserResetCssSegment);
        await  Shared.AppRunner.GatherSegmentUsedCssRefsAsync( Shared.AppRunner.FormsCssSegment);
        await  Shared.AppRunner.GatherSegmentUsedCssRefsAsync( Shared.AppRunner.UtilitiesCssSegment);
        await  Shared.AppRunner.GatherSegmentUsedCssRefsAsync( Shared.AppRunner.ImportsCssSegment);
        await  Shared.AppRunner.GatherSegmentUsedCssRefsAsync( Shared.AppRunner.ComponentsCssSegment);
        await  Shared.AppRunner.GatherSegmentUsedCssRefsAsync( Shared.AppRunner.CustomCssSegment);
    }
}
