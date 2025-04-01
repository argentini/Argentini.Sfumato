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
- CSS custom properties drives (almost) everything
- Theme customization is largely in the master SCSS file, though scanning and other settings remain in YML file
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

1.	Dark Mode Variants (dark, light)
2.	Breakpoint Variants (sm, md, lg, xl, 2xl, etc.)
3.	Variants (group, peer, etc.)
4.	Custom variants ([&.active], etc.)
5.	State Variants (hover, focus, active, etc.)
6.	Logical Variants (ltr, rtl, first, last, even, odd, etc.)

Themes
======

Theme Variable Namespaces
-------------------------

https://tailwindcss.com/docs/theme#theme-variable-namespaces

Namespace	        Utility Classes

--color-*	        Color utilities like bg-red-500, text-sky-300, and many more
--font-*	        Font family utilities like font-sans
--text-*	        Font size utilities like text-xl
--font-weight-*     Font weight utilities like font-bold
--tracking-*	    Letter spacing utilities like tracking-wide
--leading-*	        Line height utilities like leading-tight
--breakpoint-*      Responsive breakpoint variants like sm:*
--container-*	    Container query variants like @sm:* and size utilities like max-w-md
--spacing-*	        Spacing and sizing utilities like px-4, max-h-16, and many more
--radius-*	        Border radius utilities like rounded-sm
--shadow-*	        Box shadow utilities like shadow-md
--inset-shadow-*	Inset box shadow utilities like inset-shadow-xs
--drop-shadow-*	    Drop shadow filter utilities like drop-shadow-md
--blur-*	        Blur filter utilities like blur-md
--perspective-*	    Perspective utilities like perspective-near
--aspect-*	        Aspect ratio utilities like aspect-video
--ease-*	        Transition timing function utilities like ease-out
--animate-*	        Animation utilities like animate-spin

Default Theme CSS Custom Properties
-----------------------------------

