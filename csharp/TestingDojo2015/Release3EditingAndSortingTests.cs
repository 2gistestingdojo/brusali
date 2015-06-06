using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingDojo2015
{
    using System.Threading;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    class Release3EditingAndSortingTests : BaseTestFixture
    {
        public IWebElement SearchTextboxElement;
        public IWebElement SearchButtonElement;

        [SetUp]
        public void SetUp()
        {
            base.SetUp();

            this.SearchTextboxElement = this.MainWindowElement.FindElement(By.Id("QueryMW"));
            this.SearchButtonElement = this.MainWindowElement.FindElement(By.Id("SearchMW"));
        }

        [Test]
        public void EditItem()
        {
            var productItems = this.GetItemList();
            var lastItem = productItems.Last();
            var itemId = this.GetFieldTextValue(lastItem, 0);
            var actions = new Actions(this.Driver);

            actions.DoubleClick(lastItem).Perform();

            var editWindow = this.Driver.FindElementById("ChangeProductWindow");
            var productName = editWindow.FindElement(By.Id("NameCW"));
            var saveButton = editWindow.FindElement(By.Id("SaveCW"));
            var newName = "_Asc sorting test product 1";

            productName.SendKeys(newName);

            saveButton.Click();

            productItems = this.GetItemList();
            var firstItem = productItems.First();
            var id = this.GetFieldTextValue(firstItem, 0);
            var text = this.GetFieldTextValue(firstItem, 1);

            Assert.That(text, Is.EqualTo(newName));
            Assert.That(itemId, Is.EqualTo(id));
        }

        [Test]
        public void AddItemToBegin()
        {
            var addButtonElement = this.MainWindowElement.FindElement(By.Id("AddNewProductMW"));

            addButtonElement.Click();

            var addWindow = this.Driver.FindElementById("AddNewProductWindow");
            var productName = addWindow.FindElement(By.Id("NameAW"));
            var addButton = addWindow.FindElement(By.Id("AddAW"));
            var name = "_Test product 1";

            productName.SendKeys(name);

            addButton.Click();

            var productItems = this.GetItemList();
            var lastItem = productItems.First();
            var text = this.GetFieldTextValue(lastItem, 1);

            Assert.That(text, Is.EqualTo(name));
        }
    }
}
