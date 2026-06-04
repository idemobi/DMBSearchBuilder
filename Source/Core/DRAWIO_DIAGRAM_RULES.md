# DMBSearchCore Draw.io Diagram Rules

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchCore`
- Diagram role: explain shared search primitive flows and dependency boundaries.
- Preferred storage: `labs_idemobi_com/wwwroot/drawio/SearchCore/`
- Documentation target: DocumentationBuilder output rendered in `labs_idemobi_com`.

## When to create diagrams

- Create diagrams only when they clarify a concept that text alone makes harder to understand.
- Useful diagrams include normalization flow, provider dependency boundaries, and index/query compatibility.
- Do not create diagrams for trivial method descriptions.

## File rules

- Store editable diagrams as enriched `.drawio.svg` files.
- Keep source diagrams editable in Draw.io.
- Use stable names such as `search-core-normalization-flow.drawio.svg`.

## Content rules

- Show `DMBSearchCore` as independent from builder, viewer, web host, and database concerns.
- Show builders and providers depending inward on core.
- Keep labels short and implementation-neutral.

## Delivery

- Mention any diagram files added or updated.
- Do not claim diagram export verification unless it was actually performed.
