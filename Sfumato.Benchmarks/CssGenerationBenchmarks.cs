using BenchmarkDotNet.Attributes;
using Sfumato.Entities.Runners;

namespace Sfumato.Benchmarks;

[BenchmarkCategory("Generation")]
[MemoryDiagnoser]
[MedianColumn]
public class CssGenerationBenchmarks : BenchmarkBase
{
    [Benchmark]
    public async Task<string> FullBuild()
    {
        var appRunner = CreateAppRunner();

        await appRunner.LoadCssFileAsync();
        await appRunner.PerformFileScanAsync();

        return await AppRunnerExtensions.FullBuildCssAsync(appRunner);
    }
}
