using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

public class NUnitPlayWright : PageTest//call the playwright library
{
    [SetUp]
    public async Task Setup()
    {
         await Page.GotoAsync(url:"http://www.eaapp.somee.com");//go to url
    }

    [Test]
    public async Task Test1()
    {
            //change to capital p for page
     
            await Page.ClickAsync(selector:"text=login");//click in text
            //take screenshot
            await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "firstImage.jpg"
            }           
            );
            //fill in the username and password
            await Page.FillAsync(selector:"#UserName",value:"admin");//select the username box and fill in the detail
            await Page.FillAsync(selector:"#Password",value:"password");
            await Page.ClickAsync(selector:"text=Log in");
            //check if the employee details exist
            await Expect(Page.Locator(selector:"text='Employee Details'")).ToBeVisibleAsync();
 
    }
}