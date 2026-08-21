using BenchmarkDotNet.Attributes;
using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Runners;
using Sfumato.Entities.Scanning;

namespace Sfumato.Benchmarks;

[BenchmarkCategory("Scanning")]
[MemoryDiagnoser]
[MedianColumn]
public class ContentScannerBenchmarks : BenchmarkBase
{
    private AppRunner _appRunner = null!;
    private string _content = string.Empty;

    [GlobalSetup]
    public void Setup()
    {
        _appRunner = CreateAppRunner();
        _content = File.ReadAllText(SampleWebsiteSourceFilePath);
    }

    [Benchmark]
    public Dictionary<string, CssClass> ScanSourceFile() => ContentScanner.ScanFileForUtilityClasses(_content, _appRunner);
}
