# KMSLH Playwright xUnit Tests (sample)

This repository contains a Playwright (.NET) xUnit test project for **https://kmslh.com/**.
Solution targets **.NET 8.0** and follows SOLID, DRY, KISS, YAGNI and Object Calisthenics principles with a Page Object Model.

## What's included
- xUnit test project (`tests/`) with Page Objects.
- `appsettings.json` + environment-specific overrides (`appsettings.Development.json`, `appsettings.Production.json`).
- GitHub Actions CI (`.github/workflows/ci.yml`) with Playwright browser installation.
- `Dockerfile` for containerized test runs.
- Manual test cases documented in `tests/ManualTestCases.md`.

## How to run locally (high-level)
1. Install .NET 8 SDK.
2. Restore packages: `dotnet restore tests/Kmslh.Tests.csproj`
3. Install Playwright browsers: `npx playwright install --with-deps` or use the GitHub Action step.
4. Run tests: `dotnet test tests/Kmslh.Tests.csproj`

Note: In DEBUG environment (local dev) the configuration will target `appsettings.Development.json` and use `LocalBaseAddress` if provided.
