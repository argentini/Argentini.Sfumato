// ReSharper disable InvokeAsExtensionMethod

using BenchmarkDotNet.Attributes;
using Microsoft.DotNet.PlatformAbstractions;

namespace Argentini.Sfumato.Tests.Benchmarks
{
    public enum CssPipelineStage
    {
        FullBuild,
        SfumatoExtract,
        CssImports,
        ComponentsLayer,
        UtilityClasses,
        AtApplyStatements,
        Functions,
        AtVariantStatements,
        ProcessDarkThemeClasses,
        GatherCssCustomPropRefs,
        GatherUsedCssRefs,
        MergeUsedDependencies,
        GeneratePropsAndThemeLayers,
        FinalCssAssembly,
        GenerateCss
    }

    [MemoryDiagnoser]
    public class CssGenerationBenchmarks
    {
        [Params(
            CssPipelineStage.FullBuild,
            CssPipelineStage.SfumatoExtract,
            CssPipelineStage.CssImports,
            CssPipelineStage.ComponentsLayer,
            CssPipelineStage.UtilityClasses,
            CssPipelineStage.AtApplyStatements,
            CssPipelineStage.Functions,
            CssPipelineStage.AtVariantStatements,
            CssPipelineStage.ProcessDarkThemeClasses,
            CssPipelineStage.GatherCssCustomPropRefs,
            CssPipelineStage.GatherUsedCssRefs,
            CssPipelineStage.MergeUsedDependencies,
            CssPipelineStage.GeneratePropsAndThemeLayers,
            CssPipelineStage.FinalCssAssembly,
            CssPipelineStage.GenerateCss)]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public CssPipelineStage Stage { get; set; }

        // ReSharper disable once InconsistentNaming
        private AppRunner appRunner = null!;
        private string _cssPath = null!;

        [IterationSetup]
        public void IterationSetup()
        {
	        var basePath = ApplicationEnvironment.ApplicationBasePath;
	        var root = basePath[..basePath.IndexOf("Developer", StringComparison.Ordinal)];

	        _cssPath = Path.GetFullPath(Path.Combine(root, "Developer/Fynydd-Website-2024/UmbracoCms/wwwroot/stylesheets/source.css"));

	        appRunner = new AppRunner(new AppState(), _cssPath);

	        appRunner.LoadCssFileAsync().GetAwaiter().GetResult();
	        appRunner.PerformFileScanAsync().GetAwaiter().GetResult();
	        
            // 1) reset content
            appRunner.CustomCssSegment.Content.ReplaceContent(appRunner.AppRunnerSettings.CssContent);

            // 2) run every stage *before* the one we want to measure
            switch (Stage)
            {
                case CssPipelineStage.FullBuild:
                case CssPipelineStage.SfumatoExtract:
                    // nothing to pre-run
                    break;
                case CssPipelineStage.CssImports:
	                RunSfumatoExtract();
	                break;

                case CssPipelineStage.ComponentsLayer:
	                RunSfumatoExtract();
                    RunCssImports();
                    break;

                case CssPipelineStage.UtilityClasses:
	                RunSfumatoExtract();
	                RunCssImports();
	                appRunner.ProcessAllComponentsLayersAndCss();
                    break;

                case CssPipelineStage.AtApplyStatements:
	                RunSfumatoExtract();
	                RunCssImports();
	                appRunner.ProcessAllComponentsLayersAndCss();
	                appRunner.GenerateUtilityClasses();
                    break;

                case CssPipelineStage.Functions:
	                RunSfumatoExtract();
	                RunCssImports();
	                appRunner.ProcessAllComponentsLayersAndCss();
	                appRunner.GenerateUtilityClasses();
                    RunAtApplyStatements();
                    break;

                case CssPipelineStage.AtVariantStatements:
	                RunSfumatoExtract();
	                RunCssImports();
	                appRunner.ProcessAllComponentsLayersAndCss();
	                appRunner.GenerateUtilityClasses();
                    RunAtApplyStatements();
                    RunFunctions();
                    break;
                
                case CssPipelineStage.ProcessDarkThemeClasses:
	                RunSfumatoExtract();
	                RunCssImports();
	                appRunner.ProcessAllComponentsLayersAndCss();
	                appRunner.GenerateUtilityClasses();
	                RunAtApplyStatements();
	                RunFunctions();
	                RunAtVariantStatements();
	                break;
              
                case CssPipelineStage.GatherCssCustomPropRefs:
	                RunSfumatoExtract();
	                RunCssImports();
	                appRunner.ProcessAllComponentsLayersAndCss();
	                appRunner.GenerateUtilityClasses();
	                RunAtApplyStatements();
	                RunFunctions();
	                RunAtVariantStatements();
	                RunProcessDarkThemeClasses();
	                break;
                
                case CssPipelineStage.GatherUsedCssRefs:
	                RunSfumatoExtract();
	                RunCssImports();
	                appRunner.ProcessAllComponentsLayersAndCss();
	                appRunner.GenerateUtilityClasses();
	                RunAtApplyStatements();
	                RunFunctions();
	                RunAtVariantStatements();
	                RunProcessDarkThemeClasses();
	                RunGatherCssCustomPropRefs();
	                break;
                
                case CssPipelineStage.MergeUsedDependencies:
	                RunSfumatoExtract();
	                RunCssImports();
	                appRunner.ProcessAllComponentsLayersAndCss();
	                appRunner.GenerateUtilityClasses();
	                RunAtApplyStatements();
	                RunFunctions();
	                RunAtVariantStatements();
	                RunProcessDarkThemeClasses();
	                RunGatherCssCustomPropRefs();
	                RunGatherUsedCssRefs();
	                break;

                case CssPipelineStage.GeneratePropsAndThemeLayers:
	                RunSfumatoExtract();
	                RunCssImports();
	                appRunner.ProcessAllComponentsLayersAndCss();
	                appRunner.GenerateUtilityClasses();
                    RunAtApplyStatements();
                    RunFunctions();
                    RunAtVariantStatements();
	                RunProcessDarkThemeClasses();
	                RunGatherCssCustomPropRefs();
	                RunGatherUsedCssRefs();
	                appRunner.MergeUsedDependencies();
                    break;

                case CssPipelineStage.FinalCssAssembly:
	                RunSfumatoExtract();
	                RunCssImports();
                    appRunner.ProcessAllComponentsLayersAndCss();
                    appRunner.GenerateUtilityClasses();
                    RunAtApplyStatements();
                    RunFunctions();
                    RunAtVariantStatements();
	                RunProcessDarkThemeClasses();
	                RunGatherCssCustomPropRefs();
	                RunGatherUsedCssRefs();
	                appRunner.MergeUsedDependencies();
                    appRunner.GeneratePropertiesAndThemeLayers();
                    break;
                
                case CssPipelineStage.GenerateCss:
	                RunSfumatoExtract();
	                RunCssImports();
	                appRunner.ProcessAllComponentsLayersAndCss();
	                appRunner.GenerateUtilityClasses();
	                RunAtApplyStatements();
	                RunFunctions();
	                RunAtVariantStatements();
	                RunProcessDarkThemeClasses();
	                RunGatherCssCustomPropRefs();
	                RunGatherUsedCssRefs();
	                appRunner.MergeUsedDependencies();
	                appRunner.GeneratePropertiesAndThemeLayers();
	                appRunner.FinalCssAssembly();
	                break;
            }
        }

