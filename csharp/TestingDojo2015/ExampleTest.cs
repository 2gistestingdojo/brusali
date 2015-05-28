namespace TestingDojo2015
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    [TestFixture]
    public class ExampleTest : BaseTestFixture
    {
        #region Public Methods and Operators

        [Test]
        public void SearchById()
        {
            var mainWindow = this.Driver.FindElementById("MainWindow");

            var searchString = mainWindow.FindElement(By.Id("QueryMW"));
            searchString.SendKeys("3");

            var searchButton = mainWindow.FindElement(By.Id("SearchMW"));
            searchButton.Click();

            var productsList = mainWindow.FindElement(By.Id("ProductsMW"));
            var productItems = productsList.FindElements(By.ClassName("ListViewItem"));

            Assert.That(productItems.Count, Is.EqualTo(1));
        }

        #endregion
    }
}
