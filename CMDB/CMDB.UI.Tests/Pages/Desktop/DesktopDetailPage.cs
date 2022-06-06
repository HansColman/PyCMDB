﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDB.UI.Tests.Pages
{
    public class DesktopDetailPage : MainPage
    {
        public DesktopDetailPage(IWebDriver webDriver) : base(webDriver)
        {
        }
        public DesktopReleaseIdentityPage ReleaseIdentity()
        {
            ClickElementByXpath(ReleaseIdenityXpath);
            return new(driver);
        }
        public string GetLastLog()
        {
            ScrollToElement(By.XPath("//h3[.='Log overview']"));
            return TekstFromElementByXpath("//td[contains(text(),'desktop')]");
        }
    }
}
