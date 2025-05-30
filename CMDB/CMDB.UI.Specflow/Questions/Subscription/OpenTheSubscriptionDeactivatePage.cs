﻿using Bright.ScreenPlay.Actors;
using Bright.ScreenPlay.Questions;
using CMDB.UI.Specflow.Abilities.Pages;
using CMDB.UI.Specflow.Abilities.Pages.Subscription;

namespace CMDB.UI.Specflow.Questions.Subscription
{
    public class OpenTheSubscriptionDeactivatePage : Question<DeactivateSubscriptionPage>
    {
        public override DeactivateSubscriptionPage PerformAs(IPerformer actor)
        {
            var page = actor.GetAbility<SubscriptionOverviewPage>();
            page.ClickElementByXpath(MainPage.DeactivateXpath);
            DeactivateSubscriptionPage deactivateSubscriptionPage = WebPageFactory.Create<DeactivateSubscriptionPage>(page.WebDriver);
            return deactivateSubscriptionPage;
        }
    }
}
