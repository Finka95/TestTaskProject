using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestTaskProject
{
    //[TestFixture]
    public class UnitTestSeleniumCopy
    {
        IWebDriver driver;
        MainPage mainPage;
        string targetArrivalDateTime;
        string targetDepartureDateTime;

        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            mainPage = new MainPage(driver);
        }

        [Test]
        public void GoToMainPageTest()
        {
            // Act
            mainPage.GoToPage(driver);

            // Assert
            Assert.IsTrue(mainPage.CurrencyButton.Displayed);
        }

        [TestCase("RUB")]
        public void ChangeCurrencyTest(string currency)
        {
            // Arrange
            CurrencyPopUp currencyPopUp;

            // Act
            mainPage.GoToPage(driver);
            mainPage.CurrencyButton.Click();
            currencyPopUp = new CurrencyPopUp(driver);
            currencyPopUp.RussianRybLink.Click();

            // Assert
            Assert.AreEqual(mainPage.CurrencyButton.Text, currency);
        }


        [TestCase("Nederlands")]
        public void ChangeLanguageTest(string language)
        {
            // Arrange
            LanguagePopUp languagePopUp;

            // Act
            mainPage.GoToPage(driver);
            mainPage.LanguageButton.Click();
            languagePopUp = new LanguagePopUp(driver);
            languagePopUp.NederlandsLanguage.Click();

            // Assert
            Assert.IsTrue(mainPage.LanguageButton.Text.Contains(language));
        }

        [TestCase("booking.com/flights")]
        public void GoToFlights(string url)
        {
            // Act
            mainPage.GoToPage(driver);
            mainPage.FlightButton.Click();

            // Assert
            Assert.IsTrue(driver.Url.Contains(url));
        }

        [TestCase("account.booking.com")]
        public void CheckPersonalAccount(string url)
        {
            // Act
            mainPage.GoToPage(driver);
            mainPage.PersonalAccountButton.Click();

            // Assert
            Assert.IsTrue(driver.Url.Contains(url));
        }

        [TestCase("London", "3")]
        public void UseFilterTest(string cityName, string yearsOldChild)
        {
            // Arrange
            DatePopUp datePopUp;
            PeoplePopUp peoplePopUp;
            targetArrivalDateTime = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
            targetDepartureDateTime = DateTime.Now.AddDays(9).ToString("yyyy-MM-dd");

            // Act
                mainPage.GoToPage(driver);

                // select the citу
                mainPage.CityFilerField.Click();
                mainPage.CityFilerField.SendKeys(cityName);

                // select date
                mainPage.DateFilterField.Click();
                datePopUp = new DatePopUp(driver, targetArrivalDateTime, targetDepartureDateTime);
                datePopUp.TargetArrivalDateTime.Click();
                datePopUp.TargetDepartureDateTime.Click();

                // choose the number of people
                mainPage.PeopleFilerField.Click();
                peoplePopUp = new PeoplePopUp(driver);
                peoplePopUp.ClickOnAddChilldrenButton(driver);
                peoplePopUp.SelectElement.SelectByValue(yearsOldChild);
                mainPage.SearchButton.Click();

            // Assert
            Assert.IsTrue(driver.FindElement(By.XPath("//a[@class= 'fc63351294 a168c6f285 a25b1d9e47']")).Displayed);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
