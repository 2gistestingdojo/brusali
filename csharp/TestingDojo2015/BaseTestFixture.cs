namespace TestingDojo2015
{
    #region using

    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    #endregion

    public class BaseTestFixture
    {
        #region Public Properties

        public RemoteWebDriver Driver { get; set; }

        public IWebElement MainWindowElement;

        #endregion

        #region Public Methods and Operators

        public void SetUp()
        {
            var appsFolder = Environment.GetEnvironmentVariable("UITestApps");
            var appPath = Path.Combine(appsFolder ?? "C:\\app", "TestingDojo2015.exe");
            var dc = new DesiredCapabilities();
            dc.SetCapability("app", appPath);
            this.Driver = new RemoteWebDriver(new Uri("http://127.0.0.1:9999"), dc);

            this.MainWindowElement = this.Driver.FindElementById("MainWindow");
        }

        [TearDown]
        public void TearDown()
        {
            this.Driver.Quit();
        }

        protected ReadOnlyCollection<IWebElement> GetItemList()
        {
            var productsList = this.MainWindowElement.FindElement(By.Id("ProductsMW"));
            var productItems = productsList.FindElements(By.ClassName("ListViewItem"));

            return productItems;
        }

        protected string GetFieldTextValue(IWebElement element, int pos)
        {
            return element.FindElements(By.ClassName("TextBlock")).ElementAt(pos).GetAttribute("Name");
        }

        #endregion
    }
}
