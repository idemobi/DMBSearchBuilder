# DMBSearchBuilder Project Map

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchBuilder`
- Project root folder: `DMBSearchBuilder`
- Main role: prebuild crawler and SQLite search-index writer.
- Important folders: `Configuration/`, `Resources/`, `wwwroot/css/searchbuilder/`, and `wwwroot/js/searchbuilder/`.
- Documentation target: `labs_idemobi_com`

## Folder responsibilities

- Root files
  - `DMBSearchBuilderAgent.cs`: main orchestration entry point for building the search database.
  - `DMBSearchBuildOptions.cs`: build-time options such as base URI, output path, limits, and excluded paths.
  - `DMBSearchLaunchProfileBuildOptions.cs`: launch-profile-oriented build options.
  - `DMBSearchWebsiteHost.cs`: website crawl host behavior.
  - `DMBSearchPageRecord.cs`: generated page record model.
  - `DMBSearchDatabaseManager.cs`: SQLite database creation and record persistence.
  - `DMBSearchKeywordExtractor.cs`: keyword extraction behavior.
  - `DMBSearchTextNormalizer.cs`: normalization behavior used before keyword extraction.

- `Configuration/`
  - Package configuration and host integration.

- `Resources/`
  - Internal and data-annotation localization `.resx` assets for `DMBSearchBuilder`.

- `wwwroot/`
  - Embedded static assets for search-builder-related scripts and styles.

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
