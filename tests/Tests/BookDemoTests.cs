using System.Threading.Tasks;
using Kmslh.Tests.Infrastructure;
using Kmslh.Tests.Pages;
using Microsoft.Playwright;
using Xunit;

namespace Kmslh.Tests.Tests;

public sealed class BookDemoTests : IClassFixture<PlaywrightFixture>
{
    private readonly IPage _page;

    public BookDemoTests(PlaywrightFixture fixture)
    {
        _page = fixture.Page;
    }

    [Fact(DisplayName = "Validate Demo Form Fields Can Be Filled")]
    public async Task Validate_Demo_Form_Fields_Can_Be_Filled()
    {
        var home = new HomePage(_page);
        await home.NavigateAsync();

        await home.ClickHeaderBookDemoAsync();

        var demo = new DemoPage(_page);
        await demo.EnsureLoadedAsync();

        await demo.FillFormAsync(
            firstName: "Test",
            lastName: "User", 
            email: "test.user@example.com",
            phone: "+1234567890",
            jobTitle: "QA Engineer",
            country: "United States",
            message: "This is a test message for the demo request."
        );

        Assert.Equal("Test", await demo.GetFirstNameValueAsync());
        Assert.Equal("User", await demo.GetLastNameValueAsync());
        Assert.Equal("test.user@example.com", await demo.GetEmailValueAsync());
        Assert.Equal("+1234567890", await demo.GetPhoneValueAsync());
        Assert.Equal("QA Engineer", await demo.GetJobTitleValueAsync());
        Assert.Equal("United States", await demo.GetCountryValueAsync());
        Assert.Equal("This is a test message for the demo request.", await demo.GetMessageValueAsync());

        Assert.True(await demo.IsSubmitButtonEnabledAsync(),
            "Submit button should be enabled after filling required fields");
    }
}