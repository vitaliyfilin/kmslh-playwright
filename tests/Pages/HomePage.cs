using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Kmslh.Tests.Pages;

public sealed class HomePage : BasePage
{
    private readonly ILocator _headerBookDemoButton;
    private readonly ILocator _accessibilityWidgetButton;

    public HomePage(IPage page) : base(page)
    {
        _headerBookDemoButton =
            Page.GetByRole(AriaRole.Banner).GetByRole(AriaRole.Link, new()
            {
                Name = "Book a Demo"
            });
        _accessibilityWidgetButton = Page.GetByLabel("Toggle Accessibility Toolbar");
    }

    public Task NavigateAsync() => NavigateAsync("/");

    public async Task ClickHeaderBookDemoAsync()
    {
        await _headerBookDemoButton.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    public async Task OpenAccessibilityWidgetAsync()
    {
        await _accessibilityWidgetButton.ClickAsync();

        await Page.Locator("[class*='acwp-toolbar-active']").WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible
        });
        
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }
}