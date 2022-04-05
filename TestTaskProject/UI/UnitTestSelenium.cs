using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            System.Threading.Thread.Sleep(1000);
            currencyPopUp = new CurrencyPopUp(driver, currency);
            currencyPopUp.RussianRybLink.Click();
            
            // Assert
            mainPage.IniteElements(driver);
            Assert.IsTrue(mainPage.CurrencyName.Text.Contains(currency));
        }


        [TestCase("Nederlands")]
        public void ChangeLanguageTest(string language)
        {
            // Arrange
            LanguagePopUp languagePopUp;

            // Act
            mainPage.GoToPage(driver);
            mainPage.LanguageButton.Click();
            System.Threading.Thread.Sleep(1000);
            languagePopUp = new LanguagePopUp(driver);
            languagePopUp.NederlandsLanguage.Click();

            // Assert
            mainPage.IniteElements(driver);
            Assert.IsTrue(mainPage.LanguageTextName.Text.Contains(language));
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
            // check the page for the presence of London
            Assert.IsTrue(driver.FindElement(By.XPath("//a[@class= 'fc63351294 a168c6f285 a25b1d9e47']")).Displayed);

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
