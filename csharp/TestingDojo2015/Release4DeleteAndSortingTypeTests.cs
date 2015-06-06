using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingDojo2015
{
    using NUnit.Framework;

    using OpenQA.Selenium;

    class Release4DeleteAndSortingTypeTests : BaseTestFixture
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
        public void DeleteItem()
        {
            var productItems = this.GetItemList();
            var size = productItems.Count;
            var randomId = new Random().Next(0, productItems.Count - 1);
            var element = productItems.ElementAt(randomId);
            var elementId = this.GetFieldTextValue(element, 0);
            var deleteButton = element.FindElement(By.Id("DeleteMW"));

            deleteButton.Click();

            productItems = this.GetItemList();
            IWebElement elementToFind = null;

            foreach (var item in productItems)
            {
                var tmp = this.GetFieldTextValue(item, 0);

                if (tmp == elementId)
                {
                    elementToFind = item;
                }
            }

            Assert.That(elementToFind, Is.Null);
            Assert.That(size, Is.EqualTo(productItems.Count + 1));
        }

        [Test]
        public void SortDesc()
        {
            var sortDownButton = this.MainWindowElement.FindElement(By.Id("SortDownMW"));

            var productItems = this.GetItemList();
            var firstElementId = this.GetFieldTextValue(productItems.First(), 0);

            sortDownButton.Click();

            productItems = this.GetItemList();
            var lastElementId = this.GetFieldTextValue(productItems.Last(), 0);

            Assert.That(firstElementId, Is.EqualTo(lastElementId));
        }

        [Test]
        public void SortDescWithElementAdd()
        {
            var sortDownButton = this.MainWindowElement.FindElement(By.Id("SortDownMW"));
            var addNewButton = this.MainWindowElement.FindElement(By.Id("AddNewProductMW"));

            sortDownButton.Click();

            addNewButton.Click();

            var addWindow = this.Driver.FindElementById("AddNewProductWindow");
            var productName = addWindow.FindElement(By.Id("NameAW"));
            var addButton = addWindow.FindElement(By.Id("AddAW"));
            var name = "_Test product 1";

            productName.SendKeys(name);

            addButton.Click();

            var productItems = this.GetItemList();
            var lastElementName = this.GetFieldTextValue(productItems.Last(), 1);

            Assert.That(lastElementName, Is.EqualTo(name));
        }
    }
}
