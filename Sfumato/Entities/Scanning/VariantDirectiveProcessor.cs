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
        using var candidate = new CssClass(appRunner, selector: $"{expression}:[display:block]");

        if (candidate.IsValid == false || candidate.AllSegments.Count < 2)
            throw new InvalidDataException($"Unknown @variant '{expression}'.");

        var targetSelector = candidate.Selector.CssSelectorEscape();
        var selector = candidate.EscapedSelector.Replace(targetSelector, "&", StringComparison.Ordinal);
        var expanded = selector == "&" ? body : Wrap(selector, body, appRunner.AppRunnerSettings.LineBreak);
        var wrappers = candidate.Wrappers.Values.ToList();

        foreach (var segment in expression.SplitByTopLevel(':'))
            if (segment.SequenceEqual("hover"))
                wrappers.Add("@media (hover: hover) {");

        for (var index = wrappers.Count - 1; index >= 0; index--)
            expanded = Wrap(wrappers[index][..^1].TrimEnd(), expanded, appRunner.AppRunnerSettings.LineBreak);

        return expanded;
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
