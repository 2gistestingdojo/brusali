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
        public IWebElement SearchTextboxElement;
        public IWebElement SearchButtonElement;
        public IWebElement AddButtonElement;

        [SetUp]
        public void SetUp()
        {
            base.SetUp();

            this.SearchTextboxElement = this.MainWindowElement.FindElement(By.Id("QueryMW"));
            this.SearchButtonElement = this.MainWindowElement.FindElement(By.Id("SearchMW"));
            this.AddButtonElement = this.MainWindowElement.FindElement(By.Id("AddNewProductMW"));
        }

        [Test]
        public void AddItem()
        {
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

            this.AddButtonElement.Click();

            var addWindow = this.Driver.FindElementById("AddNewProductWindow");
            var productName = addWindow.FindElement(By.Id("NameAW"));
            var addButton = addWindow.FindElement(By.Id("AddAW"));
            var name = "Test product 1";

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

            Assert.That(id, Is.EqualTo(Convert.ToString(maxId + 1)));
            Assert.That(text, Is.EqualTo(name));
        }
    }
}
