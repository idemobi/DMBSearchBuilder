# DMBSearchCore Project Map

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchCore`
- Project folder: `DMBSearchBuilder/Source/Core`
- Main role: shared search normalization and tokenization primitives.
- Important files: `SearchTextNormalizer.cs`, `DMBSearchCore.csproj`, and `README.md`.
- Documentation target: `labs_idemobi_com`

## Folder responsibilities

- `Source/Core/`
  - `SearchTextNormalizer.cs`: shared normalization, token extraction, and compact technical-name comparison helpers.
  - `DMBSearchCore.csproj`: package metadata, signing, and NuGet packaging settings.
  - `DMBSearchCore.png`: NuGet package icon.
  - `README.md`: package overview and usage context.

- `bin/` and `obj/`
  - Build outputs and intermediate files. Do not use these folders as documentation or source-of-truth inputs.

## Documentation-related files

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
