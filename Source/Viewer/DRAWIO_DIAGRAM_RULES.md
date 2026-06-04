# DMBSearchViewer Draw.io Diagram Rules

## Objective

Draw.io diagrams may be created for information pages, instruction pages, concept pages, architecture pages, provider-flow pages, request-flow pages, and tutorials when a visual model makes the explanation clearer.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchViewer`
- Default publication host: `labs_idemobi_com`
- Default area folder for this project: `SearchViewer`
- Preferred rendering approach: use existing PageBuilder or BootstrapBuilder image helpers when available.

## File format

- Store diagrams as enriched `.drawio.svg` files.
- The `.drawio.svg` must remain editable in Draw.io.
- Do not use plain exported SVG when the source diagram would be lost.

## Publication path

Store diagrams under:

```text
labs_idemobi_com/wwwroot/drawio/{Area}/{diagram-name}.drawio.svg
```

Use `SearchViewer` as the default `{Area}` for this project.

## Diagram content

- Use diagrams only to explain real provider, route, query, result aggregation, navbar, SearchBuilder database, or documentation-provider flows.
- Keep labels short and technical.
- Keep geometry aligned to the Draw.io grid.
- Keep colors readable in both light and dark themes.
- Include meaningful alternative text when rendering the diagram in a page.

## Naming

Prefer clear lowercase file names:

- `search-request-flow.drawio.svg`
- `provider-aggregation-flow.drawio.svg`
- `navbar-search-flow.drawio.svg`

## Rendering

Prefer rendering diagrams through existing PageBuilder or BootstrapBuilder image helpers when an appropriate helper exists. Use raw `<img>` markup only when the local page convention already uses it.
