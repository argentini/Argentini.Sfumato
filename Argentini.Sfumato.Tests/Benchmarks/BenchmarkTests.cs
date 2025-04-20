using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Perfolizer.Horology;
using Xunit.Abstractions;

namespace Argentini.Sfumato.Tests.Benchmarks;

public class BenchmarkTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Benchmarks()
    {
        testOutputHelper.WriteLine("YOU MUST RUN THIS IN RELEASE MODE");
        testOutputHelper.WriteLine("");

        var summary = BenchmarkRunner.Run<Benchmarks>();

        Assert.NotNull(summary);
        
        testOutputHelper.WriteLine(GetBenchmarkSummaryText(summary));
    }
    
    private static string GetBenchmarkSummaryText(Summary summary)
    {
        using var writer = new StringWriter();
        var wrappedLogger = new AccumulationLogger(); // Accumulates logs in memory

        MarkdownExporter.Console.ExportToLog(summary, wrappedLogger);
        
        return wrappedLogger.GetLog(); // Retrieve formatted text output
    }
}

public class NanosecondsConfig : ManualConfig
{
    public NanosecondsConfig()
    {
        AddColumn(StatisticColumn.Mean, StatisticColumn.Error, StatisticColumn.StdDev);
        SummaryStyle = SummaryStyle.Default.WithTimeUnit(TimeUnit.Nanosecond);
    }
}
public class MicrosecondsConfig : ManualConfig
{
    public MicrosecondsConfig()
    {
        AddColumn(StatisticColumn.Mean, StatisticColumn.Error, StatisticColumn.StdDev);
        SummaryStyle = SummaryStyle.Default.WithTimeUnit(TimeUnit.Microsecond);
    }
}
public class MillisecondsConfig : ManualConfig
{
    public MillisecondsConfig()
    {
        AddColumn(StatisticColumn.Mean, StatisticColumn.Error, StatisticColumn.StdDev);
        SummaryStyle = SummaryStyle.Default.WithTimeUnit(TimeUnit.Millisecond);
    }
}