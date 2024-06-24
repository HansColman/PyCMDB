﻿using Bright.ScreenPlay.Actors;
using CMDB.UI.Specflow.Abilities.Pages.AccountPages;
using Bright.ScreenPlay.Questions;

namespace CMDB.UI.Specflow.Questions.Account
{
    public class OpenTheAccountDetailPage : Question<AccountDetailPage>
    {
        public override AccountDetailPage PerformAs(IPerformer actor)
        {
            var page = actor.GetAbility<AccountOverviewPage>();
            page.ClickElementByXpath(Abilities.Pages.MainPage.InfoXpath);
            return new();
        }
    }
}