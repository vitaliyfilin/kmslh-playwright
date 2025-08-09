using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Kmslh.Tests.Pages;

public sealed class AccessibilityWidget(IPage page) : BasePage(page)
{
    public ILocator ToggleClickableByDataName(string name) =>
        Page.Locator($".acwp-toggler label[data-name='{name}']");

    public async Task<double> GetHeadingFontSizeAsync()
    {
        var heading = Page.Locator("h1.elementor-heading-title").First;
        await heading.WaitForAsync();

        var computedFontSize = await heading.EvaluateAsync("el => window.getComputedStyle(el).fontSize");
        var match = Regex.Match(computedFontSize.ToString() ?? string.Empty, @"([0-9]+(?:\.[0-9]+)?)px");

        if (match.Success && double.TryParse(match.Groups[1].Value, NumberStyles.Float,
                CultureInfo.InvariantCulture, out var fontSize))
        {
            return fontSize;
        }

        throw new InvalidOperationException("Could not extract font size");
    }
}