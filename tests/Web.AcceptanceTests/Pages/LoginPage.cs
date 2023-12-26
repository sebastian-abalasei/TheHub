namespace TheHub.Web.AcceptanceTests.Pages;

public class LoginPage : BasePage
{
    public LoginPage(IBrowser browser, IPage page)
    {
        Browser = browser;
        Page = page;
    }

    // public override string PagePath => $"{BaseUrl}/Identity/Account/Login";
    public override string PagePath => $"{BaseUrl}/authentication/login";

    public override IBrowser Browser { get; }

    public override IPage Page { get; set; }

    public Task SetEmail(string email)
    {
        return Page.FillAsync("#email", email);
    }

    public Task SetPassword(string password)
    {
        return Page.FillAsync("#password", password);
    }

    public Task ClickLogin()
    {
        return Page.Locator("#login-submit").ClickAsync();
    }

    public Task<string?> ProfileLinkText()
    {
        return Page.Locator("a[href='/user/dashboard']").TextContentAsync();
    }

    public Task<bool> InvalidLoginAttemptMessageVisible()
    {
        return Page.Locator("text=Invalid login attempt.").IsVisibleAsync();
    }
}
