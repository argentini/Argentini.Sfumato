namespace UmbracoCms.Extensions;

public static class ContentExtensions
{
    public static string BulletsToChecks(this string value)
    {
        return value
            .Replace("<ul>", "<div class=\"grid grid-cols-[min-content_1fr] gap-x-3 gap-y-3\">")
            .Replace("</ul>", "</div>")
            .Replace("<li>", "<div class=\"mt-0!\"><i class=\"fa-regular fa-check text-primary\"></i></div><div class=\"mt-0!\">")
            .Replace("</li>", "</div>");
    }
}