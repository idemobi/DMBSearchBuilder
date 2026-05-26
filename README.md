# DMBSearchBuilder

## Context

DMBSearchBuilder is a prebuilding tool that crawls a website and stores page links, titles,
descriptions, and search keywords in a SQLite database.

## Explanation

The generated database is intended for DMBSearchViewer. The prebuilding integration is configured
directly from the `PreBuilding` project.

By default, the crawler excludes `/Documentation` because DMBSearchViewer already searches the
documentation database through its dedicated DMBDocumentationViewer provider. It also excludes
`/Search` to avoid indexing its own result pages.

## Example

```csharp
await new DMBSearchBuilderAgent().BuildAsync(new DMBSearchBuildOptions
{
    BaseUri = new Uri("http://localhost:5000/"),
    DatabasePath = "Search/data.db",
    MaxPages = 50
});
```

## Notes

The default output path is `Search/data.db` under the target website root.

Configure `ExcludedPathPrefixes` to override excluded paths. Query strings are removed by default
to avoid indexing repeated filter or search states.
