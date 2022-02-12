﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDB.UI.Tests.Pages
{
    public class CreateDesktopPage : MainPage
    {
        public CreateDesktopPage(IWebDriver webDriver) : base(webDriver)
        {
        }
        public string AssetTag
        {
            set => EnterInTextboxByXPath("//input[@id='AssetTag']", value);
        }
        public string SerialNumber
        {
            set => EnterInTextboxByXPath("//input[@id='SerialNumber']", value);
        }
        public string Type
        {
            set => SelectValueInDropDownByXpath("//select[@id='Type']", value);
        }
        public string RAM
        {
            set => SelectTektInDropDownByXpath("//select[@id='RAM']", value);
        }
        public string MAC
        {
            set => EnterInTextboxByXPath("//input[@id='MAC']", value);
        }
        public void Create()
        {
            ClickElementByXpath("//button[.='Create']");
        }
    }
}
