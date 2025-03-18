// ReSharper disable ConvertToPrimaryConstructor

using Argentini.Sfumato.Entities;
using Argentini.Sfumato.Extensions;
using Xunit.Abstractions;

namespace Argentini.Sfumato.Tests;

public class ColorTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ColorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void IdentifyColor()
    {
        Assert.True("rgba(10,20,30,1.0)".IsValidWebColor());
        Assert.True("rgb(10,20,30)".IsValidWebColor());

        Assert.True("#aabbcc".IsValidWebColor());
        Assert.True("#aab".IsValidWebColor());
        Assert.True("#abcd".IsValidWebColor());

        Assert.True("aquamarine".IsValidWebColor());

        Assert.True("oklch(0.704 0.191 22.216)".IsValidWebColor());
        Assert.True("oklch(0.704 0.191 22.216 / 0.5)".IsValidWebColor());
    }

    [Fact]
    public void SetWebColorAlpha()
    {
        Assert.Equal("rgba(10,20,30,0.5)", "rgba(10,20,30,1.0)".SetWebColorAlpha(50));
        Assert.Equal("rgba(10,20,30,0.5)", "rgba(10,20,30,1.0)".SetWebColorAlpha(0.5));
        Assert.Equal("rgba(10,20,30,0.25)", "rgba(10,20,30,0.5)".SetWebColorAlpha(50));
        Assert.Equal("rgb(10,20,30)", "rgba(10, 20, 30, 0.5)".SetWebColorAlpha(100));

        Assert.Equal("rgba(170,187,204,0.5)", "#aabbcc".SetWebColorAlpha(0.5));
        Assert.Equal("rgba(170,170,187,0.5)", "#aab".SetWebColorAlpha(0.5));
        Assert.Equal("rgba(170,187,204,0.435)", "#abcd".SetWebColorAlpha(0.5));

        Assert.Equal("rgba(127,255,212,0.5)", "aquamarine".SetWebColorAlpha(0.5));

        Assert.Equal("oklch(0.704 0.191 22.216)", "oklch(0.704 0.191 22.216)".SetWebColorAlpha());
        Assert.Equal("oklch(0.704 0.191 22.216 / 0.5)", "oklch(0.704 0.191 22.216)".SetWebColorAlpha(50));
        Assert.Equal("oklch(0.704 0.191 22.216 / 0.5)", "oklch(0.704 0.191 22.216)".SetWebColorAlpha(0.5));
        Assert.Equal("oklch(0.704 0.191 22.216 / 0.25)", "oklch(0.704 0.191 22.216 / 0.5)".SetWebColorAlpha(0.5));
        Assert.Equal("oklch(0.704 0.191 22.216 / 0.25)", "oklch(0.704 0.191 22.216/0.5)".SetWebColorAlpha(0.5));
        Assert.Equal("oklch(0.704 0.191 22.216)", "oklch(0.704 0.191 22.216 / 0.5)".SetWebColorAlpha());
    }
}
