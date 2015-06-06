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

        [Test]
        public void AddItem()
        {
            var productsList = this.MainWindowElement.FindElement(By.Id("ProductsMW"));
            var productItems = productsList.FindElements(By.ClassName("ListViewItem"));
            var maxId = 0;

            foreach (var item in productItems)
            {
                var tmp = Convert.ToInt32(item.FindElements(By.ClassName("TextBlock")).First().GetAttribute("Name"));

                if (tmp > maxId)
                {
                    maxId = tmp;
                }
            }

            this.AddButtonElement.Click();

            var addWindow = this.Driver.FindElementById("AddNewProductWindow");
            var productName = addWindow.FindElement(By.Id("NameAW"));
            var addButton = addWindow.FindElement(By.Id("AddAW"));
            var name = "Test product 1";

            productName.SendKeys(name);

            addButton.Click();

            productsList = this.MainWindowElement.FindElement(By.Id("ProductsMW"));
            productItems = productsList.FindElements(By.ClassName("ListViewItem"));
            IWebElement element = null;
         
            foreach (var item in productItems)
            {
                var tmp = item.FindElements(By.ClassName("TextBlock")).ElementAt(1).GetAttribute("Name");

                if (tmp == name)
                {
                    element = item;
                }
            }

            Assert.That(element, Is.Not.Null);

            var texts = element.FindElements(By.ClassName("TextBlock"));
            var id = texts.First();
            var text = texts.ElementAt(1);

            Assert.That(id.GetAttribute("Name"), Is.EqualTo(Convert.ToString(maxId + 1)));
            Assert.That(text.GetAttribute("Name"), Is.EqualTo("Test product 1"));
        }
    }
}
