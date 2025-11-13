# Sfumato: The Ultra-Fast CSS Generation Tool

Sfumato is a lean, modern, utility-based CSS framework with relative UI scaling and adaptive design built-in. Add few lines to your CSS file and Sfumato will watch your project as you work, keeping track of changes, and generate a custom, tiny CSS file based only on the utility classes you use. And Sfumato uses the same class naming convention as Tailwind CSS v4!

- The Sfumato CLI tool is written in cross-platform (multi-threaded) native code, not javascript, and is much faster than Tailwind
- Sfumato provides an optional scalable CSS system that makes all the viewport sizes between breakpoints scale like a PDF for a more controlled layout
- Dark theme mode that supports system theme matching, as well as classes that include an "auto" class to fall back to system matching
- Integrated form element styles (class compatible with Tailwind forms plugin)
- One install works for all your projects!

## Installation

### 1. Install Microsoft .NET

Sfumato requires that you already have the .NET 10 runtime installed, which you can get at [https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download).

### 2. Install Sfumato

Run the following command in your command line interface (e.g. cmd, PowerShell, Terminal, bash, etc.):

```dotnet tool install --global argentini.sfumato```

Later you can update Sfumato with the following command:

```dotnet tool update --global argentini.sfumato```

## How To Use

Use the following command for more information on editing your CSS file and using Sfumato commands and options:

```sfumato help```

## Uninstall

If you need to completely uninstall Sfumato, use the command below:

```dotnet tool uninstall --global argentini.sfumato```
