public static class WebPageHelpers
{
    public static async ValueTask<string> GetWebPageCardMarkupAsync(this GlobalStateService globalStateService, string url = "")
    {
        if (string.IsNullOrEmpty(url) || url.StartsWith('#'))
            return string.Empty;

        var metaData = globalStateService.RuntimeCache.GetCacheItem<MetaInformation>(url);

        if (metaData is null)
        {
            metaData = await GetMetaDataFromUrlAsync(url);
            globalStateService.RuntimeCache.InsertCacheItem(url, () => metaData, TimeSpan.FromMinutes(15)); // Cache responses to prevent remote website bans for too many consecutive calls
        }

        if (string.IsNullOrEmpty(metaData.Title))
            return string.Empty;
            
        var urlMetaTitle = metaData.Title.Truncate(500);
        var urlMetaImageUrl = metaData.ImageUrl.Truncate(500);
        var urlMetaIconUrl = metaData.IconUrl.Truncate(500);

        var title = string.IsNullOrEmpty(urlMetaTitle) == false ? urlMetaTitle : new Uri(url).Host.Truncate(100);
        var domainUrl = new Uri(url).Scheme + "://" + new Uri(url).Host;
        var previewImage = (string.IsNullOrEmpty(urlMetaImageUrl) == false ? urlMetaImageUrl : string.IsNullOrEmpty(urlMetaIconUrl) == false ? urlMetaIconUrl : "/themedimage/web-page-card/light").TrimStart("http:").TrimStart("https:");

        if (previewImage.StartsWith("//"))
            previewImage = $"https:{previewImage}";

        if (previewImage.StartsWith("http://") == false && previewImage.StartsWith("https://") == false)
            previewImage = $"https://{previewImage}";
        
        var markupBlock = """
<div class="max-md:-mx-5 md:rounded md:overflow-hidden">
""";

        markupBlock += $"""
    <a href="{url}" class="cursor-pointer" target="_blank" alt="{title.ForAttribute()}" title="{title.ForAttribute()}">
        <img src="{previewImage}" class="min-w-full" alt="{urlMetaTitle}">
    </a>
""";
            
        markupBlock += $"""
    <div class="px-6 py-5 bg-canvas-shade text-canvas-text text-pretty dark:bg-canvas">
        <div class="mb-4"><a href="{url}" class="text-xl/6 font-bold cursor-pointer hover:text-primary hover:underline" target="_blank" alt="{title.ForAttribute()}" title="{title.ForAttribute()}">{title}</a></div>
""";

        if (string.IsNullOrEmpty(urlMetaTitle) == false)
        {
            markupBlock += $"""
        <div><a href="{domainUrl}" class="cursor-pointer hover:text-primary hover:underline" target="_blank" alt="Visit {new Uri(url).Host.ForAttribute()}" title="Visit {new Uri(url).Host.ForAttribute()}"><i class="fad fa-globe mr-2"></i>{domainUrl}</a></div>
""";
        }

        markupBlock += """
    </div>
</div>
""";

        return markupBlock;
    }

    public static string GetAnchorNavMarkup(this IUmbracoContext umbracoContext, GlobalStateService globalStateService, SharedState sharedState, IWebsiteContent? content)
    {
        if (content is null)
            return string.Empty;

        var markup = globalStateService.StringBuilderPool.Get();

        try
        {
            markup.AppendLine("<div class=\"text-balance space-y-8\">");

            #region Root Block Anchors

            markup.AppendLine("<div class=\"space-y-4\">");

            foreach (var child in content.SafeBlocklistValue("blockContent").Where(c => c.Settings.HasValue("websiteAnchorText") && c.Settings.HasValue("websiteAnchorCategory") == false))
            {
                var childHash = child.Settings.SafeValue("websiteAnchorText").MakeSlug();

                markup.AppendLine($"<div><a href=\"#{childHash}\" class=\"cursor-pointer hover:underline hover:text-canvas-bold dark:hover:text-dark-canvas-bold\">{child.Settings.SafeValue("websiteAnchorText")}</a></div>");
            }

            markup.AppendLine("</div>");

            #endregion

            #region Utility Ref Anchors

            if (content.ContentType.Alias.Equals("utilityPage", StringComparison.OrdinalIgnoreCase))
            {
                var exportItems = sharedState.ExportItems.Where(i => i.Group == content.SafeValue("utilityGroup")).OrderBy(i => i.Name).ToList();

                markup.AppendLine("<div>");
                markup.AppendLine("<div class=\"mb-4 text-sm font-mono font-medium tracking-widest opacity-65 uppercase\">Quick Reference</div>");
                markup.AppendLine("<div class=\"border-l border-l-canvas-text/25 dark:border-l-dark-canvas-text/25 pl-5 space-y-4\">");

                foreach (var group in exportItems)
                {
                    var hash = group.Name.MakeSlug();

                    markup.AppendLine($"<div><a href=\"#{hash}\" class=\"cursor-pointer hover:underline hover:text-canvas-bold dark:hover:text-dark-canvas-bold\">{group.Name}</a></div>");
                }

                markup.AppendLine("</div>");
                markup.AppendLine("</div>");
            }

            #endregion

            #region Categorized Block Anchors
            
            foreach (var category in content.SafeBlocklistValue("blockContent").Where(c => c.Settings.HasValue("websiteAnchorCategory")).Select(c => c.Settings.SafeValue("websiteAnchorCategory")).Distinct(StringComparer.InvariantCultureIgnoreCase))
            {
                if (string.IsNullOrEmpty(category))
                    continue;
                
                markup.AppendLine("<div>");
                markup.AppendLine($"<div class=\"mb-4 text-sm font-mono font-medium tracking-widest opacity-65 uppercase\">{category}</div>");
                markup.AppendLine("<div class=\"border-l border-l-canvas-text/25 dark:border-l-dark-canvas-text/25 pl-5 space-y-4\">");

                foreach (var child in content.SafeBlocklistValue("blockContent").Where(c => c.Settings.SafeValue("websiteAnchorCategory").Equals(category, StringComparison.InvariantCultureIgnoreCase)))
                {
                    var childHash = child.Settings.SafeValue("websiteAnchorText").MakeSlug();

                    markup.AppendLine($"<div><a href=\"#{childHash}\" class=\"cursor-pointer hover:underline hover:text-canvas-bold dark:hover:text-dark-canvas-bold\">{child.Settings.SafeValue("websiteAnchorText")}</a></div>");
                }
                
                markup.AppendLine("</div>");
                markup.AppendLine("</div>");
            }

            #endregion

            markup.AppendLine("</div>");

            return markup.ToString();
        }
        finally
        {
            globalStateService.StringBuilderPool.Return(markup);
        }
    }
}