@theme {

    --font-sans: ui-sans-serif, system-ui, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji";
    --font-serif: ui-serif, Georgia, Cambria, "Times New Roman", Times, serif;
    --font-mono: ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, "Liberation Mono", "Courier New", monospace;

    --color-red-50: oklch(0.971 0.013 17.38);
    ...
    --color-black: #000;
    --color-white: #fff;
    ...

    --spacing: 0.25rem;

    --breakpoint-sm: 40rem;
    --breakpoint-md: 48rem;
    --breakpoint-lg: 64rem;
    --breakpoint-xl: 80rem;
    --breakpoint-2xl: 96rem;

    --container-3xs: 16rem;
    --container-2xs: 18rem;
    --container-xs: 20rem;
    --container-sm: 24rem;
    --container-md: 28rem;
    --container-lg: 32rem;
    --container-xl: 36rem;
    --container-2xl: 42rem;
    --container-3xl: 48rem;
    --container-4xl: 56rem;
    --container-5xl: 64rem;
    --container-6xl: 72rem;
    --container-7xl: 80rem;

    --text-xs: 0.75rem;
    --text-xs--line-height: calc(1 / 0.75);
    --text-sm: 0.875rem;
    --text-sm--line-height: calc(1.25 / 0.875);
    --text-base: 1rem;
    --text-base--line-height: calc(1.5 / 1);
    --text-lg: 1.125rem;
    --text-lg--line-height: calc(1.75 / 1.125);
    --text-xl: 1.25rem;
    --text-xl--line-height: calc(1.75 / 1.25);
    --text-2xl: 1.5rem;
    --text-2xl--line-height: calc(2 / 1.5);
    --text-3xl: 1.875rem;
    --text-3xl--line-height: calc(2.25 / 1.875);
    --text-4xl: 2.25rem;
    --text-4xl--line-height: calc(2.5 / 2.25);
    --text-5xl: 3rem;
    --text-5xl--line-height: 1;
    --text-6xl: 3.75rem;
    --text-6xl--line-height: 1;
    --text-7xl: 4.5rem;
    --text-7xl--line-height: 1;
    --text-8xl: 6rem;
    --text-8xl--line-height: 1;
    --text-9xl: 8rem;
    --text-9xl--line-height: 1;

    --font-weight-thin: 100;
    --font-weight-extralight: 200;
    --font-weight-light: 300;
    --font-weight-normal: 400;
    --font-weight-medium: 500;
    --font-weight-semibold: 600;
    --font-weight-bold: 700;
    --font-weight-extrabold: 800;
    --font-weight-black: 900;

    --tracking-tighter: -0.05em;
    --tracking-tight: -0.025em;
    --tracking-normal: 0em;
    --tracking-wide: 0.025em;
    --tracking-wider: 0.05em;
    --tracking-widest: 0.1em;

    --leading-tight: 1.25;
    --leading-snug: 1.375;
    --leading-normal: 1.5;
    --leading-relaxed: 1.625;
    --leading-loose: 2;

    --radius-xs: 0.125rem;
    --radius-sm: 0.25rem;
    --radius-md: 0.375rem;
    --radius-lg: 0.5rem;
    --radius-xl: 0.75rem;
    --radius-2xl: 1rem;
    --radius-3xl: 1.5rem;
    --radius-4xl: 2rem;

    --shadow-2xs: 0 1px rgb(0 0 0 / 0.05);
    --shadow-xs: 0 1px 2px 0 rgb(0 0 0 / 0.05);
    --shadow-sm: 0 1px 3px 0 rgb(0 0 0 / 0.1), 0 1px 2px -1px rgb(0 0 0 / 0.1);
    --shadow-md: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
    --shadow-lg: 0 10px 15px -3px rgb(0 0 0 / 0.1), 0 4px 6px -4px rgb(0 0 0 / 0.1);
    --shadow-xl: 0 20px 25px -5px rgb(0 0 0 / 0.1), 0 8px 10px -6px rgb(0 0 0 / 0.1);
    --shadow-2xl: 0 25px 50px -12px rgb(0 0 0 / 0.25);

    --inset-shadow-2xs: inset 0 1px rgb(0 0 0 / 0.05);
    --inset-shadow-xs: inset 0 1px 1px rgb(0 0 0 / 0.05);
    --inset-shadow-sm: inset 0 2px 4px rgb(0 0 0 / 0.05);

    --drop-shadow-xs: 0 1px 1px rgb(0 0 0 / 0.05);
    --drop-shadow-sm: 0 1px 2px rgb(0 0 0 / 0.15);
    --drop-shadow-md: 0 3px 3px rgb(0 0 0 / 0.12);
    --drop-shadow-lg: 0 4px 4px rgb(0 0 0 / 0.15);
    --drop-shadow-xl: 0 9px 7px rgb(0 0 0 / 0.1);
    --drop-shadow-2xl: 0 25px 25px rgb(0 0 0 / 0.15);

    --blur-xs: 4px;
    --blur-sm: 8px;
    --blur-md: 12px;
    --blur-lg: 16px;
    --blur-xl: 24px;
    --blur-2xl: 40px;
    --blur-3xl: 64px;

    --perspective-dramatic: 100px;
    --perspective-near: 300px;
    --perspective-normal: 500px;
    --perspective-midrange: 800px;
    --perspective-distant: 1200px;

    --aspect-video: 16 / 9;

    --ease-in: cubic-bezier(0.4, 0, 1, 1);
    --ease-out: cubic-bezier(0, 0, 0.2, 1);
    --ease-in-out: cubic-bezier(0.4, 0, 0.2, 1);

    --animate-spin: spin 1s linear infinite;
    --animate-ping: ping 1s cubic-bezier(0, 0, 0.2, 1) infinite;
    --animate-pulse: pulse 2s cubic-bezier(0.4, 0, 0.6, 1) infinite;
    --animate-bounce: bounce 1s infinite;

    @keyframes spin {
        to {
            transform: rotate(360deg);
        }
    }
    @keyframes ping {
        75%,
        100% {
            transform: scale(2);
            opacity: 0;
        }
    }
    @keyframes pulse {
        50% {
            opacity: 0.5;
        }
    }
    @keyframes bounce {
        0%,
        100% {
            transform: translateY(-25%);
            animation-timing-function: cubic-bezier(0.8, 0, 1, 1);
        }
        50% {
            transform: none;
            animation-timing-function: cubic-bezier(0, 0, 0.2, 1);
        }
    }
}

