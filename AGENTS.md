# AI Rules - DMBSearchBuilder

## Scope

- Applies to the `DMBSearchBuilder` folder and descendants.
- This project is autonomous: required rules are defined in local documentation files.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchBuilder`
- Project folder: `DMBSearchBuilder`
- Project role: prebuild search index crawler and SQLite database writer for website search.
- Primary consumer: `DMBSearchViewer`.
- Publication host: `labs_idemobi_com`
- Documentation generation strategy: DocumentationBuilder-first; AI prepares content, the developer executes generation.

## Module intent

- Crawl a target website during prebuilding and store searchable page records in a SQLite database.
- Keep crawl scope, URL normalization, title extraction, description extraction, keyword extraction, and database schema behavior stable for consumers.
- Keep the generated database compatible with `DMBSearchViewer`.

## Key constraints

- Keep public APIs and database output backward compatible unless a change request explicitly allows breakage.
- Prefer additive changes over structural rewrites.
- Keep crawling, path filtering, normalization, and keyword extraction deterministic and predictable.
- Treat target URLs, generated database paths, and crawled content as security-sensitive inputs.
- Do not run `dotnet build`, `dotnet test`, `dotnet restore`, or `dotnet format` unless explicitly requested.
- Do not run website crawling or database generation unless explicitly requested.

## Documentation objective

- Documentation must be authored so it can be extracted and rendered by DocumentationBuilder.
- Publication target is `labs_idemobi_com`.
- Documentation output must serve both developers and AI assistants.
- AI prepares documentation content and structure; the developer runs DocumentationBuilder.
- XML documentation comments must be written in English.
- Public classes, public methods, public properties, public constants, public enums, public enum values, and other public members must have useful XML documentation.

## Local rule sources

- Use [DOCUMENTATION_RULES.md](DOCUMENTATION_RULES.md) for XML HeaderDoc, README/reference documentation, and DocumentationBuilder-ready documentation.
- Use [EXAMPLES_AND_TUTORIALS_RULES.md](EXAMPLES_AND_TUTORIALS_RULES.md) only when creating or updating example, demo, information, instruction, concept, or tutorial pages.
- Use [DRAWIO_DIAGRAM_RULES.md](DRAWIO_DIAGRAM_RULES.md) when adding editable Draw.io diagrams to information, instruction, concept, architecture, crawl-flow, indexing-flow, example, or tutorial pages.
- Use `CodeBlockBuilder` or the local `Html.CodeBlock(...)` helper for code examples in information, instruction, concept, example, and tutorial pages.
- Use `ActionItem` with `ButtonRender` for page action links when the publication host exposes those helpers.
- Store editable Draw.io diagrams as enriched `.drawio.svg` files under `labs_idemobi_com/wwwroot/drawio/{Area}/`.

## Localization

- Follow local [LOCALIZATION_NOMENCLATURE.md](LOCALIZATION_NOMENCLATURE.md).
- Do not assume external localization rules unless duplicated here.

## Before delivery

- Update local docs when behavior changes.
- State untested areas explicitly.
- Do not claim build/test, crawl execution, database generation, or DocumentationBuilder execution when they were not run.
