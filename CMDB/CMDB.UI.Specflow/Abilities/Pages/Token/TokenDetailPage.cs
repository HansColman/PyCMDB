﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.Token
{
    public class TokenDetailPage : MainPage
    {
        public TokenDetailPage(IWebDriver web) : base(web)
        {
        }
        public TokenReleaseIdentityPage ReleaseIdentity()
        {
            ClickElementByXpath(ReleaseIdenityXpath);
            return new(WebDriver);
        }
        public string GetLastLog()
        {
            ScrollToElement(By.XPath("//h3[.='Log overview']"));
            return TekstFromElementByXpath("//td[contains(text(),'token')]");
        }
    }
}
