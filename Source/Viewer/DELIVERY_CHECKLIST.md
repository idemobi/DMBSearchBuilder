# DMBSearchViewer Delivery Checklist

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchViewer`
- Main delivery risk: accidental changes to search routes, provider contracts, result models, navbar integration, embedded views, or generated database compatibility.
- Publication target: `labs_idemobi_com`
- Documentation generation: developer-run DocumentationBuilder; AI must not claim generation unless it was actually run.

## 1. Behavior

- Public route, provider, configuration, component, and view-model behavior is backward compatible, or breakage is explicit.
- Provider errors remain visible without breaking the whole search page.
- Result URLs, titles, descriptions, and snippets remain safe to render.
- Generated `DMBSearchBuilder` database compatibility is considered.

## 2. Visual and route coverage

- New visual components include a demo or manual preview page.
- Search UI changes keep `/Search/Demo` useful.
- Empty state, error state, normal state, and realistic data state are covered when a visual component changes.

## 3. Documentation

- Public and protected-contract API members touched by the change have valid XML documentation.
- README or information pages are updated when public integration behavior changes.
- Documentation is understandable by both developers and AI assistants.

## 4. Verification statement

- Do not claim `dotnet build`, `dotnet test`, `dotnet restore`, `dotnet format`, database generation, search index generation, or DocumentationBuilder execution unless it was actually run.
- State untested areas clearly before delivery.
