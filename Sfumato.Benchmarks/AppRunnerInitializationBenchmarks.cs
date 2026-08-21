using BenchmarkDotNet.Attributes;
using Sfumato.Entities.Runners;

namespace Sfumato.Benchmarks;

[BenchmarkCategory("Startup")]
[MemoryDiagnoser]
[MedianColumn]
public class AppRunnerInitializationBenchmarks : BenchmarkBase
{
    [Benchmark]
    public AppRunner Initialize() => CreateAppRunner();
}
