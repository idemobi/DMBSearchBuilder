# AI Rules - DMBSearchViewer

## Scope

- Applies to the `DMBSearchBuilder/Source/Viewer` folder and descendants.
- This project is autonomous: required rules are defined in local documentation files.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchViewer`
- Project folder: `DMBSearchBuilder/Source/Viewer`
- Project role: runtime search UI, provider aggregation, navbar component, and result page package.
- Primary producer dependency: `DMBSearchBuilder`.
- Publication host: `labs_idemobi_com`
- Documentation generation strategy: DocumentationBuilder-first; AI prepares content, the developer executes generation.

## Module intent

- Provide a reusable search entry point for host websites.
- Aggregate search results from `IDMBSearchProvider` implementations.
- Display generated site-search records from `DMBSearchBuilder` and documentation records from `DMBDocumentationViewer` providers.
- Keep search routes, provider contracts, view models, embedded views, navbar integration, and configuration stable for consumers.

## Key constraints

- Keep public APIs, routes, view models, provider contracts, and result shapes backward compatible unless a change request explicitly allows breakage.
- Prefer additive changes over structural rewrites.
- Keep search provider ordering, error handling, path resolution, and result rendering deterministic.
- Treat query text, provider errors, result URLs, and rendered snippets as security-sensitive inputs.
- Do not run `dotnet build`, `dotnet test`, `dotnet restore`, or `dotnet format` unless explicitly requested.
- Do not run search database generation unless explicitly requested.

## Documentation objective

- Documentation must be authored so it can be extracted and rendered by DocumentationBuilder.
- Publication target is `labs_idemobi_com`.
- Documentation output must serve both developers and AI assistants.
- AI prepares documentation content and structure; the developer runs DocumentationBuilder.
- XML documentation comments must be written in English.
- Public classes, public interfaces, public methods, public constructors, public properties, public constants, public enums, public enum values, and protected contract members must have useful XML documentation.

## Local rule sources

- Use [DOCUMENTATION_RULES.md](DOCUMENTATION_RULES.md) for XML HeaderDoc, README/reference documentation, and DocumentationBuilder-ready documentation.
- Use [EXAMPLES_AND_TUTORIALS_RULES.md](EXAMPLES_AND_TUTORIALS_RULES.md) only when creating or updating example, demo, information, instruction, concept, or tutorial pages.
- Use [DRAWIO_DIAGRAM_RULES.md](DRAWIO_DIAGRAM_RULES.md) when adding editable Draw.io diagrams to information, instruction, concept, architecture, provider-flow, request-flow, example, or tutorial pages.
- Use `CodeBlockBuilder` or the local `Html.CodeBlock(...)` helper for code examples in information, instruction, concept, example, and tutorial pages.
- Use `ActionItem` with `ButtonRender` for page action links when the host project exposes those helpers.
- Store editable Draw.io diagrams as enriched `.drawio.svg` files under `labs_idemobi_com/wwwroot/drawio/{Area}/`.

## Localization

- Follow local [LOCALIZATION_NOMENCLATURE.md](LOCALIZATION_NOMENCLATURE.md).
- Do not assume external localization rules unless duplicated here.

## Before delivery

- Update local docs when behavior changes.
- State untested areas explicitly.
- Do not claim build/test, database generation, search index generation, or DocumentationBuilder execution when they were not run.
