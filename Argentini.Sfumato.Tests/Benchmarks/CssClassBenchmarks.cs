using Argentini.Sfumato.Validators;
using BenchmarkDotNet.Attributes;
using Microsoft.DotNet.PlatformAbstractions;

namespace Argentini.Sfumato.Tests.Benchmarks;

public class CssClassBenchmarks
{
    private AppRunner AppRunner { get; set; } = new(new AppState());
    private CssClass _cssClass = null!;

    private CssClass _cssClass2 = null!;
    private CssClass _cssClass3 = null!;
    private CssClass _cssClass4 = null!;
    private CssClass _cssClass5 = null!;
    private CssClass _cssClass6 = null!;
    
    //private const string UtilityClass = "text-sm";
    private const string UtilityClass = "tabp:hover:text-sm";
    //private const string UtilityClass = "dark:group-[.is-published]:[&.active]:[@supports(display:flex)]:tabp:max-desk:hover:text-[1rem]/6!";

    [GlobalSetup]
    public void Setup()
    {
        var basePath = ApplicationEnvironment.ApplicationBasePath;
        var root = basePath[..basePath.IndexOf("Developer", StringComparison.Ordinal)];

        AppRunner = new(new AppState(), Path.GetFullPath(Path.Combine(root, "Developer/Sfumato-Web/UmbracoCms/wwwroot/stylesheets/source.css")));

        _cssClass = new CssClass(AppRunner);
        _cssClass.Selector = UtilityClass;
        
        _cssClass2 = new CssClass(AppRunner);
        _cssClass2.Selector = UtilityClass;
        _cssClass2.SplitSelectorSegments();

        _cssClass3 = new CssClass(AppRunner);
        _cssClass3.Selector = UtilityClass;
        _cssClass3.SplitSelectorSegments();
        _cssClass3.ProcessArbitraryCss();
        
        _cssClass4 = new CssClass(AppRunner);
        _cssClass4.Selector = UtilityClass;
        _cssClass4.SplitSelectorSegments();
        _cssClass4.ProcessArbitraryCss();
        _cssClass4.ProcessUtilityClasses();
        
        _cssClass5 = new CssClass(AppRunner);
        _cssClass5.Selector = UtilityClass;
        _cssClass5.SplitSelectorSegments();
        _cssClass5.ProcessArbitraryCss();
        _cssClass5.ProcessUtilityClasses();
        _cssClass5.ProcessVariants();
        
        _cssClass6 = new CssClass(AppRunner);
        _cssClass6.Selector = UtilityClass;
        _cssClass6.SplitSelectorSegments();
        _cssClass6.ProcessArbitraryCss();
        _cssClass6.ProcessUtilityClasses();
        _cssClass6.ProcessVariants();
        _cssClass6.GenerateSelector();
    }

    [Benchmark(Baseline = true)]
    public void FullConstruct()
    {
        _cssClass = new CssClass(AppRunner, UtilityClass);
    }

    [Benchmark]
    public void Initialize()
    {
        _cssClass.Initialize(false);
    }

    [Benchmark]
    public void Step1_SplitSegments()
    {
        foreach (var segment in _cssClass.Selector.SplitByTopLevel(':'))
            _cssClass.AllSegments.Add(segment.ToString());
    }

    [Benchmark]
    public void Step2_ProcessArbitraryCss()
    {
        _cssClass2.ProcessArbitraryCss();
    }

    [Benchmark]
    public void Step3_ProcessUtilityClasses()
    {
        _cssClass3.ProcessUtilityClasses();
    }

    [Benchmark]
    public void Step4_ProcessVariants()
    {
        if (_cssClass4.AllSegments.Count > 1)
            _cssClass4.ProcessVariants();
    }

    [Benchmark]
    public void Step5_GenerateSelector()
    {
        _cssClass5.GenerateSelector();
    }

    [Benchmark]
    public void Step6_GenerateWrappers()
    {
        _cssClass6.GenerateWrappers();
    }

    [Benchmark]
    public void GenerateStyles()
    {
        _cssClass6.GenerateStyles();
    }

    [Benchmark]
    public void CssSelectorEscape()
    {
        _ = UtilityClass.CssSelectorEscape();
    }

    [Benchmark]
    public void ProcessCssClassLevel1()
    {
      _ = new CssClass(AppRunner, "text-sm");
    }

    [Benchmark]
    public void ProcessCssClassLevel2()
    {
      _ = new CssClass(AppRunner, "tabp:hover:text-sm");
    }

    [Benchmark]
    public void ProcessCssClassLevel3()
    {
      _ = new CssClass(AppRunner, "dark:group-[.is-published]:[&.active]:[@supports(display:flex)]:tabp:max-desk:hover:text-[1rem]/6!");
    }
}