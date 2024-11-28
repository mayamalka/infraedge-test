using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace infraedgeTest
{
    public class wikiTest
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void OpenGoogle()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
            Assert.That(driver.Title.Contains("Google"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }

}  
