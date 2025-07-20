using Microsoft.DotNet.PlatformAbstractions;

namespace Argentini.Sfumato.Tests;

public class SharedTestBase
{
    private static string Root => ApplicationEnvironment.ApplicationBasePath[..ApplicationEnvironment.ApplicationBasePath.IndexOf("Argentini.Sfumato.Tests", StringComparison.Ordinal)];

    protected ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();
    protected ITestOutputHelper? TestOutputHelper { get; }
    protected bool IsRelease { get; }

    public AppRunner AppRunner { get; protected set; }
    protected static string SampleCssFilePath => Path.GetFullPath(Path.Combine(Root, "Argentini.Sfumato.Tests/SampleCss/sample.css"));
    protected static string ScanSampleFilePath => Path.GetFullPath(Path.Combine(Root, "Argentini.Sfumato.Tests/SampleWebsite/scan-sample.html"));
    protected static string SampleWebsiteSourceFilePath => Path.GetFullPath(Path.Combine(Root, "Argentini.Sfumato.Tests/SampleWebsite/wwwroot/stylesheets/source.css"));

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