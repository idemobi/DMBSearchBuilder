# DMBSearchCore Documentation Rules

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchCore`
- Documentation role: shared search primitive reference documentation.
- Main audience: package consumers, provider authors, maintainers, and AI assistants.
- Documentation target: DocumentationBuilder output rendered in `labs_idemobi_com`.

## XML documentation

- Write XML documentation in English.
- Public classes, public methods, public constructors, public properties, public fields, public constants, public enums, public enum values, and public delegates must have useful XML documentation.
- Prefer `<summary>`, `<param>`, `<returns>`, `<remarks>`, `<exception>`, `<see cref="..."/>`, and `<seealso cref="..."/>`.
- Use `<see cref="DMBSearchTextNormalizer"/>` for local references when helpful.
- Keep comments focused on behavior and contracts, not implementation narration.

## Content requirements

- Explain what behavior is shared and why it belongs in core.
- Document compatibility implications for builders, viewers, and providers.
- Document edge cases when changing normalization or token extraction.
- Avoid provider-specific UI, crawler, database, or host details unless they explain a compatibility concern.

## README and information pages

- Keep README concise and package-oriented.
- Use information pages in `labs_idemobi_com` for broader conceptual explanations when needed.
- Do not invent new document structures when local templates exist.

## Verification language

- State whether build, test, restore, format, crawling, database generation, or DocumentationBuilder execution was skipped.
- Do not claim generated documentation exists unless generation was actually run.
