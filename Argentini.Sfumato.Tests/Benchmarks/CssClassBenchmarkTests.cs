namespace Argentini.Sfumato.Tests.Benchmarks;

public class CssClassBenchmarkTests
{
    private SharedBenchmarkCode Shared { get; }
    private ITestOutputHelper? TestOutputHelper { get; }
    
    public CssClassBenchmarkTests(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
        Shared = new SharedBenchmarkCode(TestOutputHelper);
    }

    [Fact]
    public async Task UtilityClassCreationBenchmark()
    {
        await Shared.IterationSetup();

        await Shared.BenchmarkMethod("BasicUtilityClass",
            async () => {
                _ = new CssClass(Shared.AppRunner, selector: Shared.BasicUtilityClass);
                await Task.CompletedTask;
            });

        await Shared.BenchmarkMethod("AverageUtilityClass",
            async () => {
                _ = new CssClass(Shared.AppRunner, selector: Shared.AverageUtilityClass);
                await Task.CompletedTask;
            });

        await Shared.BenchmarkMethod("LargeUtilityClass",
            async () => {
                _ = new CssClass(Shared.AppRunner, selector: Shared.LargeUtilityClass);
                await Task.CompletedTask;
            });

        Shared.OutputTotalTime();
    }

    [Fact]
    public async Task IsLikelyUtilityClassBenchmark()
    {
        await Shared.IterationSetup();

        await Shared.BenchmarkMethod("IsLikelyUtilityClass_Basic", async () => {
                _ = Shared.BasicUtilityClass.IsLikelyUtilityClass(Shared.AppRunner.Library.ScannerClassNamePrefixes, out _);
                await Task.CompletedTask;
            });

        await Shared.BenchmarkMethod("IsLikelyUtilityClass_Average", async () => {
                _ = Shared.AverageUtilityClass.IsLikelyUtilityClass(Shared.AppRunner.Library.ScannerClassNamePrefixes, out _);
                await Task.CompletedTask;
            });

        await Shared.BenchmarkMethod("IsLikelyUtilityClass_Large", async () => {
                _ = Shared.LargeUtilityClass.IsLikelyUtilityClass(Shared.AppRunner.Library.ScannerClassNamePrefixes, out _);
                await Task.CompletedTask;
            });
        
        Shared.OutputTotalTime();
    }
    
    [Fact]
    public async Task CssClassFlowBenchmark()
    {
        var cssClass = new CssClass(Shared.AppRunner);

        await Shared.IterationSetup();

        await Shared.BenchmarkMethod("Full_Flow", async () => {
            _ = new CssClass(Shared.AppRunner, selector: Shared.AverageUtilityClass);
            await Task.CompletedTask;
        });

        await Shared.BenchmarkMethod("Step1_SplitSegments", async () =>
        {
            cssClass = new CssClass(Shared.AppRunner, selector: Shared.AverageUtilityClass);
            cssClass.ProcessSelectorSegments(false);
            await Task.CompletedTask;
            
        }, async () => {
            foreach (var segment in cssClass.Selector.SplitByTopLevel(':'))
                cssClass.AllSegments.Add(segment.ToString());
            await Task.CompletedTask;
        });

        await Shared.BenchmarkMethod("Step2_ProcessArbitraryCss", async () =>
        {
            cssClass = new CssClass(Shared.AppRunner, selector: Shared.AverageUtilityClass);
            cssClass.ProcessSelectorSegments(false);
            await Task.CompletedTask;

        }, async () => {
            cssClass.ProcessArbitraryCss();
            await Task.CompletedTask;
        });

        await Shared.BenchmarkMethod("Step3_ProcessUtilityClasses", async () =>
        {
            cssClass = new CssClass(Shared.AppRunner, selector: Shared.AverageUtilityClass);
            cssClass.ProcessSelectorSegments(false);
            cssClass.ProcessArbitraryCss();
            await Task.CompletedTask;
            
        }, async () => {
            cssClass.ProcessUtilityClasses();
            await Task.CompletedTask;
        });

        await Shared.BenchmarkMethod("Step4_ProcessVariants", async () =>
        {
            cssClass = new CssClass(Shared.AppRunner, selector: Shared.AverageUtilityClass);
            cssClass.ProcessSelectorSegments(false);
            cssClass.ProcessArbitraryCss();
            cssClass.ProcessUtilityClasses();
            await Task.CompletedTask;
            
        }, async () => {
            cssClass.ProcessVariants();
            await Task.CompletedTask;
        });

        await Shared.BenchmarkMethod("Step5_GenerateSelector", async () =>
        {
            cssClass = new CssClass(Shared.AppRunner, selector: Shared.AverageUtilityClass);
            cssClass.ProcessSelectorSegments(false);
            cssClass.ProcessArbitraryCss();
            cssClass.ProcessUtilityClasses();
            cssClass.ProcessVariants();
            await Task.CompletedTask;
            
        }, async () => {
            cssClass.GenerateSelector();
            await Task.CompletedTask;
        });

        await Shared.BenchmarkMethod("Step6_GenerateWrappers", async () =>
        {
            cssClass = new CssClass(Shared.AppRunner, selector: Shared.AverageUtilityClass);
            cssClass.ProcessSelectorSegments(false);
            cssClass.ProcessArbitraryCss();
            cssClass.ProcessUtilityClasses();
            cssClass.ProcessVariants();
            cssClass.GenerateSelector();
            await Task.CompletedTask;
            
        }, async () => {
            cssClass.GenerateWrappers();
            await Task.CompletedTask;
        });

        await Shared.BenchmarkMethod("GenerateStyles", async () =>
        {
            cssClass = new CssClass(Shared.AppRunner, selector: Shared.AverageUtilityClass);
            cssClass.ProcessSelectorSegments(false);
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

        await Shared.BenchmarkMethod("CssSelectorEscape", async () =>
        {
            cssClass = new CssClass(Shared.AppRunner, selector: Shared.AverageUtilityClass);
            cssClass.ProcessSelectorSegments(false);
            cssClass.ProcessArbitraryCss();
            cssClass.ProcessUtilityClasses();
            cssClass.ProcessVariants();
            cssClass.GenerateSelector();
            cssClass.GenerateWrappers();
            cssClass.GenerateStyles();
            await Task.CompletedTask;

        }, async () => {
            _ = Shared.AverageUtilityClass.CssSelectorEscape();
            await Task.CompletedTask;
        });
        
        Shared.OutputTotalTime();
    }
    
    [Fact]
    public async Task ProcessVariantsBenchmark()
    {
        var cssClass = new CssClass(Shared.AppRunner);

        await Shared.IterationSetup();

        await Shared.BenchmarkMethod("ProcessVariants", async () =>
        {
            cssClass = new CssClass(Shared.AppRunner, selector: Shared.AverageUtilityClass);
            cssClass.ProcessSelectorSegments(false);
            cssClass.ProcessArbitraryCss();
            cssClass.ProcessUtilityClasses();
            await Task.CompletedTask;
            
        }, async () => {
            cssClass.ProcessVariants();
            await Task.CompletedTask;
        });
        
        Shared.OutputTotalTime();
    }
}
