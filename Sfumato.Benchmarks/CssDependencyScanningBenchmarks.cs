using System.Text;
using BenchmarkDotNet.Attributes;
using Sfumato.Entities.Runners;

namespace Sfumato.Benchmarks;

[BenchmarkCategory("Generation")]
[MemoryDiagnoser]
[MedianColumn]
public class CssDependencyScanningBenchmarks : BenchmarkBase
{
    private StringBuilder _content = null!;

    [GlobalSetup]
    public void Setup() => _content = new StringBuilder(File.ReadAllText(SampleWebsiteSourceFilePath));

    [Benchmark(Baseline = true)]
    public HashSet<string> SnapshotString() => _content.ToString().GatherCssCustomProperties();

    [Benchmark]
    public HashSet<string> ScanStringBuilder() => _content.GatherCssCustomProperties();
}
