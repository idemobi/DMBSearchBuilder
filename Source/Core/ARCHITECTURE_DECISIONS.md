# DMBSearchCore Architecture Decisions

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchCore`
- Architectural role: shared search primitives used by builder, viewer, and future providers.
- Main stability concerns: normalization rules, token extraction, compact technical-name matching, public API shape, and dependency boundaries.
- Documentation target: DocumentationBuilder output rendered in `labs_idemobi_com`.

## ADR-001: Shared normalization is a public compatibility contract

- Context: Indexing and querying must use the same text rules.
- Decision: Store normalization and token extraction in `DMBSearchCore` so builders, viewers, and providers depend on one implementation.
- Consequence: Changes to normalization behavior can affect search results and must be documented.

## ADR-002: Core has no web, crawler, or persistence dependency

- Context: Future providers should be able to use search primitives without pulling in runtime UI, crawler, or SQLite packages.
- Decision: Keep `DMBSearchCore` as a plain `Microsoft.NET.Sdk` package with no ASP.NET Core or database dependency.
- Consequence: Provider-specific models and persistence behavior stay outside this project unless promoted as deliberate shared contracts.

## ADR-003: DocumentationBuilder-first documentation

- Context: Documentation is expected to be extracted and rendered into `labs_idemobi_com`.
- Decision: Public APIs must include useful XML documentation and stable references.
- Consequence: AI prepares documentation and must not claim DocumentationBuilder execution unless it was actually run.
