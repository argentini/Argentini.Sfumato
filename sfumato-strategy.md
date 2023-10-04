# Sfumato Version 5

## Overview

1. Uses sfumato CLI for everything
2. Create 80+% of UI layout with classes
3. Build with sass; sfumato CLI watches, preprocesses, then calls sass CLI as-needed
4. The sass CLI handles CSS trimming, minification, mapping
5. Integrated JS helpers; uses selectors to engage (e.g. text truncation)
6. 

## Sfumato CLI
    
- Simple installation; once dotnet is installed, use `dotnet tool install` by publishing CLI app as a nuget package
- Embedded help
- Embedded resources (e.g. core SCSS)
- Installs dependencies (e.g. sass CLI)
- Can bootstrap a project
- Can update a project
- Can run once or watch
- Can recursively process root path or given paths
- Project root path defined by config file location (e.g. sfumato.json) or override
- Dynamic core SCSS extraction during builds
- Files with `@sfumato` directive will be processed
- SCSS transpiled in-place; override to combine all SCSS
- Structure core so class content can be injected via `@apply` in SCSS files
- Dark theme handled as explicit class or CSS `prefers-color-scheme`
- Color system with theme support
- 
