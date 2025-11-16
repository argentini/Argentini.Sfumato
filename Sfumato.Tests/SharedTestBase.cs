using Microsoft.DotNet.PlatformAbstractions;

namespace Sfumato.Tests;

public class SharedTestBase
{
    private static string Root => ApplicationEnvironment.ApplicationBasePath[..ApplicationEnvironment.ApplicationBasePath.IndexOf("Sfumato.Tests", StringComparison.Ordinal)];

    protected StringBuilderPool StringBuilderPool { get; } = new ();
    protected ITestOutputHelper? TestOutputHelper { get; }
    protected bool IsRelease { get; }

    public AppRunner AppRunner { get; protected set; }
    protected static string SampleCssFilePath => Path.GetFullPath(Path.Combine(Root, "Sfumato.Tests/SampleCss/sample.css"));
    protected static string ExportCssFilePath => Path.GetFullPath(Path.Combine(Root, "Sfumato.Tests/Export/export.css"));
    protected static string ScanSampleFilePath => Path.GetFullPath(Path.Combine(Root, "Sfumato.Tests/SampleWebsite/scan-sample.html"));
    protected static string SampleWebsiteSourceFilePath => Path.GetFullPath(Path.Combine(Root, "Sfumato.Tests/SampleWebsite/wwwroot/stylesheets/source.css"));

    protected SharedTestBase(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
        
#if RELEASE
        IsRelease = true;
#else
        IsRelease = false;
#endif
        
        AppRunner = new AppRunner(StringBuilderPool, SampleCssFilePath);
    }
}