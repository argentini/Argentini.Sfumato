using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Perfolizer.Horology;
using Xunit.Abstractions;

namespace Argentini.Sfumato.Tests;

public class BenchmarkTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    // ReSharper disable once ConvertToPrimaryConstructor
    public BenchmarkTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    #region Constants

    private static string Markup => """
                                    <!DOCTYPE html>
                                    <html lang="en">
                                    <head>
                                        <meta charset="UTF-8">
                                        <title>Sample Website</title>
                                        <meta name="viewport" content="width=device-width, initial-scale=1">
                                        <link rel="stylesheet" href="css/sfumato.css">
                                    </head>
                                    <body class="text-base/5 xl:text-base/[3rem] [-webkit-backdrop-filter:blur(1rem)]">
                                        <div id="test-home" class="text-[1rem] lg:text-[1.25rem] bg-fuchsia-500 dark:bg-fuchsia-300 dark:text-[length:1rem] xl:text-[#112233] xl:text-[red] xl:text-[--my-color-var] xl:text-[var(--my-color-var)]">
                                            <p class="[font-weight:900] sm:[font-weight:900]">Placeholder</p>
                                            <p class="[fontweight:400] sm:[fontweight:300] xl:text[#112233] xl:text-slate[#112233] xl:text-slate-50[#112233] xxl:text-slate-50-[#112233]">Invalid Classes</p>
                                        </div>
                                        <div class="content-['Hello!'] block invisible top-8 break-after-auto container aspect-screen xxl:aspect-[8/4]"></div>
                                        <script>
                                            function test() {
                                              let el = document.getElementById('test-element');
                                              if (el) {
                                                    el.classList.add($`
                                                        bg-emerald-900
                                                        [font-weight:700]
                                                        md:[font-weight:700]
                                                    `);
                                                    el.classList.add(`bg-emerald-950`);
                                                    el.classList.add(`[font-weight:600]`);
                                                    el.classList.add(`lg:[font-weight:600]`);
                                              }
                                            }
                                        </script>
                                        @{
                                            var test1 = $""
                                                block bg-slate-400
                                            "";
                                            
                                            var detailsMask = $"<span class=\"line-clamp-1 mt-1 text-slate-500 dark:text-dark-foreground-dim line-clamp-2\"><span class=""line-clamp-2"">{description}</span></span>";
                                        }
                                    </body>
                                    </html>
                                    """;
    
    #endregion

    [Fact]
    public void ScanFileForClassesBenchmark()
    {
        _testOutputHelper.WriteLine("YOU MUST RUN THIS IN RELEASE MODE");
        _testOutputHelper.WriteLine("");

        var summary = BenchmarkRunner.Run<Benchmarks>();

        Assert.NotNull(summary);
        
        _testOutputHelper.WriteLine(GetBenchmarkSummaryText(summary));
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
        SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithTimeUnit(TimeUnit.Nanosecond);
    }
}
public class MicrosecondsConfig : ManualConfig
{
    public MicrosecondsConfig()
    {
        AddColumn(StatisticColumn.Mean, StatisticColumn.Error, StatisticColumn.StdDev);
        SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithTimeUnit(TimeUnit.Microsecond);
    }
}
public class MillisecondsConfig : ManualConfig
{
    public MillisecondsConfig()
    {
        AddColumn(StatisticColumn.Mean, StatisticColumn.Error, StatisticColumn.StdDev);
        SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithTimeUnit(TimeUnit.Millisecond);
    }
}