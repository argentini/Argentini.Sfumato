using System.Globalization;
using Sfumato.Entities.CssClassProcessing;
using Sfumato.Entities.Runners;
using Sfumato.Validators;

namespace Sfumato.Entities.UtilityClasses;

public sealed class FunctionalUtilityDefinition(string name, string body)
{
    public string Name { get; } = name;
    public string Body { get; } = body;
    public bool IsValid { get; } = body.Contains("--value(", StringComparison.Ordinal);

    public bool TryResolve(string suffix, AppRunner appRunner, out string styles)
    {
        ParseSuffix(suffix, out var value, out var valueIsArbitrary, out var modifier, out var modifierIsArbitrary);

        var output = appRunner.StringBuilderPool.Get();
        var state = new ResolutionState
        {
            HasValue = string.IsNullOrEmpty(value) == false,
            HasModifier = string.IsNullOrEmpty(modifier) == false,
        };

        try
        {
            foreach (var declarationSpan in Body.SplitByTopLevel(';'))
            {
                var declaration = declarationSpan.Trim().ToString();

                if (string.IsNullOrEmpty(declaration))
                    continue;

                if (TryResolveFunctions(declaration, "--value(", value, valueIsArbitrary, appRunner, ref state, false, out declaration) == false)
                    continue;

                if (TryResolveFunctions(declaration, "--modifier(", modifier, modifierIsArbitrary, appRunner, ref state, true, out declaration) == false)
                    continue;

                if (output.Length > 0)
                    output.Append(appRunner.AppRunnerSettings.LineBreak);

                output.Append(declaration).Append(';');
            }

            if ((state.HasValue && state.ResolvedValue == false) || (state.HasModifier && state.ResolvedModifier == false) || output.Length == 0)
            {
                styles = string.Empty;
                return false;
            }

            styles = output.ToString();
            return true;
        }
        finally
        {
            appRunner.StringBuilderPool.Return(output);
        }
    }

    private static void ParseSuffix(string suffix, out string value, out bool valueIsArbitrary, out string modifier, out bool modifierIsArbitrary)
    {
        value = suffix;
        modifier = string.Empty;

        var segments = new List<string>(2);

        foreach (var segment in suffix.SplitByTopLevel('/'))
            segments.Add(segment.ToString());

        if (segments.Count == 2)
        {
            value = segments[0];
            modifier = segments[1];
        }

        value = value.TrimStart('-');
        valueIsArbitrary = value.StartsWith('[') && value.EndsWith(']');
        modifierIsArbitrary = modifier.StartsWith('[') && modifier.EndsWith(']');

        if (valueIsArbitrary)
            value = value[1..^1].ProcessUnderscores();
        else if (value.StartsWith('(') && value.EndsWith(')'))
            value = $"var({value[1..^1]})";

        if (modifierIsArbitrary)
            modifier = modifier[1..^1].ProcessUnderscores();
        else if (modifier.StartsWith('(') && modifier.EndsWith(')'))
            modifier = $"var({modifier[1..^1]})";
    }

    private static bool TryResolveFunctions(
        string source,
        string marker,
        string candidate,
        bool arbitrary,
        AppRunner appRunner,
        ref ResolutionState state,
        bool modifier,
        out string result)
    {
        var searchStart = 0;
        var output = appRunner.StringBuilderPool.Get();

        try
        {
            while (true)
            {
                var functionStart = source.IndexOf(marker, searchStart, StringComparison.Ordinal);

                if (functionStart < 0)
                {
                    output.Append(source, searchStart, source.Length - searchStart);
                    result = output.ToString();
                    return true;
                }

                output.Append(source, searchStart, functionStart - searchStart);

                var argumentsStart = functionStart + marker.Length;
                var functionEnd = FindClosingParenthesis(source, argumentsStart);

                if (functionEnd < 0)
                {
                    result = string.Empty;
                    return false;
                }

                var arguments = source[argumentsStart..functionEnd];

                if (TryResolveArguments(arguments, candidate, arbitrary, appRunner, out var resolved) == false)
                {
                    result = string.Empty;
                    return false;
                }

                if (modifier)
                    state.ResolvedModifier = true;
                else
                    state.ResolvedValue = true;

                output.Append(resolved);
                searchStart = functionEnd + 1;
            }
        }
        finally
        {
            appRunner.StringBuilderPool.Return(output);
        }
    }

    private static int FindClosingParenthesis(string source, int start)
    {
        var depth = 1;

        for (var index = start; index < source.Length; index++)
        {
            if (source[index] == '(')
                depth++;
            else if (source[index] == ')')
            {
                depth--;

                if (depth == 0)
                    return index;
            }
        }

        return -1;
    }

    private static bool TryResolveArguments(string arguments, string candidate, bool arbitrary, AppRunner appRunner, out string value)
    {
        foreach (var argumentSpan in arguments.SplitByTopLevel(','))
        {
            var argument = argumentSpan.Trim().ToString();

            if (string.IsNullOrEmpty(candidate))
            {
                if (argument.StartsWith("--default(", StringComparison.Ordinal) && argument.EndsWith(')'))
                {
                    value = argument[10..^1].Trim();
                    return string.IsNullOrEmpty(value) == false;
                }

                continue;
            }

            if (argument.StartsWith("--default(", StringComparison.Ordinal))
                continue;

            if (argument.Length > 1 && argument[0] is '\'' or '"' && argument[^1] == argument[0])
            {
                var literal = argument[1..^1];

                if (candidate == literal)
                {
                    value = literal;
                    return true;
                }

                continue;
            }

            if (argument.StartsWith("--", StringComparison.Ordinal))
            {
                var customProperty = argument.EndsWith('*')
                    ? $"{argument[..^1]}{candidate}"
                    : $"{argument}-{candidate}";

                if (appRunner.AppRunnerSettings.SfumatoBlockItems.ContainsKey(customProperty))
                {
                    value = $"var({customProperty})";
                    return true;
                }

                continue;
            }

            if (MatchesDataType(candidate, argument, arbitrary, appRunner))
            {
                value = candidate;
                return true;
            }
        }

        value = string.Empty;
        return false;
    }

    private static bool MatchesDataType(string candidate, string dataType, bool arbitrary, AppRunner appRunner)
    {
        var requiresArbitrary = dataType.StartsWith('[') && dataType.EndsWith(']');

        if (requiresArbitrary != arbitrary)
            return false;

        var type = requiresArbitrary ? dataType[1..^1] : dataType;

        if (type is "*" or "number")
            return double.TryParse(candidate, NumberStyles.Float, CultureInfo.InvariantCulture, out var number) && number >= 0;

        if (type == "integer")
            return int.TryParse(candidate, NumberStyles.Integer, CultureInfo.InvariantCulture, out var integer) && integer >= 0;

        if (type == "percentage")
            return candidate.ValueIsPercentage();

        if (type == "ratio")
            return candidate.ValueIsRatio();

        if (type is "length" or "dimension")
            return candidate.ValueIsDimensionLength(appRunner);

        if (type == "color")
            return candidate.IsValidWebColor();

        return requiresArbitrary;
    }

    private struct ResolutionState
    {
        public bool HasValue;
        public bool HasModifier;
        public bool ResolvedValue;
        public bool ResolvedModifier;
    }
}
