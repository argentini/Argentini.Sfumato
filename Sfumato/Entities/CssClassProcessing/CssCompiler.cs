using Sfumato.Entities.Scanning;
using Sfumato.Entities.UtilityClasses;
using Sfumato.Validators;

namespace Sfumato.Entities.CssClassProcessing;

internal static class CssCompiler
{
    public static void Compile(CssClass candidate)
    {
        if (CssCandidateParser.TryResolveSimple(candidate))
            return;

        CssCandidateParser.ResolveArbitrary(candidate);

        if (candidate.AllSegments.Count == 0)
            return;

        if (candidate.IsValid == false && candidate.AllSegments[^1][0] != '[')
            CssUtilityResolver.Resolve(candidate);

        if (candidate.IsValid == false)
            return;

        if (candidate.ClassDefinition?.HasBehavior(UtilityBehaviors.RazorSyntax) ?? false)
            candidate.HasRazorSyntax = true;

        CssVariantResolver.Resolve(candidate);

        if (candidate.IsValid == false)
            return;

        CssRuleEmitter.GenerateSelector(candidate);
        CssRuleEmitter.GenerateWrappers(candidate);
    }
}

internal static class CssCandidateParser
{
    public static bool TryResolveSimple(CssClass candidate)
    {
        if (string.IsNullOrEmpty(candidate.Selector))
            return false;

        candidate.IsImportant = candidate.Selector[^1] == '!';

        if (candidate.Selector.IndexOf(':') > 0)
        {
            foreach (var segment in candidate.Selector.SplitByTopLevel(':'))
                candidate.AllSegments.Add(segment.ToString());

            return false;
        }

        var hasBrackets = candidate.Selector.IndexOfAny(['[', '(']) >= 0;

        candidate.AllSegments.Add(candidate.IsImportant ? candidate.Selector[..^1] : candidate.Selector);

        if (hasBrackets || candidate.AppRunner.Library.SimpleClasses.TryGetValue(candidate.AllSegments[0], out var definition) == false)
            return false;

        candidate.ClassDefinition = definition;
        candidate.IsValid = true;
        candidate.SelectorSort = definition.SelectorSort;

        if (definition.HasBehavior(UtilityBehaviors.RazorSyntax))
            candidate.HasRazorSyntax = true;

        CssRuleEmitter.GenerateSelector(candidate);
        CssRuleEmitter.GenerateStyles(candidate);

        return true;
    }

    public static void ResolveArbitrary(CssClass candidate)
    {
        try
        {
            if (candidate.AllSegments.Count == 0)
                return;

            if (candidate.AllSegments[^1].StartsWith('[') == false || candidate.AllSegments[^1].EndsWith(']') == false)
                return;

            var trimmedValue = candidate.AllSegments[^1].TrimStart('[').TrimEnd(']');
            var colonIndex = trimmedValue.IndexOf(':');

            if (colonIndex < 1 || colonIndex > trimmedValue.Length - 2)
                return;

            candidate.Styles = string.Empty;

            foreach (var span in trimmedValue.EnumerateCssCustomProperties())
            {
                candidate.IsCssCustomPropertyAssignment = true;
                candidate.IsValid = true;
                candidate.Styles += $"{span.Property}: {span.Value};".ProcessUnderscores();
            }

            if (candidate.IsValid == false && candidate.AppRunner.Library.CssPropertyNamesWithColons.HasPrefixIn(trimmedValue))
            {
                candidate.IsArbitraryCss = true;
                candidate.IsValid = true;
                candidate.Styles = $"{trimmedValue.ProcessUnderscores().TrimEnd(';')};";
            }

            if (candidate.IsValid == false || candidate.IsImportant == false)
                return;

            if (candidate.Styles.Contains("--", StringComparison.Ordinal) == false)
            {
                candidate.Styles = candidate.Styles.Replace(";", " !important;", StringComparison.Ordinal);
                return;
            }

            var splits = candidate.Styles.Split(';', StringSplitOptions.RemoveEmptyEntries);

            candidate.Styles = string.Empty;

            foreach (var split in splits)
            {
                if (split.Trim().StartsWith("--", StringComparison.Ordinal))
                    candidate.Styles += split.Trim() + ';' + candidate.AppRunner.AppRunnerSettings.LineBreak;
                else
                    candidate.Styles += split.Trim() + " !important;" + candidate.AppRunner.AppRunnerSettings.LineBreak;
            }

            candidate.Styles = candidate.Styles.Trim();
        }
        catch
        {
            candidate.IsValid = false;
        }
    }
}

internal static class CssUtilityResolver
{
    public static void Resolve(CssClass candidate) => candidate.ResolveUtilityCore();
}

internal static class CssVariantResolver
{
    public static void Resolve(CssClass candidate)
    {
        try
        {
            if (candidate.AllSegments.Count <= 1)
                return;

            for (var index = 0; index < candidate.AllSegments.Count - 1; index++)
            {
                var segment = candidate.AllSegments[index];

                if (string.IsNullOrEmpty(segment))
                    return;

                if (segment.TryGetVariant(candidate.AppRunner, out var metadata) == false || metadata is null)
                {
                    candidate.IsValid = false;
                    return;
                }

                if (candidate.HasRazorSyntax == false && metadata.IsRazorSyntax)
                    candidate.HasRazorSyntax = true;

                candidate.VariantSegments.TryAdd(segment, metadata);

                if (metadata.PrefixType[0] != 'p')
                    continue;

                if (metadata.PrioritySort > 0)
                    candidate.SelectorSort += metadata.PrioritySort;
                else
                    candidate.SelectorSort++;
            }
        }
        catch
        {
            candidate.IsValid = false;
        }
    }
}

internal static class CssRuleEmitter
{
    public static void GenerateSelector(CssClass candidate) => candidate.GenerateSelectorCore();

    public static void GenerateStyles(CssClass candidate, bool useArbitraryValue = false) => candidate.GenerateStylesCore(useArbitraryValue);

    public static void GenerateWrappers(CssClass candidate)
    {
        if (candidate.IsValid)
            candidate.GenerateWrappersCore();
    }
}
