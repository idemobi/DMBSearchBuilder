# DMBSearchBuilder Local Development Runbook

## Objective

Guide local development for `DMBSearchBuilder` changes.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchBuilder`
- Main code areas: `Source/Builder/` crawler/build classes, `Source/Builder/Resources/`, and embedded `Source/Builder/wwwroot/` assets.
- Main risk areas: crawl scope, URL normalization, excluded paths, text normalization, keyword extraction, SQLite writes, generated database compatibility, and prebuild integration.
- Documentation target: `labs_idemobi_com`

## Standard workflow

1. Update crawler, build options, database, normalization, keyword, configuration, or resource code.
2. Update XML documentation for touched public APIs.
3. Update `README.md` or information pages when public integration behavior changes.
4. Check generated search database compatibility with `DMBSearchViewer`.
5. Hand off for developer-run DocumentationBuilder generation when documentation content changed.

## Manual review focus

- Base URI and same-site crawl behavior.
- Query-string removal and duplicate URL handling.
- Default excluded path prefixes.
- Database path handling and overwrite behavior.
- Empty pages, failed requests, redirects, and missing titles/descriptions.
- Keyword quality and normalization assumptions.

## Documentation handoff checks

- Documentation structure is extraction-ready.
- Examples are self-contained and realistic.
- Untested crawl/database generation steps are explicitly stated.

## Commands

- Do not run `dotnet build`, `dotnet test`, `dotnet restore`, or `dotnet format` unless explicitly requested.
- Do not run crawling or database generation commands unless explicitly requested.
