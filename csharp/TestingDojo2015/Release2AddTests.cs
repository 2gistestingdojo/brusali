using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingDojo2015
{
    using NUnit.Framework;

    using OpenQA.Selenium;

    class Release2AddTests : BaseTestFixture
    {
        public IWebElement MainWindowElement;
        public IWebElement SearchTextboxElement;
        public IWebElement SearchButtonElement;
        public IWebElement AddButtonElement;

        [SetUp]
        public void SetUp()
        {
            base.SetUp();

            this.MainWindowElement = this.Driver.FindElementById("MainWindow");
            this.SearchTextboxElement = this.MainWindowElement.FindElement(By.Id("QueryMW"));
            this.SearchButtonElement = this.MainWindowElement.FindElement(By.Id("SearchMW"));
            this.AddButtonElement = this.MainWindowElement.FindElement(By.Id("AddNewProductMW"));
        }

        public void AddItem()
        {
            this.AddButtonElement.Click();

            var addWindow = this.Driver.FindElementById("AddNewProductWindow");
            var productName = addWindow.FindElement(By.Id("NameAW"));
            var addButton = addWindow.FindElement(By.Id("AddAW"));

            productName.SendKeys("Test product 1");

            addButton.Click();

            var productsList = this.MainWindowElement.FindElement(By.Id("ProductsMW"));
            var productItems = productsList.FindElements(By.ClassName("ListViewItem"));
            var lastItem = productItems[productItems.Count - 1];

            Assert.That(lastItem.Text, Is.EqualTo("Test product 1"));
        }
    }
}
