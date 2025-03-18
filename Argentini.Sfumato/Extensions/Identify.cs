using System.Reflection;
using System.Runtime.Versioning;

namespace Argentini.Sfumato.Extensions;

public static class Identify
{
    #region OS and Runtime

    /// <summary>
    /// Get OS platform.
    /// </summary> 
    /// <returns>OSPlatform object</returns> 
    // ReSharper disable once MemberCanBePrivate.Global
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
        if (GetOsPlatform() == OSPlatform.OSX)
            return "macOS";

        if (GetOsPlatform() == OSPlatform.Linux)
            return "Linux";

        if (GetOsPlatform() == OSPlatform.Windows)
            return "Windows";

        return "Other";
    }

    /// <summary>
    /// Get the .NET Core runtime version (e.g. "2.2").
    /// </summary> 
    /// <returns>String with the .NET Core version number</returns> 
    public static string GetFrameworkVersion()
    {
        var result = Assembly
            .GetEntryAssembly()?
            .GetCustomAttribute<TargetFrameworkAttribute>()?
            .FrameworkName;

        if (result == null || result.IsEmpty()) return string.Empty;

        if (result.Contains("Version="))
            return result.Right("Version=").TrimStart(['v']);

        return result;
    }

    /// <summary>
    /// Get the .NET CLR runtime version (e.g. "4.6.27110.04").
    /// Only works in 2.2 or later.
    /// </summary> 
    /// <returns>String with the .NET CLR runtime version number</returns> 
    public static string GetRuntimeVersion()
    {
        return RuntimeInformation.FrameworkDescription.Right(" ");
    }

    /// <summary>
    /// Get the .NET CLR runtime version string.
    /// Only works in 2.2 or later.
    /// </summary> 
    /// <returns>String with the .NET CLR runtime version number</returns> 
    public static string GetRuntimeVersionFull()
    {
        return RuntimeInformation.FrameworkDescription;
    }

    /// <summary>
    /// Get the current processor architecture.
    /// </summary>
    /// <returns></returns>
    public static Architecture GetProcessorArchitecture()
    {
        return RuntimeInformation.ProcessArchitecture;
    }

    #endregion

    #region App Version Methods

    public static async Task<string> AppMajorVersionAsync(Assembly assembly)
    {
        var result = string.Empty;

        try
        {
            result = assembly.GetName().Version?.Major.ToString();
        }

        catch (Exception e)
        {
            await Console.Out.WriteLineAsync($"AppMajorVersion Exception: {e.Message}");
        }

        return result ?? string.Empty;
    }

    public static async Task<string> AppMinorVersionAsync(Assembly assembly)
    {
        var result = string.Empty;

        try
        {
            result = assembly.GetName().Version?.Minor.ToString();
        }

        catch (Exception e)
        {
            await Console.Out.WriteLineAsync($"AppMinorVersion Exception: {e.Message}");
        }

        return result ?? string.Empty;
    }

    public static async Task<string> AppBuildVersionAsync(Assembly assembly)
    {
        var result = string.Empty;

        try
        {
            result = assembly.GetName().Version?.Build.ToString();
        }

        catch (Exception e)
        {
            await Console.Out.WriteLineAsync($"AppBuildVersion Exception: {e.Message}");
        }

        return result ?? string.Empty;
    }

    public static async Task<string> AppRevisionVersionAsync(Assembly assembly)
    {
        var result = string.Empty;

        try
        {
            result = assembly.GetName().Version?.Revision.ToString();
        }

        catch (Exception e)
        {
            await Console.Out.WriteLineAsync($"AppRevisionVersion Exception: {e.Message}");
        }

        return result ?? string.Empty;
    }

    public static async Task<string> VersionAsync(Assembly assembly)
    {
        var result = string.Empty;

        try
        {
            result = await AppMajorVersionAsync(assembly) + "." + await AppMinorVersionAsync(assembly) + "." + await AppBuildVersionAsync(assembly);
        }

        catch (Exception e)
        {
            await Console.Out.WriteLineAsync($"Version Exception: {e.Message}");
        }

        return result;
    }

    #endregion

    #region Color

    /// <summary>
    /// Determine if a string is a valid web color (e.g. "#abc", "#abcf", "#abc123", "#112233ff", rgb(...), rgba(...), oklch(...), aliceblue, etc.)
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static bool IsValidWebColor(this string color)
    {
        if (string.IsNullOrEmpty(color))
            return false;
        
        #region Hex Color
        
        var hashIndex = color.IndexOf('#');

        if (hashIndex == 0)
        {
            if (color.Length is not (4 or 5 or 7 or 9))
                return false;

            for (var i = 1; i < color.Length; i++)
            {
                if ((color[i] >= '0' && color[i] <= '9') || (color[i] >= 'a' && color[i] <= 'f'))
                    continue;
                    
                return false;
            }

            return true;
        }

        #endregion

        #region RGB/a Color
        
        var rgbIndex = color.IndexOf("rgb", StringComparison.Ordinal);
        var indexOpen = color.IndexOf('(');
        var indexClose = color.LastIndexOf(')');

        if (rgbIndex == 0)
        {
            if (indexOpen < 0 || indexClose < 0 || indexClose <= indexOpen)
                return false;

            var segments = (color.Replace(" ", string.Empty).TrimStart("rgba(").TrimStart("rgb(")?.TrimEnd(')') ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);
                
            if (segments.Length is < 3 or > 4)
                return false;

            foreach (var segment in segments)
                if (decimal.TryParse(segment.Trim(), out _) == false)
                    return false;

            return true;
        }        

        #endregion

        #region OKLCH Color

        var oklchIndex = color.IndexOf("oklch", StringComparison.Ordinal);

        if (oklchIndex == 0)
        {
            if (indexOpen < 0 || indexClose < 0 || indexClose <= indexOpen)
                return false;

            var segments = (color.Replace("/", " / ").TrimStart("oklch(")?.TrimEnd(')') ?? string.Empty).Replace(',', ' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
            if (segments.Length != 3 && segments.Length != 5)
                return false;

            if (segments.Length == 5)
                if (double.TryParse(segments[4], out _) == false)
                    return false;

            if (double.TryParse(segments[0], out _) == false)
                return false;

            if (double.TryParse(segments[1], out _) == false)
                return false;
            
            return double.TryParse(segments[2], out _) != false;
        }
        
        #endregion
        
        return Strings.CssNamedColors.ContainsKey(color);
    }

    #endregion
}