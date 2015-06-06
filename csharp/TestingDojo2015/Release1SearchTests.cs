namespace TestingDojo2015
{
    #region using

    using System.Diagnostics;

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    [TestFixture]
    public class Release1SearchTests : BaseTestFixture
    {
        #region Public Methods and Operators

        public IWebElement MainWindowElement;
        public IWebElement SearchTextboxElement;
        public IWebElement SearchButtonElement;

        [SetUp]
        public void SetUp()
        {
            base.SetUp();

            this.MainWindowElement = this.Driver.FindElementById("MainWindow");
            this.SearchTextboxElement = this.MainWindowElement.FindElement(By.Id("QueryMW"));
            this.SearchButtonElement = this.MainWindowElement.FindElement(By.Id("SearchMW"));
        }

        [Test]
        public void SearchById()
        {
            this.SearchTextboxElement.SendKeys("3");
            this.SearchButtonElement.Click();

            Assert.That(this.GetItemsCount(), Is.EqualTo(1));
        }

        [Test]
        public void SearchByNameCaseSensetive()
        {
            this.SearchTextboxElement.SendKeys("Принтер");
            this.SearchButtonElement.Click();

            Assert.That(this.GetItemsCount(), Is.EqualTo(1));
        }

        [Test]
        public void SearchByNameCaseInsensetive()
        {
            this.SearchTextboxElement.SendKeys("ПрИнТЕР");
            this.SearchButtonElement.Click();

            Assert.That(this.GetItemsCount(), Is.EqualTo(1));
        }

        [Test]
        public void SearchByIdWithEnter()
        {
            this.SearchTextboxElement.SendKeys("3");
            this.SearchTextboxElement.Submit();

            Assert.That(this.GetItemsCount(), Is.EqualTo(1));
        }

        #endregion

#region Private Methods and Operators

        private int GetItemsCount()
        {
            var productsList = this.MainWindowElement.FindElement(By.Id("ProductsMW"));
            var productItems = productsList.FindElements(By.ClassName("ListViewItem"));

            return productItems.Count;
        }
#endregion
    }
}
