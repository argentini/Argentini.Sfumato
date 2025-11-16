using Microsoft.AspNetCore.Mvc.RazorPages;
// ReSharper disable UnusedMember.Global

namespace SampleWebsite.Pages;

public class PrivacyModel(ILogger<PrivacyModel> logger) : PageModel
{
    public ILogger<PrivacyModel> Logger { get; } = logger;

    public void OnGet()
    {
    }
}