# Sfumato: The Ultra-Fast CSS Generation Tool

Sfumato is a lean, modern, utility-based CSS framework with relative UI scaling and adaptive design built-in. Add few lines to your CSS file and Sfumato will watch your project as you work, keeping track of changes, and generate a custom, tiny CSS file based only on the utility classes you use. And Sfumato uses the same class naming convention as Tailwind CSS v4!

- Sfumato Core is written in cross-platform (multi-threaded) native code, not javascript, and is much faster than Tailwind
- Sfumato provides an optional scalable CSS system that makes all the viewport sizes between breakpoints scale like a PDF for a more controlled layout
- Dark theme mode that supports system theme matching, as well as classes that include an "auto" class to fall back to system matching
- Integrated form element styles (class compatible with Tailwind forms plugin)
- Add to your project via nuget and Sfumato generates CSS as you make changes during debug

## Installation

### 1. Install Microsoft .NET

Sfumato requires that you already have the .NET 10 runtime installed, which you can get at [https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download).

### 2. Add Sfumato

Add the `argentini.sfumato.core` nuget package.

## How To Use

Install and configure Sfumato Core in your ASP.NET project.
