using BenchmarkDotNet.Attributes;

namespace Argentini.Sfumato.Tests.Benchmarks;

public class CssClassBenchmarks
{
    private AppRunner AppRunner { get; } = new(new AppState());
    private CssClass? _cssClass;
    private const string UtilityClass = "text-sm";
    //private const string UtilityClass = "dark:tabp:text-sm";
    //private const string UtilityClass = "dark:group-[.is-published]:[&.active]:[@supports(display:flex)]:tabp:max-desk:hover:text-[1rem]/6!";

    [GlobalSetup]
    public void Setup()
    {
        _cssClass = new CssClass(AppRunner, UtilityClass);
    }

    [Benchmark(Baseline = true)]
    public void FullConstruct()
    {
        _cssClass = new CssClass(AppRunner, UtilityClass);
    }

    [Benchmark]
    public void Initialize()
    {
        _cssClass!.Initialize(false);
    }

    [Benchmark]
    public void Step1_SplitSegments()
    {
        foreach (var segment in _cssClass!.Selector.SplitByTopLevel(':'))
            _cssClass.AllSegments.Add(segment.ToString());
    }

    [Benchmark]
    public void Step2_ProcessArbitraryCss()
    {
        _cssClass!.ProcessArbitraryCss();
    }

    [Benchmark]
    public void Step3_ProcessUtilityClasses()
    {
        if (_cssClass!.IsValid == false)
            _cssClass.ProcessUtilityClasses();
    }

    [Benchmark]
    public void Step4_ProcessVariants()
    {
        if (_cssClass!.IsValid)
            if (_cssClass.AllSegments.Count > 1)
                _cssClass.ProcessVariants();
    }

    [Benchmark]
    public void Step5_GenerateSelector()
    {
        if (_cssClass!.IsValid)
            _cssClass.GenerateSelector();
    }

    [Benchmark]
    public void Step6_GenerateWrappers()
    {
        if (_cssClass!.IsValid)
            _cssClass.GenerateWrappers();
    }

    [Benchmark]
    public void GenerateStyles()
    {
        if (_cssClass!.IsValid)
            _cssClass.GenerateStyles();
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
      _ = new CssClass(AppRunner, "dark:tabp:text-sm");
    }

    [Benchmark]
    public void ProcessCssClassLevel3()
    {
      _ = new CssClass(AppRunner, "dark:group-[.is-published]:[&.active]:[@supports(display:flex)]:tabp:max-desk:hover:text-[1rem]/6!");
    }
}