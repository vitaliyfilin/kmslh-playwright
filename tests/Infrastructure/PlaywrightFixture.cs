using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Xunit;

namespace Kmslh.Tests.Infrastructure;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class PlaywrightFixture : IAsyncLifetime
{
    private static readonly string ProjectDir = AppContext.BaseDirectory;
    
    private readonly Lazy<Task<IPlaywright>> _playwrightLazy = new(async () => await Playwright.CreateAsync());
    private IBrowser? Browser { get; set; }
    private PlaywrightSettings Settings { get; set; } = new();

    // ReSharper disable once NullableWarningSuppressionIsUsed
    public IPage Page { get; private set; } = null!;

    public async Task InitializeAsync()
    {
#if DEBUG
        var env = "Development";
#else
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
#endif

        var config = BuildConfiguration(env);

        var settings = new PlaywrightSettings();
        config.GetSection(PlaywrightSettings.Section).Bind(settings);
        Settings = settings;

        var playwright = await _playwrightLazy.Value;

        var browserType = Settings.BrowserType.ToLower() switch
        {
            "firefox" => playwright.Firefox,
            "webkit" => playwright.Webkit,
            _ => playwright.Chromium,
        };

        Browser = await browserType.LaunchAsync(new()
        {
            Headless = Settings.Headless,
            SlowMo = Settings.SlowMo
        });

        var context = await Browser.NewContextAsync(new()
        {
            BaseURL = Settings.BaseAddress,
            BypassCSP = true,
            IgnoreHTTPSErrors = true,
            Locale = "en-US"
        }).ConfigureAwait(false);

        Page = await context.NewPageAsync();
        Page.SetDefaultTimeout(Settings.TimeoutMs);
    }

    private static IConfigurationRoot BuildConfiguration(string env)
    {
        return new ConfigurationBuilder()
            .SetBasePath(ProjectDir)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }

    public async Task DisposeAsync()
    {
        if (Browser is not null) await Browser.CloseAsync();

        if (_playwrightLazy.IsValueCreated)
        {
            var playwright = await _playwrightLazy.Value;
            playwright.Dispose();
        }
    }
}