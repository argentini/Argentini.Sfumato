using BenchmarkDotNet.Attributes;
using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Runners;

namespace Sfumato.Benchmarks;

[BenchmarkCategory("CssClass")]
[MemoryDiagnoser]
[MedianColumn]
public class CssClassCreationBenchmarks : BenchmarkBase
{
    private readonly string _basicSelector = "text-sm";
    private readonly string _averageSelector = "tabp:hover:text-sm";
    private readonly string _containerSelector = "sm:container";
    private readonly string _largeSelector = "dark:group-[.is-published]:[&.active]:[@supports(display:flex)]:tabp:max-desk:hover:text-[1rem]/6!";
    private AppRunner _appRunner = null!;

    [GlobalSetup]
    public void Setup() => _appRunner = CreateAppRunner();

    [Benchmark(Baseline = true)]
    public CssClass Basic() => new(_appRunner, selector: _basicSelector);

    [Benchmark]
    public CssClass Average() => new(_appRunner, selector: _averageSelector);

    [Benchmark]
    public CssClass Container() => new(_appRunner, selector: _containerSelector);

    [Benchmark]
    public CssClass Large() => new(_appRunner, selector: _largeSelector);
}
