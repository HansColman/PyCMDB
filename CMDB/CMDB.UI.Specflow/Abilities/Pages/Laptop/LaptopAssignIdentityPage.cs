﻿using OpenQA.Selenium;

namespace CMDB.UI.Specflow.Abilities.Pages.Laptop
{
    public class LaptopAssignIdentityPage : MainPage
    {
        public LaptopAssignIdentityPage() : base()
        {
        }
        public void SelectIdentity(Domain.Entities.Identity identity)
        {
            SelectValueInDropDownByXpath("//select[@id='Identity']", $"{identity.IdenId}");
        }
        public AssignFormPage Assign()
        {
            ClickElementByXpath("//button[.='Assign']");
            return new();
        }
    }
}
