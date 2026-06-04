# DMBSearchViewer Project Map

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchViewer`
- Project folder: `DMBSearchBuilder/Source/Viewer`
- Main role: runtime search UI and provider aggregation package.
- Important folders: `Components/`, `Configuration/`, `Controllers/`, `Models/`, `Providers/`, `Resources/`, `Views/Search/`, and `wwwroot/`.
- Documentation target: `labs_idemobi_com`

## Folder responsibilities

- `Source/Viewer/Components/`
  - Reusable navbar search component integration.

- `Source/Viewer/Configuration/`
  - Package configuration and host application registration.

- `Source/Viewer/Controllers/`
  - Runtime search routes.
  - `SearchController` renders the main search page and manual demo page.

- `Source/Viewer/Models/`
  - Query, response, result, provider error, options, and page view models.

- `Source/Viewer/Providers/`
  - Search source extension points and built-in providers.
  - Includes composite aggregation, SearchBuilder database provider, DocumentationViewer provider, navbar provider integration, and path resolution.

- `Source/Viewer/Resources/`
  - Internal and data-annotation localization `.resx` assets for `DMBSearchViewer`.

- `Source/Viewer/Views/Search/`
  - Embedded Razor views for search result and demo pages.

- `Source/Viewer/wwwroot/`
  - Embedded static assets for search viewer styling and behavior.

- `bin/` and `obj/`
  - Build outputs and intermediate files. Do not use these folders as documentation or source-of-truth inputs.

## Documentation-related files

- `README.md`: package overview and usage context.
- `AGENTS.md`: local AI rules and scope for this package.
- `AI_CONTEXT.md`: additional context for AI-assisted maintenance.
- `DOCUMENTATION_RULES.md`: strict documentation policy.
- `DRAWIO_DIAGRAM_RULES.md`: rules for editable Draw.io diagrams used by documentation, concept, instruction, example, and tutorial pages.
- `EXAMPLES_AND_TUTORIALS_RULES.md`: rules for example pages and tutorials only.
- `DELIVERY_CHECKLIST.md`: final quality gate before handoff.
- `ARCHITECTURE_DECISIONS.md`: local architecture decisions and constraints.
- `GLOSSARY.md`: shared vocabulary for this package.
- `LOCAL_DEVELOPMENT_RUNBOOK.md`: local workflow notes and handoff checks.
- `LOCALIZATION_NOMENCLATURE.md`: localization key naming rules for this package.
- `TROUBLESHOOTING.md`: known issues and recovery notes.
