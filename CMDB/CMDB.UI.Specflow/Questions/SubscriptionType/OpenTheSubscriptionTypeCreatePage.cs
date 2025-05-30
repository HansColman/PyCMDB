﻿using Bright.ScreenPlay.Actors;
using Bright.ScreenPlay.Questions;
using CMDB.UI.Specflow.Abilities.Pages;
using CMDB.UI.Specflow.Abilities.Pages.SubscriptionType;

namespace CMDB.UI.Specflow.Questions.SubscriptionType
{
    public class OpenTheSubscriptionTypeCreatePage : Question<CreateSubscriptionTypePage>
    {
        public override CreateSubscriptionTypePage PerformAs(IPerformer actor)
        {
            var page = actor.GetAbility<SubscriptionTypeOverviewPage>();
            page.ClickElementByXpath(MainPage.NewXpath);
            CreateSubscriptionTypePage createSubscriptionTypePage = WebPageFactory.Create<CreateSubscriptionTypePage>(page.WebDriver);
            return createSubscriptionTypePage;
        }
    }
}
