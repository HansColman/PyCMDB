using CMDB.Domain.Entities;
using CMDB.UI.Specflow.Actors;
using CMDB.UI.Specflow.Questions;
using Reqnroll;

namespace CMDB.UI.Specflow.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions: TestBase
    {
        readonly CMDBActor actor;

        public LoginStepDefinitions(ScenarioContext scenarioContext, ActorRegistry actorRegistry) : base(scenarioContext, actorRegistry)
        {
            actor = new(scenarioContext);
            ActorRegistry.RegisterActor(actor);
        }
        [Given(@"I open the home page")]
        public async Task GivenIOpenTheHomePage()
        {
            Admin = await actor.CreateNewAdmin();
        }
        [When(@"I logon with a valid user name and password")]
        public void WhenILogonWithAValidUserNameAndPassword()
        {
            actor.DoLogin(Admin.Account.UserID, "1234");
        }
        [Then(@"I can logon")]
        public void ThenICanLogon()
        {
            bool result = actor.Perform(new IsTheUserLoggedIn());
            result.Should().BeTrue();
        }
    }
}
