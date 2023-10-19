using Argentini.Sfumato.Entities;
using Argentini.Sfumato.Extensions;
using Microsoft.Extensions.ObjectPool;

namespace Argentini.Sfumato.Tests;

public class SfumatoRunnerTests
{
    #region Constants

    public static string Markup => """
                                   <!DOCTYPE html>
                                   <html lang="en">
                                   <head>
                                       <meta charset="UTF-8">
                                       <title>Sample Website</title>
                                       <meta name="viewport" content="width=device-width, initial-scale=1">
                                       <link rel="stylesheet" href="css/sfumato.css">
                                   </head>
                                   <body class="text-base/5 desk:text-base/[3rem]">
                                       <div id="test-home" class="text-[1rem] note:text-[1.25rem] bg-fuchsia-500 dark:bg-fuchsia-300 dark:text-[length:1rem] desk:text-[#112233] desk:text-[red] desk:text-[--my-color-var] desk:text-[var(--my-color-var)]">
                                           <p class="[font-weight:900] tabp:[font-weight:900]">Placeholder</p>
                                           <p class="[fontweight:400] tabp:[fontweight:300] desk:text[#112233] desk:text-slate[#112233] desk:text-slate-50[#112233] desk:text-slate-50-[#112233]">Invalid Classes</p>
                                       </div>
                                       <div class="block invisible top-8 break-after-auto container aspect-screen elas:aspect-[8/4]"></div>
                                       <script>
                                           function test() {
                                             let el = document.getElementById('test-element');
                                             if (el) {
                                                   el.classList.add($`
                                                       bg-emerald-900
                                                       [font-weight:700]
                                                       tabl:[font-weight:700]
                                                   `);
                                                   el.classList.add(`bg-emerald-950`);
                                                   el.classList.add(`[font-weight:600]`);
                                                   el.classList.add(`note:[font-weight:600]`);
                                             }
                                           }
                                       </script>
                                   </body>
                                   </html>
                                   """;
    
    #endregion
    
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
    public async Task GenerateScssClassMarkup_Base()
    {
        var pool = new DefaultObjectPoolProvider().CreateStringBuilderPool();
        
        var result = await SfumatoRunner.GenerateScssClassMarkupAsync(new ScssClass("text-base")
        {
            Value = "1rem",
            ValueTypes = "length,percentage",
            Template = "font-size: {value};"
            
        }, pool, string.Empty);

        Assert.Equal(".text-base { font-size: 1rem; }", result.CompactCss());
    }

    [Fact]
    public async Task GenerateScssClassMarkup_Options()
    {
        var pool = new DefaultObjectPoolProvider().CreateStringBuilderPool();
        
        var result = await SfumatoRunner.GenerateScssClassMarkupAsync(new ScssClass("text-base/2")
        {
            Value = "1rem",
            ValueTypes = "length,percentage,number",
            Template = "font-size: {value}; line-height: 1.15rem;"            
            
        }, pool, string.Empty);

        Assert.Equal(".text-base\\/2 { font-size: 1rem; line-height: 1.15rem; }", result.CompactCss());
        
        result = await SfumatoRunner.GenerateScssClassMarkupAsync(new ScssClass("text-base/[3rem]")
        {
            Value = "1rem",
            ValueTypes = "length,percentage,number",
            Template = "font-size: {value}; line-height: 3rem;"            
            
        }, pool, string.Empty);

        Assert.Equal(".text-base\\/\\[3rem\\] { font-size: 1rem; line-height: 3rem; }", result.CompactCss());

        var scssClass = new ScssClass("tabp:text-base/[3rem]")
        {
            Value = "3rem",
            ValueTypes = "length,percentage,number",
            Template = "font-size: 1rem; line-height: {value};"
        };
        
        result = await SfumatoRunner.GenerateScssClassMarkupAsync(scssClass, pool, "tabp:");

        Assert.Equal(".tabp\\:text-base\\/\\[3rem\\] { font-size: 1rem; line-height: 3rem; }", result.CompactCss());
    }
    
    [Fact]
    public async Task GenerateScssClassMarkup_ArbitraryStyles()
    {
        var pool = new DefaultObjectPoolProvider().CreateStringBuilderPool();
        
        var scssClass = new ScssClass("[width:10rem]")
        {
            ValueTypes = "length,percentage",
            Value = "width:10rem",
            Template = "{value};"
        };
        
        var result = await SfumatoRunner.GenerateScssClassMarkupAsync(scssClass, pool, string.Empty);

        Assert.Equal(".\\[width\\:10rem\\] { width:10rem; }", result.CompactCss());
        
        scssClass = new ScssClass("tabp:[width:10rem]")
        {
            ValueTypes = "length,percentage",
            Value = "width:10rem",
            Template = "{value};"
        };
        
        result = await SfumatoRunner.GenerateScssClassMarkupAsync(scssClass, pool, "tabp:");

        Assert.Equal(".tabp\\:\\[width\\:10rem\\] { width:10rem; }", result.CompactCss());
        
        scssClass = new ScssClass("tabp:hover:[width:10rem]")
        {
            ValueTypes = "length,percentage",
            Value = "width:10rem",
            Template = "{value};"
        };
        
        result = await SfumatoRunner.GenerateScssClassMarkupAsync(scssClass, pool, "tabp:");

        Assert.Equal(".tabp\\:hover\\:\\[width\\:10rem\\] { &:hover { width:10rem; } }", result.CompactCss());
    }
    
    [Fact]
    public async Task ExamineMarkupForUsedClasses()
    {
        var runner = new SfumatoRunner();

        await runner.InitializeAsync();

        runner.AppState.ReleaseMode = true;
        runner.AppState.Settings.ThemeMode = "system";
        runner.AppState.ExamineMarkupForUsedClasses(Markup);
        
        Assert.Equal(24, runner.AppState.UsedClasses.Count);
    }

    [Fact]
    public async Task GenerateScssObjectTree()
    {
        var runner = new SfumatoRunner();

        await runner.InitializeAsync();

        runner.AppState.ReleaseMode = true;
        runner.AppState.Settings.ThemeMode = "system";
        runner.AppState.ExamineMarkupForUsedClasses(Markup);

        var scss = await runner.GenerateScssObjectTreeAsync();

        Assert.Equal("""

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

                     """.CompactCss(), scss.CompactCss());
    }
}
