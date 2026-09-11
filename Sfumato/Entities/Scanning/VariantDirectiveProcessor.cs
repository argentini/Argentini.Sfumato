using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Runners;

namespace Sfumato.Entities.Scanning;

public static class VariantDirectiveProcessor
{
    private const string Marker = "@variant";

    public static string Process(string css, AppRunner appRunner)
    {
        var output = appRunner.StringBuilderPool.Get();
        var position = 0;

        try
        {
            while (position < css.Length)
            {
                var markerIndex = css.IndexOf(Marker, position, StringComparison.Ordinal);

                if (markerIndex < 0)
                {
                    output.Append(css, position, css.Length - position);
                    break;
                }

                output.Append(css, position, markerIndex - position);

                var expressionStart = markerIndex + Marker.Length;
                var openingBrace = FindOpeningBrace(css, expressionStart);

                if (openingBrace < 0)
                {
                    output.Append(css, markerIndex, css.Length - markerIndex);
                    break;
                }

                var closingBrace = FindClosingBrace(css, openingBrace);

                if (closingBrace < 0)
                    throw new InvalidDataException("Unclosed @variant block.");

                var expression = css[expressionStart..openingBrace].Trim();
                var body = Process(css[(openingBrace + 1)..closingBrace], appRunner);

                output.Append(Expand(expression, body, appRunner));
                position = closingBrace + 1;
            }

            return output.ToString();
        }
        finally
        {
            appRunner.StringBuilderPool.Return(output);
        }
    }

    private static string Expand(string expression, string body, AppRunner appRunner)
    {
        var output = appRunner.StringBuilderPool.Get();
        var found = false;

        try
        {
            foreach (var alternativeSpan in expression.SplitByTopLevel(','))
            {
                var alternative = alternativeSpan.Trim().ToString();

                if (string.IsNullOrEmpty(alternative))
                    throw new InvalidDataException("Cannot use @variant with an empty variant.");

                if (found)
                    output.Append(appRunner.AppRunnerSettings.LineBreak);

                output.Append(ExpandAlternative(alternative, body, appRunner));
                found = true;
            }

            if (found == false)
                throw new InvalidDataException("Cannot use @variant without a variant.");

            return output.ToString();
        }
        finally
        {
            appRunner.StringBuilderPool.Return(output);
        }
    }

    private static string ExpandAlternative(string expression, string body, AppRunner appRunner)
    {
        var selector = "&";
        var wrappers = new List<string>();

        foreach (var segmentSpan in expression.SplitByTopLevel(':'))
        {
            var segment = segmentSpan.Trim().ToString();

            if (string.IsNullOrEmpty(segment))
                throw new InvalidDataException("Cannot stack an empty variant.");

            if (segment.StartsWith('[') && segment.EndsWith(']'))
            {
                selector = segment[1..^1].Replace("&", selector, StringComparison.Ordinal);
                continue;
            }

            if (appRunner.Library.PseudoclassPrefixes.TryGetValue(segment, out var pseudo))
            {
                selector = string.IsNullOrEmpty(pseudo.SelectorPrefix)
                    ? $"{selector}{pseudo.SelectorSuffix}"
                    : $"{pseudo.SelectorPrefix}{selector}{pseudo.SelectorSuffix}";

                if (segment == "hover")
                    wrappers.Add("@media (hover: hover)");

                continue;
            }

            if (TryGetWrapper(segment, appRunner, out var wrapper))
            {
                wrappers.Add(wrapper);
                continue;
            }

            throw new InvalidDataException($"Unknown @variant '{segment}'.");
        }

        var expanded = selector == "&" ? body : Wrap(selector, body, appRunner.AppRunnerSettings.LineBreak);

        for (var index = wrappers.Count - 1; index >= 0; index--)
            expanded = Wrap(wrappers[index], expanded, appRunner.AppRunnerSettings.LineBreak);

        return expanded;
    }

    private static bool TryGetWrapper(string segment, AppRunner appRunner, out string wrapper)
    {
        if (appRunner.Library.MediaQueryPrefixes.TryGetValue(segment, out var metadata))
        {
            wrapper = $"@media {metadata.Statement}";
            return true;
        }

        if (appRunner.Library.SupportsQueryPrefixes.TryGetValue(segment, out metadata))
        {
            wrapper = $"@supports {metadata.Statement}";
            return true;
        }

        if (appRunner.Library.ContainerQueryPrefixes.TryGetValue(segment, out metadata))
        {
            wrapper = $"@container {metadata.Statement}";
            return true;
        }

        if (appRunner.Library.StartingStyleQueryPrefixes.TryGetValue(segment, out metadata))
        {
            wrapper = "@starting-style";
            return true;
        }

        wrapper = string.Empty;
        return false;
    }

    private static string Wrap(string header, string body, string lineBreak)
    {
        return $"{header} {{{lineBreak}{body}{lineBreak}}}";
    }

    private static int FindOpeningBrace(string css, int start)
    {
        var bracketDepth = 0;
        var parenthesisDepth = 0;

        for (var index = start; index < css.Length; index++)
        {
            switch (css[index])
            {
                case '[':
                    bracketDepth++;
                    break;
                case ']':
                    if (bracketDepth > 0)
                        bracketDepth--;
                    break;
                case '(':
                    parenthesisDepth++;
                    break;
                case ')':
                    if (parenthesisDepth > 0)
                        parenthesisDepth--;
                    break;
                case '{' when bracketDepth == 0 && parenthesisDepth == 0:
                    return index;
            }
        }

        return -1;
    }

    private static int FindClosingBrace(string css, int openingBrace)
    {
        var depth = 1;

        for (var index = openingBrace + 1; index < css.Length; index++)
        {
            if (css[index] == '{')
                depth++;
            else if (css[index] == '}')
            {
                depth--;

                if (depth == 0)
                    return index;
            }
        }

        return -1;
    }
}
