# DMBSearchCore Glossary

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchCore`
- Vocabulary focus: shared search normalization and provider compatibility.

## Terms

- **Core primitive**: A small reusable behavior that can be consumed by builders, viewers, and providers without importing their implementation details.
- **Normalized text**: Text converted to a lowercase, accent-insensitive, punctuation-insensitive, whitespace-normalized form.
- **Search token**: A normalized token extracted from user text or indexed content.
- **Compact search text**: Normalized text with spaces removed, used to compare technical names such as `TitleBuilder` and `title builder`.
- **Provider**: A search implementation that returns results to `DMBSearchViewer`.
- **Index-time behavior**: Rules applied while building or storing searchable content.
- **Query-time behavior**: Rules applied while processing a user search term.
- **Compatibility contract**: Behavior that consumers rely on and that should not change without explicit approval.
