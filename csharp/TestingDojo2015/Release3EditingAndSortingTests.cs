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
        public void EditItem()
        {
            var productsList = this.MainWindowElement.FindElement(By.Id("ProductsMW"));
            var productItems = productsList.FindElements(By.ClassName("ListViewItem"));
            var lastItem = productItems.Last();
            var itemId = lastItem.FindElements(By.ClassName("TextBlock")).First().GetAttribute("Name");
            var actions = new Actions(this.Driver);

            actions.DoubleClick(lastItem).Perform();

            var editWindow = this.Driver.FindElementById("ChangeProductWindow");
            var productName = editWindow.FindElement(By.Id("NameCW"));
            var saveButton = editWindow.FindElement(By.Id("SaveCW"));
            var newName = "_Asc sorting test product 1";

            productName.SendKeys(newName);

            saveButton.Click();

            productItems = productsList.FindElements(By.ClassName("ListViewItem"));
            var firstItem = productItems.First();
            var texts = firstItem.FindElements(By.ClassName("TextBlock"));
            var id = texts.First();
            var text = texts.Last();

            Assert.That(text.GetAttribute("Name"), Is.EqualTo(newName));
            Assert.That(itemId, Is.EqualTo(id.GetAttribute("Name")));
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

            var productsList = this.MainWindowElement.FindElement(By.Id("ProductsMW"));
            var productItems = productsList.FindElements(By.ClassName("ListViewItem"));
            var lastItem = productItems.First();
            var texts = lastItem.FindElements(By.ClassName("TextBlock"));
            var text = texts.Last();

            Assert.That(text.GetAttribute("Name"), Is.EqualTo(name));
        }
    }
}
