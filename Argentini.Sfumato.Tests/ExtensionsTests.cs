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
}
