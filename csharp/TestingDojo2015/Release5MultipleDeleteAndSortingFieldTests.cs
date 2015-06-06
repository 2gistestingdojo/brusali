using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingDojo2015
{
    using System.Diagnostics;

    using NUnit.Framework;

    using OpenQA.Selenium;

    class Release5MultipleDeleteAndSortingFieldTests : BaseTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            base.SetUp();
        }

        [Test]
        public void MultipleSelect()
        {
            var deleteSelectedButton = this.MainWindowElement.FindElement(By.Id("DeleteSelectedMW"));
            var isHidden = Convert.ToBoolean(deleteSelectedButton.GetAttribute("IsOffscreen"));

            Assert.That(isHidden, Is.True);

            var productItems = this.GetItemList();
            var randomId = new Random().Next(1, productItems.Count - 1);
            var element = productItems.ElementAt(randomId);
            var firstElement = productItems.First();

            firstElement.Click();
            this.Driver.ExecuteScript("input: ctrl_click", element);

            isHidden = Convert.ToBoolean(deleteSelectedButton.GetAttribute("IsOffscreen"));

            Assert.That(isHidden, Is.False);
        }

        [Test]
        public void MultipleDelete()
        {
            var deleteSelectedButton = this.MainWindowElement.FindElement(By.Id("DeleteSelectedMW"));

            var productItems = this.GetItemList();
            var size = productItems.Count;
            var randomId = new Random().Next(1, productItems.Count - 1);
            var element = productItems.ElementAt(randomId);
            var firstElement = productItems.First();

            firstElement.Click();
            this.Driver.ExecuteScript("input: ctrl_click", element);

            var firstId = this.GetFieldTextValue(firstElement, 0);
            var id = this.GetFieldTextValue(element, 0);

            deleteSelectedButton.Click();

            productItems = this.GetItemList();
            IWebElement elementToFind = null;

            foreach (var item in productItems)
            {
                var tmp = this.GetFieldTextValue(item, 0);

                if (tmp == firstId || tmp == id)
                {
                    elementToFind = item;
                }
            }

            Assert.That(elementToFind, Is.Null);
            Assert.That(size, Is.EqualTo(productItems.Count + 2));
        }

        [Test]
        public void SortDesc()
        {
            var sortDownButton = this.MainWindowElement.FindElement(By.Id("SortDownMW"));
            var sortFieldSelect = this.MainWindowElement.FindElement(By.Id("SortByMW"));

            sortFieldSelect.Click();

            var sortById = sortFieldSelect.FindElements(By.ClassName("ListBoxItem")).ElementAt(0);

            sortById.Click();

            var productItems = this.GetItemList();
            var firstElementId = this.GetFieldTextValue(productItems.First(), 0);

            sortDownButton.Click();

            productItems = this.GetItemList();
            var lastElementId = this.GetFieldTextValue(productItems.Last(), 0);

            Assert.That(firstElementId, Is.EqualTo(lastElementId));
        }

        [Test]
        public void SortAscWithElementAdd()
        {
            var sortFieldSelect = this.MainWindowElement.FindElement(By.Id("SortByMW"));

            sortFieldSelect.Click();

            var sortById = sortFieldSelect.FindElements(By.ClassName("ListBoxItem")).ElementAt(0);

            sortById.Click();

            var addNewButton = this.MainWindowElement.FindElement(By.Id("AddNewProductMW"));

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

            var prevId = 0;

            foreach (var item in productItems)
            {
                var tmp = Convert.ToInt32(this.GetFieldTextValue(item, 0));

                Assert.That(prevId, Is.LessThan(tmp));

                prevId = tmp;
            }
        }
    }
}
