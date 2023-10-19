using System.Web;
using FluentAssertions;
using FluentAssertions.Equivalency.Steps;
using Microsoft.Playwright;

namespace PlaywrightTests;

public class FlipKart
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
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync(url : "https://www.flipkart.com/", new PageGotoOptions
            {
                WaitUntil = WaitUntilState.NetworkIdle//waiting for network to idle

            });
            await page.Locator(selector:"text=x").ClickAsync();//click x to clost the login
            await page.Locator(selector:"a", new PageLocatorOptions 
            {
              HasTextString = "Login"

            }).ClickAsync();
            //want to check if the tracker login name is displayed
            var response = await page.RunAndWaitForRequestAsync(action: async()=>
            {
                await page.Locator(selector:"text=x").ClickAsync();
            }, urlOrPredicate:x => x.Url.Contains("flipkart.dk.sc.omtrdc.net") && x.Method == "GET");//make sure url contain that
            var returnData = HttpUtility.UrlDecode(response.Url);
            returnData.Should().Contain("Account Login : Displayed Exist");
    }

    [Test]
    public async Task NetworkIntercepton()
    {
            //playwright : will download all the browser package
            using var playwright = await Playwright.CreateAsync();//use await because it is asychronouse, it uses asynchronous fashion of coding
            //open a browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false// will run it in headless mode
            });//launch chrome browser
            //open a page
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.RouteAsync(url :"**/*", handle:async route =>
            {
                if(route.Request.ResourceType == "image")
                   await route.AbortAsync();
                else
                    await route.ContinueAsyc();
            });
            await page.GotoAsync(url : "https://www.flipkart.com/", new PageGotoOptions
            {
                WaitUntil = WaitUntilState.NetworkIdle//waiting for network to idle

            });
           //block image
    }
}

