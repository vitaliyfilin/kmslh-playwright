using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Kmslh.Tests.Pages;

public sealed class DemoPage : BasePage
{
    private readonly ILocator _hubSpotForm;
    private readonly ILocator _submitButton;
    private readonly ILocator _firstNameInput;
    private readonly ILocator _lastNameInput;
    private readonly ILocator _emailInput;
    private readonly ILocator _phoneInput;
    private readonly ILocator _jobTitleInput;
    private readonly ILocator _countrySelect;
    private readonly ILocator _messageTextarea;

    public DemoPage(IPage page) : base(page)
    {
        _hubSpotForm = Page.Locator(".hs-form");
        _submitButton = Page.GetByRole(AriaRole.Button, new() { Name = "Book a demo" });
        _firstNameInput = Page.GetByPlaceholder("First name*");
        _lastNameInput = Page.GetByPlaceholder("Last name*");
        _emailInput = Page.GetByPlaceholder("Professional Email*");
        _phoneInput = Page.GetByPlaceholder("Phone number*");
        _jobTitleInput = Page.GetByPlaceholder("Job Title*");
        _countrySelect = Page.Locator("select[name='country']");
        _messageTextarea = Page.GetByPlaceholder("Message:");
    }

    public async Task EnsureLoadedAsync()
    {
        await _hubSpotForm.WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });
    }

    public async Task FillFormAsync(string firstName, string lastName, string email, string phone, string jobTitle,
        string country, string message = "")
    {
        await FillInputAsync(_firstNameInput, firstName);
        await FillInputAsync(_lastNameInput, lastName);
        await FillInputAsync(_emailInput, email);
        await FillInputAsync(_phoneInput, phone);
        await FillInputAsync(_jobTitleInput, jobTitle);
        await _countrySelect.SelectOptionAsync(country);

        if (!string.IsNullOrEmpty(message))
        {
            await FillInputAsync(_messageTextarea, message);
        }
    }

    private static async Task FillInputAsync(ILocator input, string value)
    {
        await input.FillAsync(value);
    }

    private static async Task<string> GetInputValueAsync(ILocator input)
    {
        return await input.InputValueAsync();
    }

    public Task<string> GetFirstNameValueAsync() => GetInputValueAsync(_firstNameInput);
    public Task<string> GetLastNameValueAsync() => GetInputValueAsync(_lastNameInput);
    public Task<string> GetEmailValueAsync() => GetInputValueAsync(_emailInput);
    public Task<string> GetPhoneValueAsync() => GetInputValueAsync(_phoneInput);
    public Task<string> GetJobTitleValueAsync() => GetInputValueAsync(_jobTitleInput);
    public Task<string> GetCountryValueAsync() => GetInputValueAsync(_countrySelect);
    public Task<string> GetMessageValueAsync() => GetInputValueAsync(_messageTextarea);
    public async Task<bool> IsSubmitButtonEnabledAsync() => await _submitButton.IsEnabledAsync();
}