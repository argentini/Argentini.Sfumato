using Argentini.Sfumato.Extensions;

namespace Argentini.Sfumato.Tests;

public class ExtensionsTests
{
    [Fact]
    public void FormatTimer()
    {
        var timer = TimeSpan.FromMilliseconds(0);
        
        Assert.Equal("0.000s", timer.FormatTimer());

        timer = TimeSpan.FromMilliseconds(125);
        
        Assert.Equal("0.125s", timer.FormatTimer());
        
        timer = TimeSpan.FromSeconds(1);
        
        Assert.Equal("1.000s", timer.FormatTimer());
        
        timer = TimeSpan.FromSeconds(1.5);
        
        Assert.Equal("1.500s", timer.FormatTimer());

        timer = TimeSpan.FromSeconds(59.999);
        
        Assert.Equal("59.999s", timer.FormatTimer());
        
        timer = TimeSpan.FromSeconds(70);
        
        Assert.Equal("01m:10.000s", timer.FormatTimer());

        timer = TimeSpan.FromSeconds(70);
        timer = timer.Add(TimeSpan.FromHours(2));
        
        Assert.Equal("02h:01m:10.000s", timer.FormatTimer());
        
        timer = TimeSpan.FromSeconds(70);
        timer = timer.Add(TimeSpan.FromHours(2));
        timer = timer.Add(TimeSpan.FromDays(7));
        
        Assert.Equal("07d:02h:01m:10.000s", timer.FormatTimer());
    }

    [Fact]
    public void ColorConversion()
    {
        #region Invalid colors

        var color = string.Empty;
        Assert.Equal("rgba(0,0,0,-0)", color.WebColorToRgba());
        
        color = "#";
        Assert.Equal("rgba(0,0,0,-0)", color.WebColorToRgba());

        color = "#abcde";
        Assert.Equal("rgba(0,0,0,-0)", color.WebColorToRgba());

        color = "#ab";
        Assert.Equal("rgba(0,0,0,-0)", color.WebColorToRgba());
        
        color = "rgba(50,100,200,300,400)";
        Assert.Equal("rgba(0,0,0,-0)", color.WebColorToRgba());

        color = "rgb(50,100,200,300,400)";
        Assert.Equal("rgba(0,0,0,-0)", color.WebColorToRgba());

        color = "rgba(50,100)";
        Assert.Equal("rgba(0,0,0,-0)", color.WebColorToRgba());
        
        color = "rgb(50,100)";
        Assert.Equal("rgba(0,0,0,-0)", color.WebColorToRgba());

        color = "nbasvfbnasdfvasfsvl,hjfvasdjlfvs,vcsvbdhj";
        Assert.Equal("rgba(0,0,0,-0)", color.WebColorToRgba());
        
        #endregion

        #region Convert as-is
        
        color = "#fff";
        Assert.Equal("rgba(255,255,255,1)", color.WebColorToRgba());

        color = "#0000";
        Assert.Equal("rgba(0,0,0,0)", color.WebColorToRgba());

        color = "#fff0";
        Assert.Equal("rgba(255,255,255,0)", color.WebColorToRgba());
        
        color = "#000000";
        Assert.Equal("rgba(0,0,0,1)", color.WebColorToRgba());
        
        color = "#000000ff";
        Assert.Equal("rgba(0,0,0,1)", color.WebColorToRgba());
        
        color = "#00000000";
        Assert.Equal("rgba(0,0,0,0)", color.WebColorToRgba());
        
        color = "rgb(50, 100, 200)";
        Assert.Equal("rgba(50,100,200,1)", color.WebColorToRgba());
        
        color = "rgb(50,100,200)";
        Assert.Equal("rgba(50,100,200,1)", color.WebColorToRgba());
        
        color = "rgba(50,100,200)";
        Assert.Equal("rgba(50,100,200,1)", color.WebColorToRgba());
        
        color = "rgba(50,100,200,0.5)";
        Assert.Equal("rgba(50,100,200,0.5)", color.WebColorToRgba());

        color = "transparent";
        Assert.Equal("rgba(0,0,0,0)", color.WebColorToRgba());

        color = "aliceblue";
        Assert.Equal("rgba(240,248,255,1)", color.WebColorToRgba());
        
        #endregion
        
        #region Convert with new opacity
        
        color = "#fff";
        Assert.Equal("rgba(255,255,255,0.25)", color.WebColorToRgba(25));

        color = "#fff0";
        Assert.Equal("rgba(255,255,255,0.25)", color.WebColorToRgba(0.25));
        
        color = "#000000";
        Assert.Equal("rgba(0,0,0,0.75)", color.WebColorToRgba(0.75m));
        
        color = "#000000ff";
        Assert.Equal("rgba(0,0,0,1)", color.WebColorToRgba(200));
        
        color = "#00000000";
        Assert.Equal("rgba(0,0,0,1)", color.WebColorToRgba(2.0m));
        
        color = "rgb(50, 100, 200)";
        Assert.Equal("rgba(50,100,200,0.75)", color.WebColorToRgba(75));
        
        color = "rgb(50,100,200)";
        Assert.Equal("rgba(50,100,200,0.75)", color.WebColorToRgba(0.75));
        
        color = "rgba(50,100,200)";
        Assert.Equal("rgba(50,100,200,0.5)", color.WebColorToRgba(0.5m));
        
        color = "rgba(50,100,200,0.5)";
        Assert.Equal("rgba(50,100,200,0.03)", color.WebColorToRgba(3));

        #endregion
    }
}
