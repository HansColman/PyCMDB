﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.Monitor
{
    public class MonitorDetailPage : MainPage
    {
        public MonitorDetailPage(IWebDriver webDriver) : base(webDriver)
        {
        }
        public MonitorReleaseIdentityPage ReleaseIdentityPage()
        {
            ClickElementByXpath(ReleaseIdenityXpath);
            return new(WebDriver);
        }
        public string GetLastLog()
        {
            ScrollToElement(By.XPath("//h3[.='Log overview']"));
            return TekstFromElementByXpath("//td[contains(text(),'screen')]");
        }
    }
}
