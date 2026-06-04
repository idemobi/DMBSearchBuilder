# DMBSearchCore Delivery Checklist

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchCore`
- Main delivery risk: accidental changes to normalization, token extraction, compact technical-name matching, or dependency boundaries.
- Publication target: `labs_idemobi_com`
- Documentation generation: developer-run DocumentationBuilder; AI must not claim generation unless it was actually run.

## 1. Behavior

- Public normalization APIs are backward compatible, or breakage is explicit.
- Index-time and query-time behavior remain aligned for all current consumers.
- The package remains free of builder, viewer, web, crawler, and database dependencies.
- Shared behavior is deterministic and culture-invariant.

## 2. Documentation

- Public API members touched by the change have valid XML documentation.
- README or information pages are updated when public integration behavior changes.
- Documentation is understandable by both developers and AI assistants.

## 3. Examples and pages

- Example or tutorial pages, when added, live under `labs_idemobi_com`.
- Code snippets use `CodeBlockBuilder` or `Html.CodeBlock(...)` when available.
- Any raw example mirror generation that was not run is explicitly reported.

## 4. Verification statement

- Do not claim `dotnet build`, `dotnet test`, `dotnet restore`, `dotnet format`, crawl execution, database generation, or DocumentationBuilder execution unless it was actually run.
- State untested areas clearly before delivery.
