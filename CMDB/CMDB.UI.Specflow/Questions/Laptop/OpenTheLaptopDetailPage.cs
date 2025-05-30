﻿using Bright.ScreenPlay.Actors;
using Bright.ScreenPlay.Questions;
using CMDB.UI.Specflow.Abilities.Pages;
using CMDB.UI.Specflow.Abilities.Pages.Laptop;

namespace CMDB.UI.Specflow.Questions.Laptop
{
    public class OpenTheLaptopDetailPage : Question<LaptopDetailPage>
    {
        public override LaptopDetailPage PerformAs(IPerformer actor)
        {
            var page = actor.GetAbility<LaptopOverviewPage>();
            page.ClickElementByXpath(MainPage.InfoXpath);
            LaptopDetailPage laptopDetailPage = WebPageFactory.Create<LaptopDetailPage>(page.WebDriver);
            return laptopDetailPage;
        }
    }
}
