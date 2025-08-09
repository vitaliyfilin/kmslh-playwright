namespace Kmslh.Tests.Infrastructure;

public sealed class PlaywrightSettings
{
    public const string Section = "PlaywrightSettings";
    public string BaseAddress { get; set; } = "https://kmslh.com/";
    public bool Headless { get; set; } = true;
    public int TimeoutMs { get; set; } = 30000;
    public string BrowserType { get; set; } = "chromium";
    public float? SlowMo { get; set; } = 0;
}