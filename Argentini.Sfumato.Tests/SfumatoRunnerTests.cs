using System.Text;
using System.Text.RegularExpressions;
using Argentini.Sfumato.Entities;
using Argentini.Sfumato.Extensions;
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
    public async Task GenerateScssClassMarkupAsync()
    {
        var pool = new DefaultObjectPoolProvider().CreateStringBuilderPool();

        var result = await SfumatoRunner.GenerateScssClassMarkupAsync(new ScssClass
        {
            RootClassName = "text-",
            UserClassName = "text-1.0",
            Value = "1rem",
            ValueTypes = "length,percentage",
            Template = "font-size: {value};"
            
        }, pool, string.Empty);

        Assert.Equal(".text-1\\.0{\nfont-size:1rem;\n}", result.Trim().Replace(" ", string.Empty));
        
        result = await SfumatoRunner.GenerateScssClassMarkupAsync(new ScssClass
        {
            RootClassName = "text-base/2",
            UserClassName = "text-base/2",
            Value = "1rem",
            ValueTypes = "length,percentage,number",
            Template = "font-size: {value}; line-height: 1.15rem;"            
            
        }, pool, string.Empty);

        Assert.Equal(".text-base\\/2{\nfont-size:1rem;line-height:1.15rem;\n}", result.Trim().Replace(" ", string.Empty));
        
        result = await SfumatoRunner.GenerateScssClassMarkupAsync(new ScssClass
        {
            RootClassName = "text-base/[3rem]",
            UserClassName = "text-base/[3rem]",
            Value = "1rem",
            ValueTypes = "length,percentage,number",
            Template = "font-size: {value}; line-height: 3rem;"            
            
        }, pool, string.Empty);

        Assert.Equal(".text-base\\/\\[3rem\\]{\nfont-size:1rem;line-height:3rem;\n}", result.Trim().Replace(" ", string.Empty));
    }
    
    [Fact]
    public async Task UsedClasses()
    {
        var runner = new SfumatoRunner();

        await runner.InitializeAsync();

        runner.AppState.ReleaseMode = true;
        runner.AppState.Settings.ThemeMode = "system";
        runner.AppState.ExamineMarkupForUsedClasses(SfumatoAppStateTests.Markup);
        
        Assert.Equal(24, runner.AppState.UsedClasses.Count);

        var scss = await runner.GenerateScssObjectTreeAsync();

        Assert.Equal(@"
.\[font-weight\:600\] {
    font-weight:600;
}
.\[font-weight\:700\] {
    font-weight:700;
}
.\[font-weight\:900\] {
    font-weight:900;
}
.bg-emerald-900 {
    background-color: rgb(6, 78, 59);
}
.bg-emerald-950 {
    background-color: rgb(2, 44, 34);
}
.bg-fuchsia-500 {
    background-color: rgb(217, 70, 239);
}
.aspect-screen {
    aspect-ratio: 4/3;
}
.container {
    width: 100%;
    
    @include sf-media($from: phab) {
       max-width: $phab-breakpoint;
    }
    
    @include sf-media($from: tabp) {
       max-width: $tabp-breakpoint;
    }
    
    @include sf-media($from: tabl) {
       max-width: $tabl-breakpoint;
    }
    
    @include sf-media($from: note) {
       max-width: $note-breakpoint;
    }
    
    @include sf-media($from: desk) {
       max-width: $desk-breakpoint;
    }
    
    @include sf-media($from: elas) {
       max-width: $elas-breakpoint;
    }
}
.break-after-auto {
    break-after: auto;
}
.block {
    display: block;
}
.top-8 {
    top: 2rem;
}
.invisible {
    visibility: hidden;
}
.text-\[1rem\] {
    font-size: 1rem;
}
.text-base\/5 {
    font-size: 1rem; line-height: 1.25rem;
}
@include sf-media($from: tabp) {
    .tabp\:\[font-weight\:900\] {
        font-weight:900;
    }
}
@include sf-media($from: tabl) {
    .tabl\:\[font-weight\:700\] {
        font-weight:700;
    }
}
@include sf-media($from: note) {
    .note\:\[font-weight\:600\] {
        font-weight:600;
    }
    .note\:text-\[1\.25rem\] {
        font-size: 1.25rem;
    }
}
@include sf-media($from: desk) {
    .desk\:text-base\/\[3rem\] {
        font-size: 1rem; line-height: 3rem;
    }
    .desk\:text-\[\#112233\] {
        color: #112233;
    }
    .desk\:text-\[red\] {
        color: red;
    }
}
@include sf-media($from: elas) {
    .elas\:aspect-\[8\/4\] {
        aspect-ratio: 8/4;
    }
}
@media (prefers-color-scheme: dark) {
    .dark\:bg-fuchsia-300 {
        background-color: rgb(240, 171, 252);
    }
    .dark\:text-\[length\:1rem\] {
        font-size: 1rem;
    }
}
".Trim().NormalizeLinebreaks(), scss.Trim().NormalizeLinebreaks());
    }
}
