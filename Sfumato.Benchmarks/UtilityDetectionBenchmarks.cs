using BenchmarkDotNet.Attributes;
using Sfumato.Entities.Runners;
using Sfumato.Helpers;

namespace Sfumato.Benchmarks;

[BenchmarkCategory("Scanning")]
[MemoryDiagnoser]
[MedianColumn]
public class UtilityDetectionBenchmarks : BenchmarkBase
{
    private readonly string _basicSelector = "text-sm";
    private readonly string _averageSelector = "tabp:hover:text-sm";
    private readonly string _largeSelector = "dark:group-[.is-published]:[&.active]:[@supports(display:flex)]:tabp:max-desk:hover:text-[1rem]/6!";
    private AppRunner _appRunner = null!;

    [GlobalSetup]
    public void Setup() => _appRunner = CreateAppRunner();

    [Benchmark(Baseline = true)]
    public bool Basic() => _basicSelector.IsLikelyUtilityClass(_appRunner.Library.ScannerClassNamePrefixes, out _);

    [Benchmark]
    public bool Average() => _averageSelector.IsLikelyUtilityClass(_appRunner.Library.ScannerClassNamePrefixes, out _);

    [Benchmark]
    public bool Large() => _largeSelector.IsLikelyUtilityClass(_appRunner.Library.ScannerClassNamePrefixes, out _);
}
