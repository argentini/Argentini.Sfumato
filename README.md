# SFUMATO

Sfumato is a lean, modern, utility-based CSS framework generation tool. It is compatible with the Tailwind CSS class structure and has the following additional features:

- The Sfumato CLI tool is written in cros-platform native code, not javascript, and is very fast
- Dart Sass (also cross-platform native code) is embedded so you get all the benefits of using Sass as part of your workflow
- Sfumato uses a scalable CSS system that makes all the viewport sizes between breakpoints scale like a PDF for a more controlled layout

## How To Use

Create one simple "sfumato.json" file for your web-based app or website project and run the Sfumato CLI command. It will watch your project files as you work, keeping track of your markup changes, and generating a custom, tiny CSS library based only on the Sfumato features you use. Any watched `*.scss` files can use Sfumato helpers, like the media breakpoint mixin, are transpiled in-place as part of the build.

## Installation

Install dotnet 7 or later from [https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download) and then install Sfumato with the following command:

```dotnet tool install --global argentini.sfumato```

Later you can update Sfumato with the following command:

```dotnet tool update --global argentini.sfumato```
