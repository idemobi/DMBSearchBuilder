# DMBSearchViewer Documentation Rules

## Language

- Documentation must be written in English.
- XML documentation comments must be written in English.

## Target audience

- Primary: developers maintaining or integrating `DMBSearchViewer`.
- Secondary: developers adding search providers or using `DMBSearchBuilder` generated data.
- Tertiary: AI assistants consuming structured project rules and technical context.

Documentation must be useful without private chat context. A reader should understand which routes are exposed, how providers are queried, how results are rendered, and what stability constraints apply before reading the implementation.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchViewer`
- Primary API families: search controller, navbar component, provider contracts, composite search agent, SearchBuilder provider, DocumentationViewer provider, path resolver, configuration, query models, result models, response models, provider errors, and embedded views.
- Important types to reference when relevant: `SearchController`, `DMBSearchNavbarComponent`, `IDMBSearchProvider`, `DMBSearchCompositeAgent`, `DMBSearchBuilderSearchProvider`, `DMBDocumentationViewerSearchProvider`, `DMBSearchPathResolver`, `DMBSearchViewerConfiguration`, `DMBSearchQuery`, `DMBSearchResult`, `DMBSearchResponse`, `DMBSearchProviderError`, and `DMBSearchPageViewModel`.
- Publication host: `labs_idemobi_com`
- Documentation generation strategy: DocumentationBuilder-first; AI prepares content, the developer executes generation.

## Strict C# XML documentation policy

- Always write XML HeaderDoc for public classes, interfaces, structs, methods, constructors, properties, fields, constants, events, delegates, enums, enum values, and extension methods.
- Also write XML HeaderDoc for protected members when they are part of the inheritance contract or expected to be overridden.
- Internal and private members do not require XML HeaderDoc unless they explain complex routing, provider aggregation, rendering, path, or security behavior.
- XML documentation must use valid C# XML syntax.
- Prefer `<summary>`, `<param>`, `<typeparam>`, `<returns>`, `<value>`, `<remarks>`, `<exception>`, `<see cref="..."/>`, and `<seealso cref="..."/>`.
- Use `<inheritdoc/>` only when the inherited documentation is accurate for the current member.

## XML documentation quality standard

For classes and interfaces, document:

- the type's role in search routing, provider querying, navbar rendering, result aggregation, path resolution, or configuration,
- the relationship with important types such as `IDMBSearchProvider`, `DMBSearchCompositeAgent`, `SearchController`, and search result models,
- lifecycle expectations, including whether the type is called by MVC, the host application, a Razor view, or another provider.

For methods and constructors, document:

- what the member changes or returns in routed output, provider results, rendered UI, or configuration,
- every parameter and the expected format when relevant,
- returned values and side effects such as provider queries, result aggregation, error collection, URL resolution, or endpoint registration,
- validation rules and exceptions,
- whether `null`, empty strings, empty results, provider failures, duplicate records, or repeated calls have special behavior.

For properties, fields, and constants, document:

- the meaning of the value,
- the default value when meaningful,
- whether consumers may set it directly,
- how it affects routing, query filtering, result ordering, provider behavior, rendering, or configuration.

## Project API documentation requirements

- Route APIs must document route values, query parameters, empty query behavior, and response model expectations.
- Provider APIs must document query inputs, cancellation behavior when applicable, error behavior, and expected result ordering.
- Result model APIs must document URL, title, description, provider, and score semantics when present.
- Configuration APIs must document how they are registered by host applications.
- Visual component APIs must document rendered UI behavior, empty state, error state, and accessibility expectations when relevant.
- Security-sensitive APIs must mention query text, URL, snippet, provider error, and rendered-content risks when consumer-provided values are rendered.

## Examples in XML documentation

Use `<example>` when it materially improves understanding of:

- registering `DMBSearchViewerConfiguration`,
- implementing `IDMBSearchProvider`,
- querying through `DMBSearchCompositeAgent`,
- using `DMBSearchQuery`,
- rendering or configuring navbar search behavior.

Examples must be short, realistic, and compile-oriented.

## Markdown documentation policy

- Follow PageBuilder markdown conventions in `../MARKDOWN_GUIDELINES.md`.
- Keep this structure where applicable:
  1. Context
  2. Explanation
  3. Example
  4. Notes / constraints

## Draw.io diagrams for conceptual documentation

Information pages, instruction pages, concept pages, architecture pages, provider-flow pages, and request-flow pages may use Draw.io diagrams when they clarify a real model or flow.

Draw.io diagrams must follow `DRAWIO_DIAGRAM_RULES.md`.

## DocumentationBuilder-first rule

Documentation in this module must be authored with a DocumentationBuilder-first objective.

- Write docs so they can be extracted and rendered without manual rewrite.
- Keep headings deterministic and stable.
- Keep examples self-contained and realistically useful.
- Avoid implicit references to chat history or hidden context.
- Prefer stable type and member names that DocumentationBuilder can cross-reference.
- Use `<see cref="..."/>` and `<seealso cref="..."/>` for related search viewer types whenever it improves navigation.

## Minimum update policy

If public routing behavior, provider behavior, result behavior, navbar behavior, embedded view behavior, or configuration behavior changes, update in the same change set:

- local `README.md`,
- relevant XML docs,
- impacted guidance/examples.

If a new visual or user-facing rendering component is added, update or add a demo/example page and apply `EXAMPLES_AND_TUTORIALS_RULES.md`.

## Review checklist for documentation changes

- The documentation names the real SearchViewer concept, not a copied source project concept.
- All public and protected-contract API members touched by the change have valid XML documentation.
- Summaries are specific enough to help IntelliSense users choose the right API.
- Parameters, return values, exceptions, and side effects are documented where applicable.
- Examples reflect current code behavior and realistic SearchViewer usage.
- Draw.io diagrams, when added, follow `DRAWIO_DIAGRAM_RULES.md`.
- DocumentationBuilder can extract the content without needing hidden context or manual rewrite.
