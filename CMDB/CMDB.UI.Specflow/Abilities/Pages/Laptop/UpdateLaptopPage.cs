﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.Laptop
{
    public class UpdateLaptopPage : MainPage
    {
        public UpdateLaptopPage(IWebDriver web) : base(web)
        {
        }
        public string AssetTag
        {
            set => EnterInTextboxByXPath("//input[@id='AssetTag']", value);
            get => TekstFromTextBox("//input[@id='AssetTag']");
        }
        public string SerialNumber
        {
            set => EnterInTextboxByXPath("//input[@id='SerialNumber']", value);
            get => TekstFromTextBox("//input[@id='SerialNumber']");
        }
        public string Type
        {
            set => SelectValueInDropDownByXpath("//select[@id='AssetType_TypeID']", "7");
            get => GetSelectedValueFromDropDownByXpath("//select[@id='AssetType_TypeID']");
        }
        public string RAM
        {
            set => SelectTektInDropDownByXpath("//select[@id='RAM']", value);
            get => GetSelectedValueFromDropDownByXpath("//select[@id='RAM']");
        }
        public string MAC
        {
            set => EnterInTextboxByXPath("//input[@id='MAC']", value);
            get => TekstFromTextBox("//input[@id='MAC']");
        }
        public void Edit()
        {
            ClickElementByXpath("//button[.='Edit']");
            WaitOnAddNew();
        }
    }
}
