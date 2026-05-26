# DMBSearchBuilder Delivery Checklist

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchBuilder`
- Main delivery risk: accidental changes to crawl scope, URL normalization, keyword extraction, generated database compatibility, or default exclusions.
- Publication target: `labs_idemobi_com`
- Documentation generation: developer-run DocumentationBuilder; AI must not claim generation unless it was actually run.

## 1. Behavior

- Public crawl options, database output, and generated record behavior are backward compatible, or breakage is explicit.
- Default excluded paths remain intentional.
- Query-string handling and URL normalization are documented when changed.
- Generated database compatibility with `DMBSearchViewer` is considered.

## 2. Documentation

- Public and protected-contract API members touched by the change have valid XML documentation.
- README or information pages are updated when public integration behavior changes.
- Documentation is understandable by both developers and AI assistants.

## 3. Examples and pages

- Example or tutorial pages, when added, live under `labs_idemobi_com`.
- Code snippets use `CodeBlockBuilder` or `Html.CodeBlock(...)` when available.
- Any raw example mirror generation that was not run is explicitly reported.

## 4. Verification statement

- Do not claim `dotnet build`, `dotnet test`, `dotnet restore`, `dotnet format`, crawl execution, database generation, or DocumentationBuilder execution unless it was actually run.
- State untested areas clearly before delivery.
