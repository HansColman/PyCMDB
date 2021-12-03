﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using CMDB.UI.Tests.Data;
using System.IO;
using System;

namespace CMDB.UI.Tests.Hooks
{
    /// <summary>
    /// This is the hooks class For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
    /// </summary>
    [Binding]
    public sealed class Hooks
    {
        /// <summary>
        /// The Nlog logger
        /// </summary>
        private readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// This function will run before evry scenario
        /// </summary>
        /// <param name="context">The Scenario context</param>
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext context)
        {
            log.Debug("Scenario {0} started", context.ScenarioInfo.Title);
        }
        /// <summary>
        /// This funtion will runn after evry scenario
        /// </summary>
        /// <param name="context"></param>
        [AfterScenario]
        public void AfterScenario(ScenarioContext context)
        {
            log.Debug("Scenario {0} stoped", context.ScenarioInfo.Title);
        }
        /// <summary>
        /// This function will run before each Feature
        /// </summary>
        /// <param name="scenarioData">The data of the scenario</param>
        [BeforeFeature]
        public static void BeforeFeature(ScenarioData scenarioData)
        {
            scenarioData.Context = new DataContext();
            scenarioData.Admin = scenarioData.Context.CreateNewAdmin();
            /*var options = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };
            //options.AddArgument("-headless");
            IWebDriver webDriver = new FirefoxDriver(options);*/
            var options = new ChromeOptions()
            {
                AcceptInsecureCertificates = true
            };
            IWebDriver webDriver = new ChromeDriver(options);
            scenarioData.Driver = webDriver;
            scenarioData.Driver.Manage().Window.Maximize();
        }
        /// <summary>
        /// This function will runn after each feature
        /// </summary>
        /// <param name="scenarioData">The data of the scenario</param>
        [AfterFeature]
        public static void AfterFeature(ScenarioData scenarioData)
        {
            scenarioData.Driver.Close();
            scenarioData.Driver.Quit();
            scenarioData.Context = null;
        }
        [AfterStep]
        public void AfterStep(ScenarioData scenarioData, ScenarioContext context)
        {
            var result = context.StepContext.Status;
            if (result == ScenarioExecutionStatus.TestError)
            {
                log.Error("The scenario {0} ended with {1}", context.ScenarioInfo.Title, result);
                //there was an error lets take a screenshot
                ITakesScreenshot takesScreenshot = (ITakesScreenshot)scenarioData.Driver;
                var screenshot = takesScreenshot.GetScreenshot();
                var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "");
                string fileName = $"{context.ScenarioInfo.Title}_{DateTime.Now:yyyy-MM-dd'T'HH-mm-ss}.png";
                string tempFileName = Path.Combine(path, @"../../../Screenshots/", fileName);

                screenshot.SaveAsFile(tempFileName, ScreenshotImageFormat.Png);
                log.Debug("Screenshot saved: {0}", tempFileName);
            }
        }
    }
}