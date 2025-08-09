# KMSLH

## Prerequisites

- [.NET SDK 8.0 or later](https://dotnet.microsoft.com/download)
- [Playwright CLI](https://playwright.dev/dotnet/docs/intro) (optional for playwright tooling)
- (Optional) ReportUnit for converting `.trx` files to HTML reports

## Setup Instructions

### 1. Clone the repository

### 2. Restore dependencies

Restore all NuGet packages:

```bash
dotnet restore
```

### 3. Build the project

Build the project:

```bash
dotnet build
```

### 4. Run tests with TRX logger

Run the tests and generate a `.trx` test results file:

```bash
dotnet test --logger "trx;LogFileName=test_results.trx"
```

This will create a `test_results.trx` file in the test output folder.

---

### 5. (Optional) Convert `.trx` to HTML report

If you want an easy-to-read HTML report, install ReportUnit globally:

```bash
dotnet tool install --global ReportUnit
```

Then convert the `.trx` file to HTML:

```bash
reportunit test_results.trx test_results.html
```

Open `test_results.html` in your browser to view the report.

---

## Notes

- Make sure your browser binaries are installed for Playwright:

```bash
playwright install
```

- Adjust the test project path if necessary.

---

## Troubleshooting

- After building the project, the Playwright installation script `.ps1` can be found in the `bin/Debug/net8.0` (or your target framework) folder.  
  You can install Playwright browsers by running this script in PowerShell:

  ```powershell
  ./playwright.ps1 install
- If ReportUnit is not found, ensure that `.dotnet/tools` is in your PATH or use the full path to the tool.


---

