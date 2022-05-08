﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDB.UI.Tests.Pages
{
    public class ReleaseIdentityPage : MainPage
    {
        public string UserId => GetAttributeFromXpath("//td[@id='UserId']", "innerHTML");
        public string Type => GetAttributeFromXpath("//td[@id='Type']", "innerHTML");
        public string Application => GetAttributeFromXpath("//td[@Application]", "innerHTML");
        public string IdentityName => GetAttributeFromXpath("//td[@id='IdentityName']", "innerHTML");
        public string IdentityUserId => GetAttributeFromXpath("//td[@id='IdentityUserId']", "innerHTML");
        public string IdentityEMail => GetAttributeFromXpath("//td[@id='IdentityEMail']", "innerHTML");

        public string Employee
        {
            set => EnterInTextboxByXPath("//input[@id='Employee']", value);
            get => TekstFromTextBox("//input[@id='Employee']");
        }
        public string ITEmployee
        {
            set => EnterInTextboxByXPath("//input[@id='ITEmp']", value);
            get => TekstFromTextBox("//input[@id='ITEmp']");
        }
        public ReleaseIdentityPage(IWebDriver webDriver) : base(webDriver)
        {
        }
        public void CreatePDF()
        {
            ClickElementByXpath("//button[@type='submit']");
            WaitOnAddNew();
        }
    }
}
