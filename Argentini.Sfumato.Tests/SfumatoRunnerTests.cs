using Argentini.Sfumato.Collections;
using Argentini.Sfumato.Entities;
using Microsoft.Extensions.ObjectPool;

namespace Argentini.Sfumato.Tests;

public class SfumatoRunnerTests
{
    [Fact]
    public void IsMediaQueryPrefix()
    {
        Assert.True(SfumatoRunner.IsMediaQueryPrefix("tabp"));
        Assert.True(SfumatoRunner.IsMediaQueryPrefix("dark"));
        Assert.False(SfumatoRunner.IsMediaQueryPrefix("hover"));
        Assert.False(SfumatoRunner.IsMediaQueryPrefix("focus"));
    }
    
    [Fact]
    public void IsPseudoclassPrefix()
    {
        Assert.False(SfumatoRunner.IsPseudoclassPrefix("tabp"));
        Assert.False(SfumatoRunner.IsPseudoclassPrefix("dark"));
        Assert.True(SfumatoRunner.IsPseudoclassPrefix("hover"));
        Assert.True(SfumatoRunner.IsPseudoclassPrefix("focus"));
    }
    
    [Fact]
    public async Task UsedClasses()
    {
        // var runner = new SfumatoRunner();
        //
        // runner.AppState.UsedClasses.Add("text-red-500", new ScssClass
        // {
        //     
        // });
        
        
        
        

    }
}
