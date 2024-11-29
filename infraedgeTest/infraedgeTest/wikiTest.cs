using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace infraedgeTest
{
    public class wikiTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        #region selectors

        string TestDrivenTitleId = "Test-driven_development";
        string TitlesClass = "mw-heading3";
        string FollowingSiblingXpath = "following-sibling::*";

        #endregion

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Test_automation");
        }

        [Test]
        public void WikiUi()
        {
            IWebElement TestDrivenTitleInner = driver.FindElement(By.Id(TestDrivenTitleId));
            Actions actions = new Actions(driver);
            actions.MoveToElement(TestDrivenTitleInner).Perform();

            wait.Until(d => TestDrivenTitleInner.Displayed);

            IWebElement TestDrivenTitle = driver.FindElements(By.ClassName(TitlesClass))
                .First(e => e.FindElements(By.Id(TestDrivenTitleId)).Any());

            string TestDrivenSectionText = TestDrivenTitleInner.Text + " " +
                TestDrivenTitle.FindElement(By.XPath(FollowingSiblingXpath)).Text;

            Dictionary<string, int> UniqueWords = utilityFunctions.GetUniqueWords(TestDrivenSectionText);

            foreach (KeyValuePair<string, int> UniqueWord in UniqueWords)
            {
                Console.WriteLine($"{UniqueWord.Key}: {UniqueWord.Value}");
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
