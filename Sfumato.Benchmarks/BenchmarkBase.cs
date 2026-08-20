using Sfumato.Entities.Runners;
using Sfumato.Helpers;

namespace Sfumato.Benchmarks;

public abstract class BenchmarkBase
{
    protected StringBuilderPool StringBuilderPool { get; } = new();

    protected static string SampleWebsiteSourceFilePath => Path.Combine(
        AppContext.BaseDirectory,
        "Fixtures",
        "SampleWebsite",
        "wwwroot",
        "stylesheets",
        "source.css"
    );

    protected AppRunner CreateAppRunner() => new(StringBuilderPool, SampleWebsiteSourceFilePath);
}
