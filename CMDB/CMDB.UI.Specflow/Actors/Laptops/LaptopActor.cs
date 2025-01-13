﻿using CMDB.UI.Specflow.Questions.Laptop;

namespace CMDB.UI.Specflow.Actors.Laptops
{
    public class LaptopActor : CMDBActor
    {
        protected static string Table => "laptop";
        public LaptopActor(ScenarioContext scenarioContext, string name = "LaptopActor") : base(scenarioContext, name)
        {
        }
        public string LaptopLastLogLine
        {
            get
            {
                var page = Perform(new OpenTheLaptopDetailPage());
                page.WebDriver = Driver;
                return page.GetLastLog();
            }
        }
    }
}
