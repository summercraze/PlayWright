using Microsoft.Playwright;

namespace PlaywrightTests.Pages;
public class LoginPage
{
    private static IPage _page;
    private readonly ILocator _lnkLogin;
    private readonly ILocator _txtUsername;
    private readonly ILocator _txtPassword;
    private readonly ILocator _btnLogin;
    private readonly ILocator _lnkEmployeeDetails;
    private readonly ILocator _employeeList;
    public LoginPage(IPage page)//IPage responsible for the locator
    {
         _page = page;
         _lnkLogin = _page.Locator(selector:"text=login");//initialize all the values of the constructor of locator
         _txtUsername = _page.Locator(selector:"#UserName");
         _txtPassword = _page.Locator(selector:"#Password");
         _btnLogin = _page.Locator(selector:"text=Log in");
         _lnkEmployeeDetails = _page.Locator(selector:"text='Employee Details'");
         _employeeList = _page.Locator(selector:"text='Employee List'");

    }

//     public async Task ClickLogin() => await _lnkLogin.ClickAsync(); //=> expression bodied member
    public async Task ClickLogin()
    {
     await _page.RunAndWaitForNavigationAsync(action: async() =>//wait for something happen sync up
     {
          await _lnkLogin.ClickAsync();//when login 
     }, new PageRunAndWaitForNavigationOptions//wait for the navigation to go the URL
     {
          UrlString = "**/Login"
     });
    } //when you click the login
    public async Task Login(string username, string password)//login method that allow you to enter password and username
    {
         await _txtUsername.FillAsync(username);
         await _txtPassword.FillAsync(password);
    }

    public async Task ClickLoginBtn() => await _btnLogin.ClickAsync(); //=> expression bodied member
    public async Task<bool> IsEmployeeDetailsExists() => await _lnkEmployeeDetails.IsVisibleAsync(); //method that make sure the button is visible

    public async Task ClickEmployeeList() => await _employeeList.ClickAsync(); 
}