# DMBSearchCore

DMBSearchCore contains shared search primitives used by DMB search packages.

The first shared primitive is `DMBSearchTextNormalizer`, which keeps indexing and query matching consistent across builders, viewers, and future search providers.

## Current Scope

- Normalize search text without accents, casing differences, or punctuation noise.
- Extract stable ordered search tokens.
- Compare compact technical names such as `TitleBuilder` and `title builder`.

## Consumers

- `DMBSearchBuilder` uses the core normalizer while generating page keyword indexes.
- `DMBSearchViewer` uses the same normalizer while querying builder and documentation providers.
- Future providers should depend on this package instead of copying normalization rules.
