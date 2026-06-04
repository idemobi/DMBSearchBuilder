# DMBSearchViewer Troubleshooting

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchViewer`
- Main troubleshooting areas: empty queries, empty results, provider failures, generated SearchBuilder database paths, documentation-provider integration, route values, navbar rendering, and result URL handling.
- Documentation target: `labs_idemobi_com`

## Search page returns no results

1. Confirm the query text is not empty after trimming.
2. Confirm at least one `IDMBSearchProvider` is registered.
3. Check whether the SearchBuilder database exists at the configured path.
4. Confirm documentation provider dependencies are available when documentation results are expected.
5. Review provider errors returned in the search response.

## Provider errors appear

1. Identify which provider returned the error.
2. Confirm provider configuration paths and dependencies.
3. Keep partial results visible when only one provider fails.
4. Avoid hiding provider failures silently unless the user explicitly asks for that behavior.

## Navbar search is missing

1. Confirm `DMBSearchViewerConfiguration` is registered by the host.
2. Confirm navbar/profile bar integration is active.
3. Check localization keys for placeholder, labels, and actions.
4. Confirm static assets are available when the component depends on them.

## Result links are wrong

1. Check `DMBSearchPathResolver`.
2. Confirm generated database URLs match the host path strategy.
3. Verify documentation-provider URLs use the expected DocumentationViewer route.
4. Treat user-provided or generated URLs as security-sensitive before rendering.

## Documentation output quality is weak

1. Confirm XML documentation explains routes, providers, query models, result models, and error behavior.
2. Avoid copied documentation-viewer or page-builder wording from unrelated packages.
3. Keep examples focused on configuration, provider implementation, queries, and result rendering.
