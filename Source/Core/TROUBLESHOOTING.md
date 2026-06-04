# DMBSearchCore Troubleshooting

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchCore`
- Main troubleshooting area: mismatched search behavior between indexing and querying.

## Search results differ between builder and viewer

- Confirm both packages reference the same `DMBSearchCore` version.
- Confirm provider code uses `DMBSearchTextNormalizer` from `DMBSearchCore`.
- Rebuild and regenerate the search index after normalization changes.

## A technical name does not match compact queries

- Check whether the input is split by PascalCase or acronym boundaries before normalization.
- Compare `NormalizeSearchText(...)` and `CompactSearchText(...)` output.
- Add a shared rule only when it benefits both index-time and query-time behavior.

## A new provider copies normalization logic

- Replace copied logic with `DMBSearchCore.DMBSearchTextNormalizer`.
- Keep provider-specific ranking outside core unless it becomes a shared contract.

## Package restore or signing issues

- Confirm `DMBSearchCore.png` is included as `PackageIcon`.
- Confirm `DMBSearchCore.snk` is present and referenced by `AssemblyOriginatorKeyFile`.
- Confirm downstream signed packages reference a signed `DMBSearchCore` assembly.
