﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.Token
{
    public class CreateTokenPage : MainPage
    {
        public CreateTokenPage(IWebDriver web) : base(web)
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
            set => SelectValueInDropDownByXpath("//select[@id='AssetType']", value);
        }
        public void Create()
        {
            ClickElementByXpath("//button[.='Create']");
        }
    }
}
