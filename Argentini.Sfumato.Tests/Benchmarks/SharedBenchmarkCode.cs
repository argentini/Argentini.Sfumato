// ReSharper disable MemberCanBeMadeStatic.Global
// ReSharper disable MemberCanBePrivate.Global

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.DotNet.PlatformAbstractions;

namespace Argentini.Sfumato.Tests.Benchmarks;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class SharedBenchmarkCode
{
    public AppRunner AppRunner { get; private set; }
    private ITestOutputHelper? TestOutputHelper { get; }
    public bool IsDebug { get; }
    private int Iterations { get; }

    public long GlobalStartTime { get; } = Stopwatch.GetTimestamp();
    public double GlobalMeanTime { get; set; }
    public string BasicUtilityClass => "text-sm";
    public string AverageUtilityClass => "tabp:hover:text-sm";
    public string LargeUtilityClass => "dark:group-[.is-published]:[&.active]:[@supports(display:flex)]:tabp:max-desk:hover:text-[1rem]/6!";

    private static readonly double NsPerTick = 1_000_000_000.0 / Stopwatch.Frequency;
    private const int InnerRepeat = 10;
    private const int WarmupTimeSeconds = 2;

    public SharedBenchmarkCode(ITestOutputHelper testOutputHelper, int? iterations = null)
    {
        TestOutputHelper = testOutputHelper;
        Iterations = iterations ?? 100;
        
#if DEBUG
        IsDebug = true;
#else
        IsDebug = false;
#endif

        AppRunner = new AppRunner(new AppState());

        TestOutputHelper?.WriteLine("B E N C H M A R K  -----------------------------------------------------------------------------");
        TestOutputHelper?.WriteLine(string.Empty);
        TestOutputHelper?.WriteLine($"Timestamp   :  {DateTime.Now:s}");
        TestOutputHelper?.WriteLine($"Platform    :  {GetOsPlatformName()}");
        TestOutputHelper?.WriteLine($"Version     :  {Environment.OSVersion.VersionString}");
        TestOutputHelper?.WriteLine($"Cores       :  {Environment.ProcessorCount}");
        TestOutputHelper?.WriteLine($"Build       :  {(IsDebug ? "Debug" : "Release")}");
        TestOutputHelper?.WriteLine($"Iterations  :  {Iterations} x {InnerRepeat}");
        TestOutputHelper?.WriteLine($"Warmup      :  {WarmupTimeSeconds:F1} s");
        TestOutputHelper?.WriteLine(string.Empty);
        TestOutputHelper?.WriteLine("Method                                                               Mean                Elapsed");
        TestOutputHelper?.WriteLine("------------------------------------------------------------------------------------------------");
    }

    public async ValueTask IterationSetup()
    {
        var basePath = ApplicationEnvironment.ApplicationBasePath;
        var root = basePath[..basePath.IndexOf("Argentini.Sfumato.Tests", StringComparison.Ordinal)];

        AppRunner = new AppRunner(new AppState(), Path.GetFullPath(Path.Combine(root, "Argentini.Sfumato.Tests/SampleWebsite/wwwroot/stylesheets/source.css")));

        await Task.CompletedTask;
    }

    public void OutputTotalTime()
    {
        var testTime = Stopwatch.GetElapsedTime(GlobalStartTime);

        TestOutputHelper?.WriteLine("------------------------------------------------------------------------------------------------");
        TestOutputHelper?.WriteLine($"{"Totals",-45}   {GlobalMeanTime,22:N0} ns   {testTime.FormatTimer(),20}");
    }

    public async ValueTask BenchmarkMethod(string methodName, Func<Task> action)
    {
        await BenchmarkMethod(methodName, null, action);
    }

    public async ValueTask BenchmarkMethod(string methodName, Func<Task>? setupAction, Func<Task> action)
    {
        var testStartTime = Stopwatch.GetTimestamp();
        var log = new List<double>();

        #region Warmup

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect(); // Ensure Gen2 cleanup        
        
        // Force JIT compilation ahead of time
        if (setupAction is not null)
            RuntimeHelpers.PrepareMethod(setupAction.Method.MethodHandle);

        RuntimeHelpers.PrepareMethod(action.Method.MethodHandle);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process.GetCurrentProcess().ProcessorAffinity = 1;
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
        }

        do
        {
            if (setupAction is not null)
                await setupAction();

            var start = Stopwatch.GetTimestamp();
            await action();
            var elapsed = Stopwatch.GetTimestamp() - start;

            log.Add(elapsed * NsPerTick);

        } while (Stopwatch.GetElapsedTime(testStartTime).TotalSeconds < WarmupTimeSeconds);

        #endregion

        log = [];
        
        for (var i = 0; i < Iterations; i++)
        {
            for (var j = 0; j < InnerRepeat; j++)
            {
                if (setupAction is not null)
                    await setupAction(); // NOT timed

                var start = Stopwatch.GetTimestamp();
                await action();         // ONLY THIS is timed
                var elapsed = Stopwatch.GetTimestamp() - start;

                log.Add(elapsed * NsPerTick);
            }
        }        
        
        var sample = log
            .OrderBy(x => x)
            .Skip(log.Count / 10)
            .Take(log.Count * 8 / 10); // Trim 10% from top and bottom

        var averageNs = sample.Average();
        var endTestTime = Stopwatch.GetTimestamp() - testStartTime;

        GlobalMeanTime += averageNs;
        
        TestOutputHelper?.WriteLine($"{methodName,-45}   {averageNs,22:N0} ns   {(endTestTime * NsPerTick).FormatTimerFromNanoseconds(),20}");
    }
    
    /// <summary>
    /// Get OS platform.
    /// </summary> 
    /// <returns>OSPlatform object</returns> 
    public static OSPlatform GetOsPlatform() 
    { 
        var osPlatform = OSPlatform.Create("Other platform");

        // Check if it's windows 
        var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows); 
        osPlatform = isWindows ? OSPlatform.Windows : osPlatform; 

        // Check if it's osx 
        var isOsx = RuntimeInformation.IsOSPlatform(OSPlatform.OSX); 
        osPlatform = isOsx ? OSPlatform.OSX : osPlatform; 

        // Check if it's Linux 
        var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux); 
        osPlatform = isLinux ? OSPlatform.Linux : osPlatform; 

        return osPlatform; 
    } 

    /// <summary>
    /// Get OS platform name for output to users.
    /// </summary> 
    /// <returns>OSPlatform name (friendly for output)</returns> 
    public static string GetOsPlatformName()
    {
        var result = "Other";

        if (GetOsPlatform() == OSPlatform.OSX)
        {
            result = "macOS";
        }

        else if (GetOsPlatform() == OSPlatform.Linux)
        {
            result = "Linux";
        }

        else if (GetOsPlatform() == OSPlatform.Windows)
        {
            result = "Windows";
        }

        return result;
    }
}
