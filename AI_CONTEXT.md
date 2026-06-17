# DMBSearchBuilder AI Context

## Context

`DMBSearchBuilder` is the prebuild package that crawls a website and writes a compact SQLite search index for `DMBSearchViewer`.

It should be treated as search infrastructure: changes can affect crawled URL coverage, generated database compatibility, keyword relevance, and the search experience in `labs_idemobi_com`.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchBuilder`
- Project role: website crawler, page metadata extractor, keyword extractor, text normalizer, and SQLite search database writer.
- Primary consumers: `PreBuilding`, `labs_idemobi_com`, and `DMBSearchViewer`.
- Main documentation target: DocumentationBuilder output rendered in `labs_idemobi_com`.
- Main source inputs: target website pages, crawl options, excluded path prefixes, launch profile options, and generated page content.
- Main output: `SearchPageRecord` rows stored in the configured SQLite database.

## Important behavior

- Crawl only the intended website scope.
- Remove query strings by default to avoid repeated filter or search states.
- Exclude `/Documentation` by default because documentation search is handled by `DMBDocumentationViewer` through `DMBSearchViewer`.
- Exclude `/Search` by default to avoid indexing search result pages.
- Normalize text before keyword extraction.
- Keep generated database paths and schema compatible with `DMBSearchViewer`.

## Maintenance posture

1. Identify whether a change affects crawl scope, URL normalization, keyword extraction, database schema, launch profile integration, or default exclusions.
2. Preserve deterministic behavior unless the user explicitly asks for a new strategy.
3. Update XML documentation for every touched public member.
4. Update README or information pages when public integration behavior changes.
5. Do not run build, test, restore, format, crawling, or database generation commands unless explicitly requested.

## Documentation strategy for AI

- Produce extraction-ready docs for DocumentationBuilder.
- Explain crawler inputs, generated outputs, defaults, and compatibility with `DMBSearchViewer`.
- Use `<see cref="..."/>` references for related search builder types when useful.
- State untested areas and skipped generation steps explicitly.
