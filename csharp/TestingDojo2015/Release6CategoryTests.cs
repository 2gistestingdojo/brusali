using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingDojo2015
{
    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    class Release6CategoryTests : BaseTestFixture
    {

        [SetUp]
        public void SetUp()
        {
            base.SetUp();
        }

        [Test]
        public void AddItemWithCategory()
        {
            var addButtonElement = this.MainWindowElement.FindElement(By.Id("AddNewProductMW"));
            var productItems = this.GetItemList();
            var maxId = 0;

            foreach (var item in productItems)
            {
                var tmp = Convert.ToInt32(this.GetFieldTextValue(item, 0));

                if (tmp > maxId)
                {
                    maxId = tmp;
                }
            }

            addButtonElement.Click();

            var addWindow = this.Driver.FindElementById("AddNewProductWindow");
            var productName = addWindow.FindElement(By.Id("NameAW"));
            var addButton = addWindow.FindElement(By.Id("AddAW"));
            var categorySelect = addWindow.FindElement(By.Id("CategoryAW"));
            var name = "Test product 1";

            categorySelect.Click();

            var categories = categorySelect.FindElements(By.ClassName("ListBoxItem"));
            var randomId = new Random().Next(0, categories.Count - 1);
            var category = categories.ElementAt(randomId);

            Assert.That(category, Is.Not.Null);

            var categoryText = category.GetAttribute("Name");

            category.Click();

            productName.SendKeys(name);

            addButton.Click();

            productItems = this.GetItemList();
            IWebElement element = null;

            foreach (var item in productItems)
            {
                var tmp = this.GetFieldTextValue(item, 1);

                if (tmp == name)
                {
                    element = item;
                }
            }

            Assert.That(element, Is.Not.Null);

            var id = this.GetFieldTextValue(element, 0);
            var text = this.GetFieldTextValue(element, 1);
            var itemCategory = this.GetFieldTextValue(element, 2);

            Assert.That(id, Is.EqualTo(Convert.ToString(maxId + 1)));
            Assert.That(text, Is.EqualTo(name));
            Assert.That(itemCategory, Is.EqualTo(categoryText));
        }

        [Test]
        public void EditItemWithCategory()
        {
            var productItems = this.GetItemList();
            var lastItem = productItems.Last();
            var itemId = this.GetFieldTextValue(lastItem, 0);
            var lastCategoryName = this.GetFieldTextValue(lastItem, 2);
            var actions = new Actions(this.Driver);

            actions.DoubleClick(lastItem).Perform();

            var editWindow = this.Driver.FindElementById("ChangeProductWindow");
            var productName = editWindow.FindElement(By.Id("NameCW"));
            var saveButton = editWindow.FindElement(By.Id("SaveCW"));
            var newName = "_Asc sorting test product 1";
            var categorySelect = editWindow.FindElement(By.Id("CategoryCW"));

            categorySelect.Click();

            var categories = categorySelect.FindElements(By.ClassName("ListBoxItem"));
            IWebElement category = null;
            var categoryText = lastCategoryName;

            while (categoryText == lastCategoryName)
            {
                var randomId = new Random().Next(0, categories.Count - 1);
                category = categories.ElementAt(randomId);
                categoryText = category.GetAttribute("Name");
            }

            Assert.That(category, Is.Not.Null);

            category.Click();

            productName.SendKeys(newName);

            saveButton.Click();

            productItems = this.GetItemList();
            var firstItem = productItems.First();
            var id = this.GetFieldTextValue(firstItem, 0);
            var text = this.GetFieldTextValue(firstItem, 1);
            var itemCategory = this.GetFieldTextValue(firstItem, 2);

            Assert.That(text, Is.EqualTo(newName));
            Assert.That(itemId, Is.EqualTo(id));
            Assert.That(itemCategory, Is.EqualTo(categoryText));
        }
    }
}
