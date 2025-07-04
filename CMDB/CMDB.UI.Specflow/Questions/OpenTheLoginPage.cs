﻿using Bright.ScreenPlay.Actors;
using Bright.ScreenPlay.Questions;
using CMDB.UI.Specflow.Abilities.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace CMDB.UI.Specflow.Questions
{
    public class OpenTheLoginPage : Question<LoginPage>
    {
        public override LoginPage PerformAs(IPerformer actor)
        {
            var options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            //options.AddArgument("-headless");
            options.AddArgument("-disable-extensions");
            options.AddArgument("-disable-dev-shm-usage");
            options.AddArgument("-no-sandbox");
            IWebDriver webDriver = new FirefoxDriver(options);
            var page = WebPageFactory.Create<LoginPage>(webDriver);
            //page.WebDriver = webDriver;
            page.WebDriver.Navigate().GoToUrl(page.Settings.BaseUrl);
            page.WebDriver.Manage().Window.Maximize();
            return page;
        }
    }
}
