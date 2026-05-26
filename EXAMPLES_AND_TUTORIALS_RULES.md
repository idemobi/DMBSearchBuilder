# DMBSearchBuilder Examples and Tutorials Rules

## Objective

Define how example pages, demo pages, information pages, concept pages, instruction pages, and tutorials are created for `DMBSearchBuilder`.

These rules apply only when the task explicitly creates or updates:

- example pages,
- demo pages,
- information pages,
- instruction pages,
- concept pages,
- tutorial pages,
- walkthrough pages,
- example partials rendered through `RenderExamplePartialAsync`.

Do not use this file as the rule source for XML API documentation or reference documentation. Use `DOCUMENTATION_RULES.md` for that work.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchBuilder`
- Default documentation area: `SearchBuilder`
- Publication target: `../labs_idemobi_com`
- Shared UI stack: `DMBBootstrapBuilder`, `DMBComponentBuilder`, `DMBFormBuilder`, and `DMBPageBuilder`
- Default DocumentationViewer package id for this project: `DMBSearchBuilder`
- Default DocumentationViewer namespace for this project: `DMBSearchBuilder`

## Publication target

Examples and tutorials must be written in `../labs_idemobi_com`.

Use the existing MVC conventions in that project:

- controller actions in `labs_idemobi_com/Controllers`,
- full pages in `labs_idemobi_com/Views/{FeatureOrComponent}/`,
- reusable example partials in `labs_idemobi_com/Views/Shared/Examples/`,
- generated raw-code mirrors in `labs_idemobi_com/Views/Shared/Examples_Raw/`.

AI may create or update source example partials under `Views/Shared/Examples/`. The developer or prebuild step is responsible for regenerating `Views/Shared/Examples_Raw/` when required.

## General information page format

Use this format for package introductions, architecture pages, crawl-flow pages, indexing-flow pages, getting started pages, integration guides, and tutorials that are not focused on one public class.

Required structure:

1. Title.
2. Short context paragraph explaining the topic and audience.
3. Explanation sections with deterministic headings.
4. Practical integration or usage section when relevant.
5. Notes, constraints, or next steps.
6. Links to related documentation pages or API reference when useful.

General information pages:

- may include short code snippets rendered with `CodeBlockBuilder` or `Html.CodeBlock(...)`,
- may include diagrams or structured lists when they clarify crawl, indexing, or database flow,
- should avoid long inline API listings that belong in DocumentationViewer.

## Code examples

Code examples must:

- specify the language,
- have a useful title when the page contains more than one snippet,
- enable copy behavior when consistent with existing pages,
- stay focused on SearchBuilder concepts such as build options, excluded paths, database output, and viewer compatibility.

## Links and actions

Links that behave like page actions must be generated through `ActionItem` implementations and rendered with `ButtonRender` when possible.

Use plain inline anchors only for natural text links inside paragraphs where a button/action would be visually inappropriate.

## Tutorial expectations

Tutorials must:

- explain prerequisites and assumptions,
- provide step-by-step usage,
- state the expected result for each step,
- include common mistakes and recovery guidance,
- link to relevant SearchBuilder or SearchViewer documentation pages.

## Delivery checklist for examples

Before finishing an example or tutorial task, verify:

- the page is under `labs_idemobi_com`,
- the page uses existing BootstrapBuilder, ComponentBuilder, FormBuilder, or PageBuilder components where appropriate,
- code snippets use `CodeBlockBuilder` or `Html.CodeBlock(...)` when available,
- Draw.io diagrams, when added, follow `DRAWIO_DIAGRAM_RULES.md`,
- any raw example generation that was not run is explicitly reported.
