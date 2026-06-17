# DMBSearchViewer AI Context

## Context

`DMBSearchViewer` is the runtime package that provides search UI and search-result aggregation for PageBuilder ecosystem websites.

It should be treated as host-facing runtime infrastructure: changes can affect navbar search, `/Search` routes, provider aggregation, result rendering, and the experience of `labs_idemobi_com`.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchViewer`
- Project role: search controller, navbar component, provider aggregation, result models, path resolution, and search page rendering.
- Primary consumers: `labs_idemobi_com`, host MVC applications, and packages that expose `ISearchProvider`.
- Main source inputs: query text, generated `DMBSearchBuilder` database records, documentation provider results, configured providers, and route values.
- Main output: rendered search pages, navbar search UI, provider result lists, provider errors, and view models.

## Important behavior

- `/Search/Demo` is the manual preview page.
- `ISearchProvider` implementations are the extension point for search sources.
- `SearchBuilderSearchProvider` reads generated site-search data.
- `DocumentationViewerSearchProvider` adds generated documentation results.
- `SearchCompositeAgent` aggregates provider results and provider errors.
- Result URLs and snippets must remain safe to render.

## Maintenance posture

1. Identify whether a change affects routes, provider contracts, query models, result models, path resolution, embedded views, or navbar integration.
2. Preserve deterministic provider behavior and result shape unless the user explicitly asks for a new strategy.
3. Update XML documentation for every touched public member.
4. Update README or information pages when public integration behavior changes.
5. Do not run build, test, restore, format, search index generation, or database generation commands unless explicitly requested.

## Documentation strategy for AI

- Produce extraction-ready docs for DocumentationBuilder.
- Explain provider inputs, result models, routes, defaults, and compatibility with `DMBSearchBuilder`.
- Use `<see cref="..."/>` references for related search viewer types when useful.
- State untested areas and skipped generation steps explicitly.