        [Benchmark(Baseline = true)]
        public void ExecuteStage()
        {
            // measure *only* the new code for this stage:
            switch (Stage)
            {
                case CssPipelineStage.FullBuild:
                    _ = AppRunnerExtensions.FullBuildCssAsync(appRunner).GetAwaiter().GetResult();
                    break;

                case CssPipelineStage.SfumatoExtract:
                    RunSfumatoExtract();
                    break;

                case CssPipelineStage.CssImports:
	                RunCssImports();
	                break;

                case CssPipelineStage.ComponentsLayer:
                    AppRunnerExtensions.ProcessAllComponentsLayersAndCss(appRunner);
                    break;

                case CssPipelineStage.UtilityClasses:
                    AppRunnerExtensions.GenerateUtilityClasses(appRunner);
                    break;

                case CssPipelineStage.AtApplyStatements:
                    RunAtApplyStatements();
                    break;

                case CssPipelineStage.Functions:
                    RunFunctions();
                    break;

                case CssPipelineStage.AtVariantStatements:
                    RunAtVariantStatements();
                    break;

                case CssPipelineStage.ProcessDarkThemeClasses:
	                RunProcessDarkThemeClasses();
	                break;

	            case CssPipelineStage.GatherCssCustomPropRefs:
		            RunGatherCssCustomPropRefs();
		            break;

                case CssPipelineStage.GatherUsedCssRefs:
	                RunGatherUsedCssRefs();
		            break;

                case CssPipelineStage.MergeUsedDependencies:
	                appRunner.MergeUsedDependencies();
		            break;
                
                case CssPipelineStage.GeneratePropsAndThemeLayers:
                    appRunner.GeneratePropertiesAndThemeLayers();
                    break;

                case CssPipelineStage.FinalCssAssembly:
                    RunFinalCssAssembly();
                    break;
                
                case CssPipelineStage.GenerateCss:
	                RunGenerateCss();
	                break;
            }
        }

        private void RunSfumatoExtract()
        {
            var (i, len) = appRunner.CustomCssSegment.Content.ExtractSfumatoBlock(appRunner);
            if (i > -1) appRunner.CustomCssSegment.Content.Remove(i, len);
        }

