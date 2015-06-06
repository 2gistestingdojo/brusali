﻿using System;
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
        public void DeleteItem()
        {
            var productsList = this.MainWindowElement.FindElement(By.Id("ProductsMW"));
            var productItems = productsList.FindElements(By.ClassName("ListViewItem"));
            var size = productItems.Count;
            var randomId = new Random().Next(0, productItems.Count - 1);
            var element = productItems.ElementAt(randomId);
            var elementId = element.FindElements(By.ClassName("TextBlock")).ElementAt(0).GetAttribute("Name");
            var deleteButton = element.FindElement(By.Id("DeleteMW"));

            deleteButton.Click();

            productsList = this.MainWindowElement.FindElement(By.Id("ProductsMW"));
            productItems = productsList.FindElements(By.ClassName("ListViewItem"));
            IWebElement elementToFind = null;

            foreach (var item in productItems)
            {
                var tmp = item.FindElements(By.ClassName("TextBlock")).ElementAt(0).GetAttribute("Name");

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

            var productsList = this.MainWindowElement.FindElement(By.Id("ProductsMW"));
            var productItems = productsList.FindElements(By.ClassName("ListViewItem"));
            var firstElementId =
                productItems.First().FindElements(By.ClassName("TextBlock")).ElementAt(0).GetAttribute("Name");

            sortDownButton.Click();

            productsList = this.MainWindowElement.FindElement(By.Id("ProductsMW"));
            productItems = productsList.FindElements(By.ClassName("ListViewItem"));
            var lastElementId = productItems.Last().FindElements(By.ClassName("TextBlock")).ElementAt(0).GetAttribute("Name");

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

            var productsList = this.MainWindowElement.FindElement(By.Id("ProductsMW"));
            var productItems = productsList.FindElements(By.ClassName("ListViewItem"));
            var lastElementName = productItems.Last().FindElements(By.ClassName("TextBlock")).ElementAt(1).GetAttribute("Name");

            Assert.That(lastElementName, Is.EqualTo(name));
        }
    }
}