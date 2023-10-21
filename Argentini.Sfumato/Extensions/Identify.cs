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
            return result.Right("Version=").TrimStart(new[] { 'v' });

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
    /// Determine if a string is a valid web hex color (e.g. "#abc", "#abcf", "#abc123", "#112233ff").
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static bool IsValidWebHexColor(this string color)
    {
        if (string.IsNullOrEmpty(color) || color.Length < 4)
            return false;

        if (color[0] != '#')
            return false;

        // Get the length excluding the '#'
        var length = color.Length - 1;

        // Ensure the length is 3, 6, 9, or 12
        if (length != 3 && length != 4 && length != 6 && length != 8)
            return false;

        for (var i = 1; i < color.Length; i++)
        {
            var c = color[i];

            if (c is >= '0' and <= '9' or >= 'a' and <= 'f' or >= 'A' and <= 'F' == false)
                return false;
        }

        return true;
    }

    #endregion
}