        private void RunCssImports()
        {
	        var (i, len) = appRunner.CustomCssSegment.Content.ExtractCssImportStatements(appRunner, true);
	        if (i > -1) appRunner.CustomCssSegment.Content.Remove(i, len);
        }

        private void RunAtApplyStatements()
        {
	        appRunner.ProcessSegmentAtApplyStatementsAsync(appRunner.ImportsCssSegment).GetAwaiter();
	        appRunner.ProcessSegmentAtApplyStatementsAsync(appRunner.ComponentsCssSegment).GetAwaiter();
	        appRunner.ProcessSegmentAtApplyStatementsAsync(appRunner.CustomCssSegment).GetAwaiter();
        }

        private void RunFunctions()
        {
	        appRunner.ProcessSegmentFunctionsAsync(appRunner.BrowserResetCssSegment).GetAwaiter();
	        appRunner.ProcessSegmentFunctionsAsync(appRunner.FormsCssSegment).GetAwaiter();
	        appRunner.ProcessSegmentFunctionsAsync(appRunner.UtilitiesCssSegment).GetAwaiter();
	        appRunner.ProcessSegmentFunctionsAsync(appRunner.ImportsCssSegment).GetAwaiter();
	        appRunner.ProcessSegmentFunctionsAsync(appRunner.ComponentsCssSegment).GetAwaiter();
	        appRunner.ProcessSegmentFunctionsAsync(appRunner.CustomCssSegment).GetAwaiter();
        }

        private void RunAtVariantStatements()
        {
	        appRunner.ProcessSegmentAtVariantStatementsAsync(appRunner.UtilitiesCssSegment).GetAwaiter();
	        appRunner.ProcessSegmentAtVariantStatementsAsync(appRunner.ImportsCssSegment).GetAwaiter();
	        appRunner.ProcessSegmentAtVariantStatementsAsync(appRunner.ComponentsCssSegment).GetAwaiter();
	        appRunner.ProcessSegmentAtVariantStatementsAsync(appRunner.CustomCssSegment).GetAwaiter();
        }

        private void RunProcessDarkThemeClasses()
        {
            if (appRunner.AppRunnerSettings.UseDarkThemeClasses == false)
                return;

            foreach (var seg in new[]{ 
                appRunner.UtilitiesCssSegment,
                appRunner.ImportsCssSegment,
                appRunner.ComponentsCssSegment,
                appRunner.CustomCssSegment })
            {
                appRunner.ProcessDarkThemeClassesAsync(seg).GetAwaiter();
            }
        }

        private void RunGatherCssCustomPropRefs()
        {
	        appRunner.PropertiesCssSegment.Content.Clear();
	        appRunner.PropertyListCssSegment.Content.Clear();
	        appRunner.ThemeCssSegment.Content.Clear();

	        appRunner.GatherSegmentCssCustomPropertyRefsAsync(appRunner.BrowserResetCssSegment).GetAwaiter();
	        appRunner.GatherSegmentCssCustomPropertyRefsAsync(appRunner.FormsCssSegment).GetAwaiter();
	        appRunner.GatherSegmentCssCustomPropertyRefsAsync(appRunner.UtilitiesCssSegment).GetAwaiter();
	        appRunner.GatherSegmentCssCustomPropertyRefsAsync(appRunner.ImportsCssSegment).GetAwaiter();
	        appRunner.GatherSegmentCssCustomPropertyRefsAsync(appRunner.ComponentsCssSegment).GetAwaiter();
	        appRunner.GatherSegmentCssCustomPropertyRefsAsync(appRunner.CustomCssSegment).GetAwaiter();
        }
        
        private void RunGatherUsedCssRefs()
        {
	        appRunner.GatherSegmentUsedCssRefsAsync(appRunner.BrowserResetCssSegment).GetAwaiter();
	        appRunner.GatherSegmentUsedCssRefsAsync(appRunner.FormsCssSegment).GetAwaiter();
	        appRunner.GatherSegmentUsedCssRefsAsync(appRunner.UtilitiesCssSegment).GetAwaiter();
	        appRunner.GatherSegmentUsedCssRefsAsync(appRunner.ImportsCssSegment).GetAwaiter();
	        appRunner.GatherSegmentUsedCssRefsAsync(appRunner.ComponentsCssSegment).GetAwaiter();
	        appRunner.GatherSegmentUsedCssRefsAsync(appRunner.CustomCssSegment).GetAwaiter();
        }
        
        private void RunFinalCssAssembly()
        {
	        appRunner.FinalCssAssembly();
        }
        
        private void RunGenerateCss()
        {
	        _ = appRunner.GenerateFinalCss();
        }
    }
}