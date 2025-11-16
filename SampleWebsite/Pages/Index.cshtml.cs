using Microsoft.AspNetCore.Mvc.RazorPages;
// ReSharper disable UnusedMember.Global

namespace SampleWebsite.Pages;

public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
    public ILogger<IndexModel> Logger { get; } = logger;

    public void OnGet()
    {
    }
}