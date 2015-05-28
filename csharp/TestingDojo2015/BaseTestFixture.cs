namespace TestingDojo2015
{
    #region using

    using System;
    using System.IO;

    using NUnit.Framework;

    using OpenQA.Selenium.Remote;

    #endregion

    public class BaseTestFixture
    {
        #region Public Properties

        public RemoteWebDriver Driver { get; set; }

        #endregion

        #region Public Methods and Operators

        [SetUp]
        public void SetUp()
        {
            var appsFolder = Environment.GetEnvironmentVariable("UITestApps");
            var appPath = Path.Combine(appsFolder ?? "C:\\app", "TestingDojo2015.exe");
            var dc = new DesiredCapabilities();
            dc.SetCapability("app", appPath);
            this.Driver = new RemoteWebDriver(new Uri("http://127.0.0.1:9999"), dc);
        }

        [TearDown]
        public void TearDown()
        {
            this.Driver.Quit();
        }

        #endregion
    }
}
