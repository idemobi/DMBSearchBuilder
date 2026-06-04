# DMBSearchCore Local Development Runbook

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchCore`
- Project folder: `DMBSearchBuilder/Source/Core`
- Main workflow: update shared search primitives, then verify downstream consumers when the developer chooses to build.
- Downstream consumers: `DMBSearchBuilder`, `DMBSearchViewer`, and future search providers.

## Typical maintenance flow

1. Read the existing public API and consumer usage first.
2. Make the smallest shared change that solves the provider or indexing problem.
3. Keep dependencies minimal and avoid web, crawler, viewer, and database references.
4. Update XML documentation and README when public behavior changes.
5. Ask the developer to run build/restore/test steps when needed.

## Commands

- Do not run `dotnet build`, `dotnet test`, `dotnet restore`, or `dotnet format` unless explicitly requested.
- Do not run crawling, database generation, or DocumentationBuilder unless explicitly requested.

## Handoff notes

- Mention whether downstream consumers were only statically inspected.
- Mention any public normalization behavior change.
- Mention any skipped build or restore step.
