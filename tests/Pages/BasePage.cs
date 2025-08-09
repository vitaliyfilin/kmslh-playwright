using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Kmslh.Tests.Pages;

public abstract class BasePage
{
    protected readonly IPage Page;

    protected BasePage(IPage page)
    {
        Page = page;
    }

    protected Task NavigateAsync(string url, string? referer = null) =>
        Page.GotoAsync(url, new()
        {
            Referer = referer
        });
}