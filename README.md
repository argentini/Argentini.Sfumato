# SFUMATO

Sfumato is a lean, modern, utility-based CSS framework generation tool. It is compatible with the Tailwind CSS class naming structure and has the following additional features:

- The Sfumato CLI tool is written in cros-platform native code, not javascript, and is very fast
- Dart Sass (also cross-platform native code) is embedded so you get all the benefits of using Sass as part of your workflow
- Sfumato uses a scalable CSS system that makes all the viewport sizes between breakpoints scale like a PDF for a more controlled layout
- Theme mode that supports system theme matching, as well as classes that include an "auto" class to fall back to system matching
- Integrated form element styles (class compatible with Tailwind forms plugin)

## How To Use

Create one simple "sfumato.yml" file (manually or using the Sfumato "init" command) for your web-based app or website project and run the Sfumato CLI "watch" command. It will watch your project files as you work, keeping track of your markup changes, and will transpile your SCSS files into custom, tiny CSS files based only on the Sfumato features you use.

Use the following command for more information on Sfumato commands and options:

```sfumato help```

## Installation

### 1. Install Microsoft .NET

Sfumato requires that you already have the .NET 8.0 runtime installed, which you can get at [https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download).

### 2. Install Sfumato

Run the following command in your command line interface (e.g. cmd, PowerShell, Terminal, bash, etc.):

```dotnet tool install --global argentini.sfumato```

Later you can update Sfumato with the following command:

```dotnet tool update --global argentini.sfumato```

## Uninstall

If you need to completely uninstall Sfumato, use the command below:

```dotnet tool uninstall --global argentini.sfumato```
