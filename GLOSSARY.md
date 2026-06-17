# DMBSearchBuilder Glossary

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBSearchBuilder`
- Project role: prebuild crawler and generated search-index writer.
- Publication host: `labs_idemobi_com`

## DMBSearchBuilder

Package that crawls a website and stores searchable page records in a SQLite database consumed by `DMBSearchViewer`.

## Search Index

Generated SQLite database containing page URLs, titles, descriptions, and keywords.

## Page Record

Stored representation of one crawled page, usually modeled by `SearchPageRecord`.

## Crawl Scope

The set of URLs allowed by the base URI, path normalization rules, maximum page count, and excluded path prefixes.

## Excluded Path Prefix

Path prefix skipped by the crawler. Defaults include `/Documentation` and `/Search` to avoid duplicate documentation search and self-indexed search results.

## Text Normalization

Process that converts crawled page text into a stable representation before keyword extraction.

## Keyword Extraction

Process that selects searchable terms from normalized page content for later querying by `DMBSearchViewer`.

## DocumentationBuilder-first

Documentation approach where XML documentation and markdown are prepared so DocumentationBuilder can extract and render them into `labs_idemobi_com`.
