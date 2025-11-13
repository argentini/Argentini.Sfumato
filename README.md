# Sfumato: The Ultra-Fast CSS Generation Tool

Sfumato is a command line interface (CLI) tool that generates CSS for your web-based projects. You can create HTML markup and use pre-defined utility class names to style the rendered markup, without actually writing any CSS code or leaving your HTML editor!

The resulting HTML markup is clean, readable, and consistent. And the generated CSS file is tiny, even if it hasn't been minified!

Some of the benefits of using Sfumato over Tailwind include:

- Single, easy installation; Install once and use Sfumato on any projects now and in the future.
- Fastest way to build; Sfumato is a multi-threaded, 64-bit native, super fast cross-platform app. It's up to 2-3x faster than Tailwind CSS 4.1!
- No dependencies; When using Sfumato your project remains clean. Tailwind uses Node.js with a bunch of third party dependencies and adds a node-modules folder to your project containing a ton of extra files.
- Multi-project builds; Sfumato can build and watch multiple project configurations at once. It's so fast you may not even notice a difference.
- Works great with ASP.NET; Sfumato supports ASP.NET, Blazor, and other Microsoft stack projects by handling "@" escapes in razor/cshtml markup files. So you can use container utilities like "@container" by escaping them in razor syntax (e.g. "@@container").
- Imported CSS files work as-is; Sfumato features can be used in imported CSS files without any modifications. It just works. Tailwind's Node.js pipeline requires additional changes to be made in imported CSS files that use Tailwind features and setup is finicky.
- Better dark theme support; Unlike Tailwind, Sfumato allows you to provide "system", "light", and "dark" options in your web app without writing any JavaScript code (other than widget UI code).
- Adaptive design baked in; In addition to the standard media breakpoint variants (e.g. sm, md, lg, etc.) Sfumato has adaptive breakpoints that use viewport aspect ratio for better device identification (e.g. mobi, tabp, tabl, desk, etc.).
- Integrated form element styles; Sfumato includes form field styles that are class name compatible with the Tailwind forms plugin.
- More colorful; The Sfumato color library provides 20 shade steps per color (values of 50-1000 in increments of 50).
- More compact CSS; Sfumato combines media queries (like dark theme styles), reducing the size of the generated CSS even without minification.
- Workflow-friendly; Sfumato supports redirected input for use in automation workflows.

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
