using CMDB.Domain.Entities;
using CMDB.UI.Specflow.Actors.AssetTypes;
using CMDB.UI.Specflow.Questions;
using AssetType = CMDB.UI.Specflow.Helpers.AssetType;
using Reqnroll;

namespace CMDB.UI.Specflow.StepDefinitions
{
    [Binding]
    public class AssetTypeStepDefinitions : TestBase
    {
        private AssetTypeCreator _assetTypeCreator;
        private AssetTypeUpdator _assetTypeUpdator;
        private AssetType _assetType;
        private Domain.Entities.AssetType AssetType;
        public AssetTypeStepDefinitions(ScenarioContext scenarioContext, ActorRegistry actorRegistry) : base(scenarioContext, actorRegistry)
        {
        }

        [Given(@"I want to create a new (.*) with (.*) and (.*)")]
        public async Task GivenIWantToCreateANewKensingtonWithKensingtonAndBlack(string category, string vendor, string type)
        {
            _assetType = new()
            {
                Category = category,
                Vendor = vendor,
                Type = type
            };
            _assetTypeCreator = new(ScenarioContext);
            ActorRegistry.RegisterActor(_assetTypeCreator);
            Admin = await _assetTypeCreator.CreateNewAdmin();
            _assetTypeCreator.DoLogin(Admin.Account.UserID,"1234");
            var result = _assetTypeCreator.Perform(new IsTheUserLoggedIn());
            result.Should().BeTrue();
            _assetTypeCreator.OpenAssetTypeOverviewPage();
        }
        [When(@"I create that (.*)")]
        public void WhenICreateThatKensington(string category)
        {
            log.Debug($"Gooing to create a {category} type");
            _assetType = _assetTypeCreator.CreateAssetType(_assetType.Category, _assetType.Vendor, _assetType.Type);
        }
        [Then(@"The (.*) is created")]
        public void ThenTheKensingtonIsCreated(string category)
        {
            log.Debug($"Checking if the {category} is created");
            _assetTypeCreator.SearchAssetType(_assetType);
            var lastlog = _assetTypeCreator.AssetTypeLastLogLine;
            _assetTypeCreator.ExpectedLog.Should().BeEquivalentTo(lastlog);
        }
        #region Edit
        [Given(@"There is an AssetType existing")]
        public async Task GivenThereIsAnAssetTypeExisting()
        {
            _assetTypeUpdator = new(ScenarioContext);
            ActorRegistry.RegisterActor(_assetTypeUpdator);
            Admin = await _assetTypeUpdator.CreateNewAdmin();
            AssetType = await _assetTypeUpdator.CreateAssetType();
            _assetTypeUpdator.DoLogin(Admin.Account.UserID, "1234");
            var result = _assetTypeUpdator.Perform(new IsTheUserLoggedIn());
            result.Should().BeTrue();
            _assetTypeUpdator.OpenAssetTypeOverviewPage();
            _assetTypeUpdator.Search(AssetType.Type);
        }
        [When(@"I change the Type and save the changes")]
        public void WhenIChangeTheTypeAndSaveTheChanges()
        {
            AssetType = _assetTypeUpdator.UpdateAssetType(AssetType);
        }
        [Then(@"The changes are saved")]
        public void ThenTheChangesAreSaved()
        {
            _assetTypeUpdator.Search(AssetType.Type);
            var laslog = _assetTypeUpdator.AssetTypeLastLogLine;
            _assetTypeUpdator.ExpectedLog.Should().BeEquivalentTo(laslog);
        }
        #endregion
        #region Activate
        [Given(@"There is an Inactive AssetType existing")]
        public async Task GivenThereIsAnInactiveAssetTypeExisting()
        {
            _assetTypeUpdator = new(ScenarioContext);
            ActorRegistry.RegisterActor(_assetTypeUpdator);
            Admin = await _assetTypeUpdator.CreateNewAdmin();
            AssetType = await _assetTypeUpdator.CreateAssetType(false);
            _assetTypeUpdator.DoLogin(Admin.Account.UserID, "1234");
            var result = _assetTypeUpdator.Perform(new IsTheUserLoggedIn());
            result.Should().BeTrue();
            _assetTypeUpdator.OpenAssetTypeOverviewPage();
            _assetTypeUpdator.Search(AssetType.Type);
        }
        [When(@"I want to activate the assettype")]
        public void WhenIWantToActivateTheAssettype()
        {
            _assetTypeUpdator.ActivateAssetType(AssetType);
        }
        [Then(@"the assettype has been activeted")]
        public void ThenTheAssettypeHasBeenActiveted()
        {
            _assetTypeUpdator.Search(AssetType.Type);
            var laslog = _assetTypeUpdator.AssetTypeLastLogLine;
            _assetTypeUpdator.ExpectedLog.Should().BeEquivalentTo(laslog);
        }
        #endregion
        #region Deactivate
        [Given(@"There is an active AssetType existing")]
        public async Task GivenThereIsAnActiveAssetTypeExisting()
        {
            _assetTypeUpdator = new(ScenarioContext);
            ActorRegistry.RegisterActor(_assetTypeUpdator);
            Admin = await _assetTypeUpdator.CreateNewAdmin();
            AssetType = await _assetTypeUpdator.CreateAssetType();
            _assetTypeUpdator.DoLogin(Admin.Account.UserID, "1234");
            var result = _assetTypeUpdator.Perform(new IsTheUserLoggedIn());
            result.Should().BeTrue();
            _assetTypeUpdator.OpenAssetTypeOverviewPage();
            _assetTypeUpdator.Search(AssetType.Type);
        }
        [When(@"I want to deactivate the assettype with reason (.*)")]
        public void WhenIWantToDeactivateTheAssettypeWithReasonTest(string reason)
        {
            _assetTypeUpdator.DeActivateAssetType(AssetType, reason);
        }
        [Then(@"the assettype has been deactiveted")]
        public void ThenTheAssettypeHasBeenDeactiveted()
        {
            _assetTypeUpdator.Search(AssetType.Type);
            var laslog = _assetTypeUpdator.AssetTypeLastLogLine;
            _assetTypeUpdator.ExpectedLog.Should().BeEquivalentTo(laslog);
        }
        #endregion
    }
}
