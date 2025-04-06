Sfumato 6 Technical Strategy
============================

To Do
=====

1. Refactor to leverage CSS custom properties/namespaces below
2. Add all utility classes
3. Create code to iterate, group, wrap classes, generate actual CSS (perhaps create a list of segments based on like media queries, then stack them in the final CSS)
4. Remove unused properties across all entities





Architecture
============

New Features
------------

- Class-compatible with Tailwind CSS v4
- Even faster performance!
- CSS-only; no more Sass; custom CSS minifier
- CSS custom properties drives (almost) everything
- Breakpoint names can be used as measurements (e.g. max-w-md)
- Support relevant Tailwind v4 functions and directives
- max for inline arbitrary breakpoints (e.g. max-[40rem])
- Colors use oklab() for wider gamut
- More maintainable and extensible project structure
- Container query support
- Support multiple projects; single YML file to generate multi-project CSS
- Add more complex features like arbitrary variants (e.g. [&.active]:bg-red)

C# Entities
-----------

- PathWatcher[]; provide path and file info to watch for file changes (watch mode)
- ScssClass; individual utility class object that is parsed and provides methods for generating classes, styles, and values
- Scss; collection of ScssClass objects with methods for consolidation and generation of final CSS
- Library; collection of supported utility classes with capabilities and templates; can be used to generate documentation data

Scanning
--------

- Simpler regex with listed prefixes; use multiple regex passes to avoid complex regex strings.
- Scan includes CSS custom property prefixes so when they're used we can ensure they're included in the CSS
- Regex is dynamic in that defined breakpoints, states, etc. are included

Variants Order
--------------

1. Dark Mode Variants (dark, light)
2. Breakpoint Variants (sm, md, lg, xl, 2xl, etc.)
3. Variants (group, peer, etc.)
4. Custom variants ([&.active], etc.)
5. Container queries
6. State Variants (hover, focus, active, etc.)
7. Logical Variants (ltr, rtl, first, last, even, odd, etc.)

Themes
======

Theme Variable Namespaces
-------------------------

https://tailwindcss.com/docs/functions-and-directives

https://tailwindcss.com/docs/theme#theme-variable-namespaces

https://github.com/tailwindlabs/tailwindcss/blob/main/packages/tailwindcss/

Namespace	        Utility Classes

--color-*	      Color utilities like bg-red-500, text-sky-300, and many more
--font-*	      Font family utilities like font-sans
--text-*	      Font size utilities like text-xl
--font-weight-*   Font weight utilities like font-bold
--tracking-*	  Letter spacing utilities like tracking-wide
--leading-*	      Line height utilities like leading-tight
--breakpoint-*    Responsive breakpoint variants like sm:*
--container-*	  Container query variants like @sm:* and size utilities like max-w-md
--spacing-*	      Spacing and sizing utilities like px-4, max-h-16, and many more
--radius-*	      Border radius utilities like rounded-sm
--shadow-*	      Box shadow utilities like shadow-md
--inset-shadow-*  Inset box shadow utilities like inset-shadow-xs
--drop-shadow-*	  Drop shadow filter utilities like drop-shadow-md
--blur-*	      Blur filter utilities like blur-md
--perspective-*	  Perspective utilities like perspective-near
--aspect-*	      Aspect ratio utilities like aspect-video
--ease-*	      Transition timing function utilities like ease-out
--animate-*	      Animation utilities like animate-spin

Configuration
-------------

Uses a pure CSS configuration, including content scanning settings. 
The CLI is passed one or more CSS files which are processed/watched concurrently.
Imported CSS modules are processed by sfumato, not the browser.

```css
::sfumato {

    --paths: ["../Models/", "../Views/"]; /* Relative paths for project scanning; required */
    --output: "output.css"; /* Generated CSS file path; required */
    --not-paths: ["../Views/temp/"]; /* Relative paths to exclude content scanning */

    --use-reset: true; /* Inject the CSS reset */
    --use-forms: true; /* Add form input styles */
    --use-minify: false; /* Compress the output CSS */

    /*
        Other overrides and new utilities go here
        See the defaults.css file
    */
}

/* Optional: How to use @apply and @variant (but not pseudoclass variants) */
.my-class {

    @apply text-base/5 text-indigo-500;

    @variant dark {
      @variant tabp {
          @apply text-indigo-200;
      }
    }
}

```
