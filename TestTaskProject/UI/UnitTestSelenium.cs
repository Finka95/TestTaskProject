using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestTaskProject
{
    //[TestFixture]
    public class UnitTestSelenium
    {
        IWebDriver driver;
        string link;
        string targetArrivalDateTime;
        string targetDepartureDateTime;

        //[OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            link = @"https://www.booking.com/";
            targetArrivalDateTime = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
            targetDepartureDateTime = DateTime.Now.AddDays(9).ToString("yyyy-MM-dd");
        }

        public void GoToMainPage()
        {
            driver.Navigate().GoToUrl(link);
        }

        //[Test]
        public void GoToMainPageTest()
        {
            // Act
            GoToMainPage();

            // Assert
            Assert.IsTrue(driver.FindElement(By.XPath("//button[@class= 'bui-button bui-button--light bui-button--large']")).Displayed);

        }

        //[TestCase("RUB")]
        public void ChangeCurrencyTest(string currency)
        {
            // Act
            GoToMainPage();
            driver.FindElement(By.XPath("//button[@class= 'bui-button bui-button--light bui-button--large']")).Click();
            driver.FindElement(By.PartialLinkText(currency)).Click();

            // Assert
            Assert.AreEqual(driver.FindElement(By.XPath("//span[@aria-hidden= 'true']")).Text, currency);
        }


        //[TestCase("Nederlands")]
        public void ChangeLanguageTest(string language)
        {
            // Arrange
            ReadOnlyCollection<IWebElement> buttons;

            // Act
            GoToMainPage();
            driver.FindElement(By.XPath("//button[@data-modal-aria-label= 'Выберите язык']")).Click();
            driver.FindElement(By.PartialLinkText(language)).Click();
            buttons = driver.FindElements(By.XPath("//span[@class= 'bui-u-sr-only']"));

            // Assert
            Assert.IsTrue(buttons[1].Text.Contains(language));
        }

        //[TestCase("booking.com/flights")]
        public void GoToFlights(string url)
        {
            // Act
            GoToMainPage();
            driver.FindElement(By.XPath("//a[@data-decider-header= 'flights']")).Click();

            // Assert
            Assert.IsTrue(driver.Url.Contains(url));
        }

        //[Test]
        public void CheckPersonalAccount()
        {
            // Act
            GoToMainPage();
            driver.FindElement(By.XPath("//a[@data-google-track= 'Click/Action: index/header_logged_out_link_box']")).Click();

            // Assert
            Assert.IsTrue(driver.Url.Contains("account.booking.com"));
        }

        //[TestCase("London")]
        public void UseFilterTest(string cityName)
        {
            // Arrange
            IWebElement city;
            SelectElement selectElement;
            ReadOnlyCollection<IWebElement> parentButton;

            // Act
            GoToMainPage();
            // select the citу
            city = driver.FindElement(By.XPath("//input[@type= 'search']"));
            city.Click();
            city.SendKeys(cityName);
            // select date
            driver.FindElement(By.XPath("//div[@class= 'xp__dates-inner']")).Click();
            driver.FindElement(By.XPath($"//td[@data-date= '{targetArrivalDateTime}']")).Click();
            driver.FindElement(By.XPath($"//td[@data-date= '{targetDepartureDateTime}']")).Click();
            // choose the number of people
            driver.FindElement(By.XPath("//div[@data-visible= 'accommodation,flights']")).Click();
            parentButton = driver.FindElements(By.XPath("//button[@class= 'bui-button bui-button--secondary bui-stepper__add-button ']"));
            parentButton[1].Click();
            selectElement = new SelectElement(driver.FindElement(By.XPath("//select[@name= 'age']")));
            selectElement.SelectByValue("3");
            driver.FindElement(By.XPath("//button[@data-sb-id= 'main']")).Click();

            // Assert
            Assert.IsTrue(driver.FindElement(By.XPath("//a[@class= 'fc63351294 a168c6f285 a25b1d9e47']")).Displayed);
        }

        //[OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
