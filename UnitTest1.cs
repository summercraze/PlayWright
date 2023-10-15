using Microsoft.Playwright;

namespace PlaywrightTests;

public class Tests
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
            await page.ClickAsync(selector:"text=login");//click in text
            //take screenshot
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "firstImage.jpg"
            }           
            );
            //fill in the username and password
            await page.FillAsync(selector:"#UserName",value:"admin");//select the username box and fill in the detail
            await page.FillAsync(selector:"#Password",value:"password");
            await page.ClickAsync(selector:"text=Log in");
            //check if the employee details exist
            var IsExist = await page.Locator(selector:"text='Employee Details'").IsVisibleAsync();//check if it is visible
            Assert.IsTrue(IsExist);
 
    }
}