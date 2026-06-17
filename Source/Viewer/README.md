# DMBSearchViewer

## Context

DMBSearchViewer adds a navbar search component and a simple result listing page for generated
site and documentation indexes.

## Explanation

Search results are collected through `ISearchProvider` implementations. The first providers
cover the generated DMBSearchBuilder database and the DMBDocumentationViewer database.

## Example

```csharp
SearchViewerConfiguration.LoadCommonConfig(builder);
SearchViewerConfiguration.UseApp(app);
```

## Notes

The manual preview page is available at `/Search/Demo`.
