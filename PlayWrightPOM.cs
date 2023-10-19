using Microsoft.Playwright;
using PlaywrightTests.Pages;
// using Pages.LoginPage;
namespace PlaywrightTests;

public class PlayWrightPOM
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
            //playwright : will download all the browser package
            using var playwright = await Playwright.CreateAsync();//use await because it is asychronouse, it uses asynchronous fashion of coding
            //open a browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false// will run it in headless mode
            });//launch chrome browser
            //open a page
            var page = await browser.NewPageAsync();//we need the variable across all method so we don't use the using keyword
            await page.GotoAsync(url:"http://www.eaapp.somee.com");//go to url
            LoginPage loginPage = new LoginPage(page);
            await loginPage.ClickLogin();
            await loginPage.Login(username:"admin",password:"password");
            await loginPage.ClickLoginBtn();
            // var isExist = await loginPage.IsEmployeeDetailsExists();
            // Assert.IsTrue(isExist);
            var waitResponse = page.WaitForRequestAsync(urlOrPredicate : "**/Employee");
            await loginPage.ClickEmployeeList();
            var getResponse = await waitResponse;
 
    }
}

