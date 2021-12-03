﻿using CMDB.UI.Tests.Hooks;
using CMDB.UI.Tests.Pages;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace CMDB.UI.Tests.Stepdefinitions.AssetType
{
    [Binding]
    public sealed class AssetTypesSteps : TestBase
    {
        private LoginPage login;
        private MainPage main;
        private AssetTypeOverviewPage overviewPage;

        private readonly Random rnd = new();
        private int rndNr;
        private string vendor, type, newtype;
        public AssetTypesSteps(ScenarioData scenarioData) : base(scenarioData)
        {
        }
        [Given(@"I want to create a new (.*) with (.*) and (.*)")]
        public void GivenIWantToCreateANewKensingtonWithKensingtonAndBlack(string category, string vendor, string type)
        {
            rndNr = rnd.Next();
            Url = "https://localhost:44314/";
            ScenarioData.Driver.Navigate().GoToUrl(Url);
            login = new LoginPage(ScenarioData.Driver);
            login.EnterUserID(admin.Account.UserID);
            login.EnterPassword("1234");
            main = login.LogIn();
            overviewPage = main.AssetTypeOverview();
            var newPage = overviewPage.New();
            newPage.Category = category;
            newPage.Vendor = vendor + rndNr.ToString();
            newPage.Type = type + rndNr.ToString();
            this.vendor = vendor + rndNr.ToString();
            this.type = type + rndNr.ToString();
        }
        [When(@"I create that (.*)")]
        public void WhenICreateThatKensington(string category)
        {
            overviewPage.Create();
        }
        [Then(@"The (.*) is created")]
        public void ThenTheIsCreated(string category)
        {
            overviewPage.Search(vendor);
            string expectedlog = $"The {category} type Vendor: {vendor} and type {type} is created by {admin.Account.UserID} in table assettype";
            var detail = overviewPage.Detail();
            var log = detail.GetLastLog();
            Assert.Equal(expectedlog, log);
        }

        [Given(@"There is an AssetType existing")]
        public void GivenThereIsAnAssetTypeExisting()
        {
            Url = "https://localhost:44314/";
            ScenarioData.Driver.Navigate().GoToUrl(Url);
            login = new LoginPage(ScenarioData.Driver);
            login.EnterUserID(admin.Account.UserID);
            login.EnterPassword("1234");
            main = login.LogIn();
            overviewPage = main.AssetTypeOverview();
            overviewPage.Search("kensington");
        }
        [When(@"I change the Type and save the changes")]
        public void WhenIChangeTheTypeAndSaveTheChanges()
        {
            rndNr = rnd.Next();
            newtype = "Orange" + rndNr.ToString();
            var editPage = overviewPage.Edit();
            type = editPage.Type;
            vendor = editPage.Vendor;
            editPage.Type = newtype;
            editPage.Edit();
        }
        [Then(@"The changes are saved")]
        public void ThenTheChangesAreSaved()
        {
            overviewPage.Search("kensington");
            string expectedlog = $"The Type in table assettype has been changed from {type} to {newtype} by {admin.Account.UserID}";
            var detail = overviewPage.Detail();
            var log = detail.GetLastLog();
            Assert.Equal(expectedlog, log);
        }

        [Given(@"There is an active AssetType existing")]
        public void GivenThereIsAnActiveAssetTypeExisting()
        {
            Url = "https://localhost:44314/";
            ScenarioData.Driver.Navigate().GoToUrl(Url);
            login = new LoginPage(ScenarioData.Driver);
            login.EnterUserID(admin.Account.UserID);
            login.EnterPassword("1234");
            main = login.LogIn();
            overviewPage = main.AssetTypeOverview();
            overviewPage.Search("kensington");
        }
        [When(@"I want to deactivate the assettype with reason (.*)")]
        public void WhenIWantToDeactivateTheAssettypeWithReasonTest(string reason)
        {
            var deactivatepage = overviewPage.Deactivate();
            deactivatepage.Reason = reason;
            deactivatepage.Delete();
        }
        [Then(@"the assettype has been deactiveted")]
        public void ThenTheAssettypeHasBeenDeactiveted()
        {
            overviewPage.Search("kensington");
            string expectedlog = $"The Type in table assettype has been changed from {type} to {newtype} by {admin.Account.UserID}";
            var detail = overviewPage.Detail();
            var log = detail.GetLastLog();
            Assert.Equal(expectedlog, log);
        }

        [Given(@"There is an Inactive AssetType existing")]
        public void GivenThereIsAnInactiveAssetTypeExisting()
        {
            Url = "https://localhost:44314/";
            ScenarioData.Driver.Navigate().GoToUrl(Url);
            login = new LoginPage(ScenarioData.Driver);
            login.EnterUserID(admin.Account.UserID);
            login.EnterPassword("1234");
            main = login.LogIn();
            overviewPage = main.AssetTypeOverview();
            overviewPage.Search("kensington");
        }
        [When(@"I want to activate the assettype")]
        public void WhenIWantToActivateTheAssettype()
        {
            overviewPage.Activate();
        }
        [Then(@"the assettype has been activeted")]
        public void ThenTheAssettypeHasBeenActiveted()
        {
            overviewPage.Search("kensington");
            var detail = overviewPage.Detail();
            int id = detail.Id;
            CMDB.Domain.Entities.AssetType assetType = context.GetAssetType(id);
            string expectedlog = $"The {assetType.Category.Category} type Vendor: {assetType.Vendor} and type {assetType.Type} in table assettype is activated by {admin.Account.UserID}";
            var log = detail.GetLastLog();
            Assert.Equal(expectedlog, log);
        }

    }
}