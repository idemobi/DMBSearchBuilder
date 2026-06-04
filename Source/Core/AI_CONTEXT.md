# DMBSearchCore AI Context

## Context

`DMBSearchCore` is the shared foundation for search behavior used by DMB search packages.

It should be treated as compatibility-sensitive infrastructure: changes can affect generated search indexes, runtime queries, documentation search providers, and future provider implementations.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchCore`
- Project role: shared search normalization and tokenization primitives.
- Primary consumers: `DMBSearchBuilder`, `DMBSearchViewer`, and future search providers.
- Main documentation target: DocumentationBuilder output rendered in `labs_idemobi_com`.
- Main source inputs: raw page text, query text, technical identifiers, titles, descriptions, and provider keywords.
- Main output: normalized text, ordered search tokens, and compact technical-name strings.

## Important behavior

- Normalize accents, case, punctuation, and repeated whitespace consistently.
- Split PascalCase and acronym boundaries before token extraction.
- Keep compact comparisons stable for technical names such as `TitleBuilder` and `title builder`.
- Avoid references to builder, viewer, ASP.NET Core, SQLite, crawling, or rendering packages.
- Keep the package small enough for future providers to reference safely.

## Maintenance posture

1. Identify whether a change affects normalization, token extraction, compact matching, or public API shape.
2. Preserve deterministic behavior unless the user explicitly asks for a new strategy.
3. Update XML documentation for every touched public member.
4. Update README or information pages when public integration behavior changes.
5. Do not run build, test, restore, format, crawling, database generation, or DocumentationBuilder commands unless explicitly requested.

## Documentation strategy for AI

- Produce extraction-ready docs for DocumentationBuilder.
- Explain shared contracts and compatibility with builders, viewers, and future providers.
- Use `<see cref="..."/>` references for related search core types when useful.
- State untested areas and skipped generation steps explicitly.
