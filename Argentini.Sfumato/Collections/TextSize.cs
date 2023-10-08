namespace Argentini.Sfumato.Collections;

public sealed class TextSize
{
    public Dictionary<string, ScssClass> Classes { get; } = new ()
    {
        ["text-"] = new ScssClass
        {
            Value = "",
            ValueTypes = "length,percentage",
            Template = "font-size: {value};"
        },
        ["text-xs"] = new ScssClass
        {
            Value = "0.75rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        },
        ["text-sm"] = new ScssClass
        {
            Value = "0.875rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        },
        ["text-base"] = new ScssClass
        {
            Value = "1rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        },
        ["text-lg"] = new ScssClass
        {
            Value = "1.125rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        },
        ["text-xl"] = new ScssClass
        {
            Value = "1.25rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        },
        ["text-2xl"] = new ScssClass
        {
            Value = "1.5rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        },
        ["text-3xl"] = new ScssClass
        {
            Value = "1.875rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        },
        ["text-4xl"] = new ScssClass
        {
            Value = "2.25rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        },
        ["text-5xl"] = new ScssClass
        {
            Value = "3rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        },
        ["text-6xl"] = new ScssClass
        {
            Value = "3.75rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        },
        ["text-7xl"] = new ScssClass
        {
            Value = "4.5rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        },
        ["text-8xl"] = new ScssClass
        {
            Value = "6rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        },
        ["text-9xl"] = new ScssClass
        {
            Value = "8rem",
            ValueTypes = "length",
            Template = "font-size: {value};"
        }
    };
}