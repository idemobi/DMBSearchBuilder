# DMBSearchCore Examples And Tutorials Rules

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchCore`
- Example role: show shared normalization and tokenization behavior.
- Tutorial role: explain how provider authors should reuse core primitives.
- Publication host: `labs_idemobi_com`

## Example content

- Keep examples small and behavior-focused.
- Prefer examples that compare equivalent inputs such as accented text, mixed case, punctuation, and PascalCase identifiers.
- Show how builders and providers use the same normalized terms.
- Do not include crawler, database, or UI examples unless the page explicitly discusses integration with another package.

## Tutorial content

- Use existing documentation templates when available.
- Explain prerequisites, expected output, and compatibility implications.
- Keep provider authoring guidance generic enough for future providers.

## Code samples

- Use `CodeBlockBuilder` or `Html.CodeBlock(...)` when the host project exposes those helpers.
- Use concise C# snippets.
- Avoid long generated output dumps.

## Verification

- State whether sample code was compiled or only reviewed.
- Do not claim DocumentationBuilder execution unless it was actually run.
