﻿using CMDB.UI.Specflow.Questions.Account;

namespace CMDB.UI.Specflow.Actors.AccountAcctors
{
    public class AccountActor : CMDBActor
    {
        protected string Table => "account";
        public AccountActor(ScenarioContext scenarioContext, string name = "CMDB") : base(scenarioContext, name)
        {
        }
        public string AccountLastLogLine
        {
            get
            {
                var detail = Perform(new OpenTheAccountDetailPage());
                detail.WebDriver = Driver;
                detail.TakeScreenShot($"{ScenarioContext.ScenarioInfo.Title}_{ScenarioContext.CurrentScenarioBlock}_detail");
                return Perform(new TheAccountDertailLastLogLine());
            }
        }
    }
}
