# DMBSearchViewer

## Context

DMBSearchViewer adds a navbar search component and a simple result listing page for generated
site and documentation indexes.

## Explanation

Search results are collected through `IDMBSearchProvider` implementations. The first providers
cover the generated DMBSearchBuilder database and the DMBDocumentationViewer database.

## Example

```csharp
DMBSearchViewerConfiguration.LoadCommonConfig(builder);
DMBSearchViewerConfiguration.UseApp(app);
```

## Notes

The manual preview page is available at `/Search/Demo`.
