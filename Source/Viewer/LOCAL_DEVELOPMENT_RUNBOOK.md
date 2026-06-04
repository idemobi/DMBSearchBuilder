# DMBSearchViewer Local Development Runbook

## Objective

Guide local development for `DMBSearchViewer` changes.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchViewer`
- Project folder: `DMBSearchBuilder/Source/Viewer`
- Main code areas: `Components/`, `Configuration/`, `Controllers/`, `Models/`, `Providers/`, `Views/Search/`, `Resources/`, and `wwwroot/`.
- Main risk areas: route compatibility, provider contract behavior, view-model shape, result rendering, provider error handling, navbar integration, path resolution, and generated SearchBuilder database compatibility.
- Documentation target: `labs_idemobi_com`

## Standard workflow

1. Update controller, provider, model, component, configuration, view, resource, or path-resolution code.
2. Update XML documentation for touched public APIs.
3. Update `README.md` or information pages when public integration behavior changes.
4. Check whether `/Search/Demo` still covers the changed visual behavior.
5. Hand off for developer-run DocumentationBuilder generation when documentation content changed.

## Manual review focus

- Empty query and empty-result behavior.
- Provider failures and partial results.
- SearchBuilder database path assumptions.
- Documentation provider integration.
- Result URL safety and path resolution.
- Navbar search rendering and accessibility.
- `/Search/Index` and `/Search/Demo` view model expectations.

## Documentation handoff checks

- Documentation structure is extraction-ready.
- Examples are self-contained and realistic.
- Untested search/database generation steps are explicitly stated.

## Commands

- Do not run `dotnet build`, `dotnet test`, `dotnet restore`, or `dotnet format` unless explicitly requested.
- Do not run search index or database generation unless explicitly requested.
