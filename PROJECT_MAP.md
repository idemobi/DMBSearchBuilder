# DMBSearchBuilder Project Map

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchBuilder`
- Project root folder: `DMBSearchBuilder`
- Builder source folder: `Source/Builder`
- Core source folder: `Source/Core`
- Viewer source folder: `Source/Viewer`
- Main role: prebuild crawler and SQLite search-index writer.
- Important folders: `Source/Builder/`, `Source/Core/`, and `Source/Viewer/`.
- Labs folder: `Labs/`
- Unit test folders: `UnitTests/Builder/`, `UnitTests/Core/`, and `UnitTests/Viewer/`.
- Documentation target: `labs_idemobi_com`

## Folder responsibilities

- `Source/Builder/`
  - `DMBSearchBuilder.csproj`: package project file.
  - `SearchBuilderAgent.cs`: main orchestration entry point for building the search database.
  - `SearchBuildOptions.cs`: build-time options such as base URI, output path, limits, and excluded paths.
  - `SearchLaunchProfileBuildOptions.cs`: launch-profile-oriented build options.
  - `SearchWebsiteHost.cs`: website crawl host behavior.
  - `SearchPageRecord.cs`: generated page record model.
  - `SearchDatabaseManager.cs`: SQLite database creation and record persistence.
  - `SearchKeywordExtractor.cs`: keyword extraction behavior.
  - `SearchTextNormalizer.cs`: normalization behavior used before keyword extraction.
  - `README.md`, `LICENSE.md`, `DMBSearchBuilder.png`, and `DMBSearchBuilder.snk`: package metadata and signing assets.

- `Source/Builder/Resources/`
  - Internal and data-annotation localization `.resx` assets for `DMBSearchBuilder`.

- `Source/Builder/wwwroot/`
  - Embedded static assets for search-builder-related scripts and styles.

- `Source/Core/`
  - Shared search normalization package, package metadata, and package-specific AI/documentation context.

- `Source/Viewer/`
  - Runtime search UI package, Razor views, resources, providers, package metadata, and package-specific AI/documentation context.

- `Labs/`
  - Razor project that provides SearchBuilder and SearchViewer documentation controllers, views, and local view helper extensions for `labs_idemobi_com`.

- `UnitTests/`
  - Search family unit tests grouped by Builder, Core, and Viewer package.

- `bin/` and `obj/`
  - Build outputs and intermediate files. Do not use these folders as documentation or source-of-truth inputs.

## Documentation-related files

- `Source/Builder/README.md`: package overview and usage context.
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
