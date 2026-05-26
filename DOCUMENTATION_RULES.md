# DMBSearchBuilder Documentation Rules

## Language

- Documentation must be written in English.
- XML documentation comments must be written in English.

## Target audience

- Primary: developers maintaining or integrating `DMBSearchBuilder`.
- Secondary: developers using `DMBSearchViewer` with a generated site-search database.
- Tertiary: AI assistants consuming structured project rules and technical context.

Documentation must be useful without private chat context. A reader should understand what the crawler reads, what the database stores, how options affect crawl scope, and what compatibility constraints apply before reading the implementation.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchBuilder`
- Primary API families: build options, launch profile options, website host crawling, page record storage, database manager, text normalization, keyword extraction, configuration, and prebuild agent orchestration.
- Important types to reference when relevant: `DMBSearchBuilderAgent`, `DMBSearchBuildOptions`, `DMBSearchLaunchProfileBuildOptions`, `DMBSearchWebsiteHost`, `DMBSearchPageRecord`, `DMBSearchDatabaseManager`, `DMBSearchKeywordExtractor`, `DMBSearchTextNormalizer`, and `DMBSearchBuilderConfiguration`.
- Publication host: `labs_idemobi_com`
- Documentation generation strategy: DocumentationBuilder-first; AI prepares content, the developer executes generation.

## Strict C# XML documentation policy

- Always write XML HeaderDoc for public classes, interfaces, structs, methods, constructors, properties, fields, constants, events, delegates, enums, enum values, and extension methods.
- Also write XML HeaderDoc for protected members when they are part of the inheritance contract or expected to be overridden.
- Internal and private members do not require XML HeaderDoc unless they explain complex crawl, database, normalization, or security behavior.
- XML documentation must use valid C# XML syntax.
- Prefer `<summary>`, `<param>`, `<typeparam>`, `<returns>`, `<value>`, `<remarks>`, `<exception>`, `<see cref="..."/>`, and `<seealso cref="..."/>`.
- Use `<inheritdoc/>` only when the inherited documentation is accurate for the current member.

## XML documentation quality standard

For classes and interfaces, document:

- the type's role in crawl orchestration, website access, page record storage, keyword extraction, normalization, or configuration,
- the relationship with important types such as `DMBSearchBuilderAgent`, `DMBSearchBuildOptions`, `DMBSearchPageRecord`, and `DMBSearchDatabaseManager`,
- lifecycle expectations, including whether the type is called by prebuild code, host configuration, or another package.

For methods and constructors, document:

- what the member changes or returns in crawl behavior, generated records, database output, or configuration,
- every parameter and the expected format when relevant,
- returned values and side effects such as HTTP requests, database writes, path normalization, keyword extraction, or filesystem output,
- validation rules and exceptions,
- whether `null`, empty strings, duplicate URLs, repeated calls, missing pages, redirects, or failed requests have special behavior.

For properties, fields, and constants, document:

- the meaning of the value,
- the default value when meaningful,
- whether consumers may set it directly,
- how it affects crawl scope, filtering, normalization, ordering, deduplication, database paths, or search relevance.

## Project API documentation requirements

- Crawl APIs must document base URI requirements, path filtering, query-string behavior, maximum page limits, and failure handling.
- Database APIs must document schema expectations, database path behavior, overwrite/append behavior, and compatibility with `DMBSearchViewer`.
- Text and keyword APIs must document normalization rules, language assumptions, token filtering, and empty-content behavior.
- Configuration APIs must document how they are registered by host or prebuild projects.
- Security-sensitive APIs must mention URL, filesystem path, HTML content, database path, and crawled-content risks when consumer-provided values are used.

## Examples in XML documentation

Use `<example>` when it materially improves understanding of:

- configuring `DMBSearchBuildOptions`,
- running `DMBSearchBuilderAgent`,
- overriding excluded paths,
- using custom database paths,
- understanding generated page records.

Examples must be short, realistic, and compile-oriented.

## Markdown documentation policy

- Follow PageBuilder markdown conventions in `../MARKDOWN_GUIDELINES.md`.
- Keep this structure where applicable:
  1. Context
  2. Explanation
  3. Example
  4. Notes / constraints

## Draw.io diagrams for conceptual documentation

Information pages, instruction pages, concept pages, architecture pages, crawl-flow pages, and indexing-flow pages may use Draw.io diagrams when they clarify a real model or flow.

Draw.io diagrams must follow `DRAWIO_DIAGRAM_RULES.md`.

## DocumentationBuilder-first rule

Documentation in this module must be authored with a DocumentationBuilder-first objective.

- Write docs so they can be extracted and rendered without manual rewrite.
- Keep headings deterministic and stable.
- Keep examples self-contained and realistically useful.
- Avoid implicit references to chat history or hidden context.
- Prefer stable type and member names that DocumentationBuilder can cross-reference.
- Use `<see cref="..."/>` and `<seealso cref="..."/>` for related search builder types whenever it improves navigation.

## Minimum update policy

If public crawl behavior, database output behavior, keyword extraction behavior, or configuration behavior changes, update in the same change set:

- local `README.md`,
- relevant XML docs,
- impacted guidance/examples.

## Review checklist for documentation changes

- The documentation names the real SearchBuilder concept, not a copied source project concept.
- All public and protected-contract API members touched by the change have valid XML documentation.
- Summaries are specific enough to help IntelliSense users choose the right API.
- Parameters, return values, exceptions, and side effects are documented where applicable.
- Examples reflect current code behavior and realistic SearchBuilder usage.
- Draw.io diagrams, when added, follow `DRAWIO_DIAGRAM_RULES.md`.
- DocumentationBuilder can extract the content without needing hidden context or manual rewrite.
