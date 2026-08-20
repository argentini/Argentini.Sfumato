using BenchmarkDotNet.Attributes;
using Sfumato.Entities.Runners;
using Library = Sfumato.Entities.Library.Library;

namespace Sfumato.Benchmarks;

[BenchmarkCategory("Startup")]
[MemoryDiagnoser]
[MedianColumn]
public class AppRunnerInitializationBenchmarks : BenchmarkBase
{
    [Benchmark]
    public Library InitializeLibrary() => new();

    [Benchmark]
    public AppRunner Initialize() => CreateAppRunner();
}
