using System.Text;
using BenchmarkDotNet.Attributes;
using Sfumato.Helpers;

namespace Sfumato.Benchmarks;

[MemoryDiagnoser]
[MedianColumn]
[BenchmarkCategory("Strings")]
public class StringBuilderBenchmarks
{
    private StringBuilder _source = null!;
    private string _needle = null!;

    [GlobalSetup]
    public void Setup()
    {
        _needle = "needle-value";
        _source = new StringBuilder(new string('a', 4_096));
        _source.Append(_needle);
    }

    [Benchmark]
    public string Substring() => _source.Substring(1_024, 512);

    [Benchmark]
    public int IndexOfLateMatch() => _source.IndexOf(_needle);

    [Benchmark]
    public bool ContainsLateMatch() => _source.Contains(_needle);
}
