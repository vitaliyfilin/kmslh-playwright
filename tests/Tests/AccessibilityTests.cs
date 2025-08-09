using System.Threading.Tasks;
using Kmslh.Tests.Infrastructure;
using Kmslh.Tests.Pages;
using Microsoft.Playwright;
using Xunit;

namespace Kmslh.Tests.Tests;

public sealed class AccessibilityTests : IClassFixture<PlaywrightFixture>
{
    private readonly IPage _page;

    public AccessibilityTests(PlaywrightFixture playwrightFixture)
    {
        _page = playwrightFixture.Page;
    }

    private async Task<AccessibilityWidget> PrepareAccessibilityWidgetAsync()
    {
        var home = new HomePage(_page);
        await home.NavigateAsync();
        await home.OpenAccessibilityWidgetAsync();

        return new AccessibilityWidget(_page);
    }

    [Fact(DisplayName = "'Increase Text' actually increases font size")]
    public async Task IncreaseText_ActuallyIncreasesFontSize()
    {
        var widget = await PrepareAccessibilityWidgetAsync();
        const string dataName = "incfont";
        var clickable = widget.ToggleClickableByDataName(dataName);

        var initialSize = await widget.GetHeadingFontSizeAsync();

        await clickable.ClickAsync();
        await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        var increasedSize = await widget.GetHeadingFontSizeAsync();

        Assert.True(increasedSize > initialSize,
            $"Font size should increase. Was {initialSize}px, now {increasedSize}px.");
    }
}