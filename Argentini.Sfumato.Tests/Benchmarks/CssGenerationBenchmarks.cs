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
        GeneratePropertiesAndThemeLayers,
        ProcessDarkTheme,
        Appending,
        Formatting
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
            CssPipelineStage.GeneratePropertiesAndThemeLayers,
            CssPipelineStage.ProcessDarkTheme,
            CssPipelineStage.Appending,
            CssPipelineStage.Formatting)]
        public CssPipelineStage Stage { get; set; }

        private AppRunner _runner;
        private string _cssPath;

        [IterationSetup]
        public void IterationSetup()
        {
	        var basePath = ApplicationEnvironment.ApplicationBasePath;
	        var root = basePath[..basePath.IndexOf("Developer", StringComparison.Ordinal)];

	        _cssPath = Path.GetFullPath(Path.Combine(root, "Developer/Fynydd-Website-2024/UmbracoCms/wwwroot/stylesheets/source.css"));

	        _runner = new AppRunner(new AppState(), _cssPath);

	        _runner.LoadCssFileAsync().GetAwaiter().GetResult();
	        _runner.PerformFileScanAsync().GetAwaiter().GetResult();
	        
            // 1) reset content
            _runner.CustomCssSegment.ReplaceContent(_runner.AppRunnerSettings.CssContent);

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
                    AppRunnerExtensions.ProcessComponentsLayerAndCss(_runner);
                    break;

                case CssPipelineStage.AtApplyStatements:
	                RunSfumatoExtract();
	                RunCssImports();
                    AppRunnerExtensions.ProcessComponentsLayerAndCss(_runner);
                    AppRunnerExtensions.ProcessUtilityClasses(_runner);
                    break;

                case CssPipelineStage.Functions:
	                RunSfumatoExtract();
	                RunCssImports();
                    AppRunnerExtensions.ProcessComponentsLayerAndCss(_runner);
                    AppRunnerExtensions.ProcessUtilityClasses(_runner);
                    RunAtApplyStatements();
                    break;

                case CssPipelineStage.AtVariantStatements:
	                RunSfumatoExtract();
	                RunCssImports();
                    AppRunnerExtensions.ProcessComponentsLayerAndCss(_runner);
                    AppRunnerExtensions.ProcessUtilityClasses(_runner);
                    RunAtApplyStatements();
                    RunFunctions();
                    break;

                case CssPipelineStage.GeneratePropertiesAndThemeLayers:
	                RunSfumatoExtract();
	                RunCssImports();
                    AppRunnerExtensions.ProcessComponentsLayerAndCss(_runner);
                    AppRunnerExtensions.ProcessUtilityClasses(_runner);
                    RunAtApplyStatements();
                    RunFunctions();
                    RunAtVariantStatements();
                    break;

                case CssPipelineStage.ProcessDarkTheme:
	                RunSfumatoExtract();
	                RunCssImports();
                    AppRunnerExtensions.ProcessComponentsLayerAndCss(_runner);
                    AppRunnerExtensions.ProcessUtilityClasses(_runner);
                    RunAtApplyStatements();
                    RunFunctions();
                    RunAtVariantStatements();
                    AppRunnerExtensions.GeneratePropertiesAndThemeLayers(_runner);
                    break;

                case CssPipelineStage.Appending:
	                RunSfumatoExtract();
	                RunCssImports();
                    AppRunnerExtensions.ProcessComponentsLayerAndCss(_runner);
                    AppRunnerExtensions.ProcessUtilityClasses(_runner);
                    RunAtApplyStatements();
                    RunFunctions();
                    RunAtVariantStatements();
                    AppRunnerExtensions.GeneratePropertiesAndThemeLayers(_runner);
                    RunProcessDarkTheme();
                    break;

                case CssPipelineStage.Formatting:
                    // everything before the final "AllTasks" step:
                    RunSfumatoExtract();
                    RunCssImports();
                    AppRunnerExtensions.ProcessComponentsLayerAndCss(_runner);
                    AppRunnerExtensions.ProcessUtilityClasses(_runner);
                    RunAtApplyStatements();
                    RunFunctions();
                    RunAtVariantStatements();
                    AppRunnerExtensions.GeneratePropertiesAndThemeLayers(_runner);
                    RunProcessDarkTheme();
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
                    _ = _runner.AppRunnerSettings.CssContent.BuildCss(_runner);
                    break;

                case CssPipelineStage.SfumatoExtract:
                    RunSfumatoExtract();
                    break;

                case CssPipelineStage.CssImports:
	                RunCssImports();
	                break;

                case CssPipelineStage.ComponentsLayer:
                    AppRunnerExtensions.ProcessComponentsLayerAndCss(_runner);
                    break;

                case CssPipelineStage.UtilityClasses:
                    AppRunnerExtensions.ProcessUtilityClasses(_runner);
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

                case CssPipelineStage.GeneratePropertiesAndThemeLayers:
                    AppRunnerExtensions.GeneratePropertiesAndThemeLayers(_runner);
                    break;

                case CssPipelineStage.ProcessDarkTheme:
                    RunProcessDarkTheme();
                    break;

                case CssPipelineStage.Appending:
                    RunAppending();
                    break;

                case CssPipelineStage.Formatting:
	                RunFormattingTasks();  // e.g. minify + final ToString
                    break;
            }
        }

        private void RunSfumatoExtract()
        {
            var (i, len) = _runner.CustomCssSegment.ExtractSfumatoBlock(_runner);
            if (i > -1) _runner.CustomCssSegment.Remove(i, len);
        }

        private void RunCssImports()
        {
	        var (i, len) = _runner.CustomCssSegment.ProcessCssImportStatements(_runner, true);
	        if (i > -1) _runner.CustomCssSegment.Remove(i, len);
        }

        private void RunAtApplyStatements()
        {
            AppRunnerExtensions.ProcessAtApplyStatements(_runner.ImportsCssSegment,    _runner);
            AppRunnerExtensions.ProcessAtApplyStatements(_runner.ComponentsCssSegment, _runner);
            AppRunnerExtensions.ProcessAtApplyStatements(_runner.CustomCssSegment,     _runner);
        }

        private void RunFunctions()
        {
	        AppRunnerExtensions.ProcessFunctions(_runner.BrowserResetCss,             _runner);
            AppRunnerExtensions.ProcessFunctions(_runner.FormsCss,             _runner);
            AppRunnerExtensions.ProcessFunctions(_runner.UtilitiesCssSegment,  _runner);
            AppRunnerExtensions.ProcessFunctions(_runner.ImportsCssSegment,    _runner);
            AppRunnerExtensions.ProcessFunctions(_runner.ComponentsCssSegment, _runner);
            AppRunnerExtensions.ProcessFunctions(_runner.CustomCssSegment,     _runner);
        }

        private void RunAtVariantStatements()
        {
            AppRunnerExtensions.ProcessAtVariantStatements(_runner.UtilitiesCssSegment,  _runner);
            AppRunnerExtensions.ProcessAtVariantStatements(_runner.ImportsCssSegment,    _runner);
            AppRunnerExtensions.ProcessAtVariantStatements(_runner.ComponentsCssSegment, _runner);
            AppRunnerExtensions.ProcessAtVariantStatements(_runner.CustomCssSegment,     _runner);
        }

        private void RunProcessDarkTheme()
        {
            if (!_runner.AppRunnerSettings.UseDarkThemeClasses)
                return;

            foreach (var seg in new[]{ 
                _runner.UtilitiesCssSegment,
                _runner.ImportsCssSegment,
                _runner.ComponentsCssSegment,
                _runner.CustomCssSegment })
            {
                AppRunnerExtensions.ProcessDarkTheme(seg, _runner);
            }
        }

        private void RunAppending()
        {
	        var appRunner = _runner;
	        
            // copy in the StringBuilder-based append/minify logic here
            var workingSb = appRunner.AppState.StringBuilderPool.Get();
			var outputSb = appRunner.AppState.StringBuilderPool.Get();

			try
			{
				var useLayers = appRunner.AppRunnerSettings.UseCompatibilityMode == false;

				if (useLayers)
				{
					outputSb.Append("@layer properties, theme, base, forms, components, utilities;");
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
					
					outputSb.Append("@layer theme {");
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);

					outputSb.Append(appRunner.ThemeCssSegment);
					
					outputSb.Append('}');
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
					
				}
				else
				{
					outputSb.Append(appRunner.ThemeCssSegment);
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}

				if (appRunner.AppRunnerSettings.UseReset)
				{
					if (useLayers)
					{
						outputSb.Append("@layer base {");
						outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
					
						outputSb.Append(appRunner.BrowserResetCss);
						
						outputSb.Append('}');
						outputSb.Append(appRunner.AppRunnerSettings.LineBreak);

					}
					else
					{
						outputSb.Append(appRunner.BrowserResetCss);
						outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
					}
				}

				if (appRunner.AppRunnerSettings.UseForms)
				{
					if (useLayers)
					{
						outputSb.Append("@layer forms {");
						outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
					
						outputSb.Append(appRunner.FormsCss);
						
						outputSb.Append('}');
						outputSb.Append(appRunner.AppRunnerSettings.LineBreak);

					}
					else
					{
						outputSb.Append(appRunner.FormsCss);
						outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
					}
				}

				if (useLayers && appRunner.ComponentsCssSegment.Length > 0)
				{
					outputSb.Append("@layer components {");
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
					
					outputSb.Append(appRunner.ComponentsCssSegment);
						
					outputSb.Append('}');
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);

				}
				else
				{
					outputSb.Append(appRunner.ComponentsCssSegment);
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}

				if (useLayers)
				{
					outputSb.Append("@layer utilities {");
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
					
					outputSb.Append(appRunner.UtilitiesCssSegment);
						
					outputSb.Append('}');
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}
				else
				{
					outputSb.Append(appRunner.UtilitiesCssSegment);
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}

				if (appRunner.ImportsCssSegment.Length > 0)
				{
					outputSb.Append(appRunner.ImportsCssSegment);
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}

				if (appRunner.CustomCssSegment.Length > 0)
				{
					outputSb.Append(appRunner.CustomCssSegment);
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}

				if (appRunner.PropertyListCssSegment.Length > 0)
				{
					outputSb.Append(appRunner.PropertyListCssSegment);
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}

				if (useLayers)
				{
					outputSb.Append("@layer properties {");
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
					
					outputSb.Append(appRunner.PropertiesCssSegment);
						
					outputSb.Append('}');
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}
				else
				{
					outputSb.Append(appRunner.PropertiesCssSegment);
					outputSb.Append(appRunner.AppRunnerSettings.LineBreak);
				}
			}
			finally
			{
				appRunner.AppState.StringBuilderPool.Return(workingSb);
				appRunner.AppState.StringBuilderPool.Return(outputSb);
			}
        }

        private void RunFormattingTasks()
        {
            // same as WithAllTasks: after RunAppending() include minify/normalize

            var workingSb = _runner.AppState.StringBuilderPool.Get();
            var outputSb = _runner.AppState.StringBuilderPool.Get();

            try
            {
	            _ = _runner.AppRunnerSettings.UseMinify ? outputSb.ToString().CompactCss(workingSb) : outputSb.ReformatCss(workingSb).ToString().NormalizeLinebreaks(_runner.AppRunnerSettings.LineBreak);
            }
            finally
            {
	            _runner.AppState.StringBuilderPool.Return(workingSb);
	            _runner.AppState.StringBuilderPool.Return(outputSb);
            }
        }
    }
}