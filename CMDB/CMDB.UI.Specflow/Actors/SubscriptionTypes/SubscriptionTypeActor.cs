﻿using CMDB.UI.Specflow.Questions.SubscriptionType;

namespace CMDB.UI.Specflow.Actors.SubscriptionTypes
{
    public class SubscriptionTypeActor : CMDBActor
    {
        protected static string Table => "subscriptiontype";
        public SubscriptionTypeActor(ScenarioContext scenarioContext, string name = "SubscriptionType") : base(scenarioContext, name)
        {
        }
        public string LastLogLine
        {
            get
            {
                var page = Perform(new OpenTheSubscriptionTypeDetailPage());
                page.WebDriver = Driver;
                return page.GetLastLog();
            }
        }

    }
}
