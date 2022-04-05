using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestTaskProject
{ 
    public class MainPage
    {
        public WebDriverWait webDriverWait;

        private const string MAIN_PAGE = @"https://www.booking.com/";

        public IWebElement CurrencyButton { get; set; }

        public IWebElement CurrencyName { get; set; }

        public IWebElement LanguageButton { get; set; }

        public IWebElement LanguageTextName { get; set; }

        public IWebElement PersonalAccountButton { get; set; }

        public IWebElement FlightButton { get; set; }

        public IWebElement CityFilerField { get; set; }

        public IWebElement DateFilterField { get; set; }

        public IWebElement PeopleFilerField { get; set; }

        public IWebElement SearchButton { get; set; }

        public MainPage(IWebDriver driver)
        {
            GoToPage(driver);
        }

        public void GoToPage(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(MAIN_PAGE);
            IniteElements(driver);
            webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void IniteElements(IWebDriver driver)
        {
            CurrencyButton = driver.FindElement(By.XPath("//*[@id=\"b2indexPage\"]/header/nav[1]/div[2]/div[1]/button"));
            CurrencyName = driver.FindElement(By.XPath("//*[@id=\"b2indexPage\"]/header/nav[1]/div[2]/div[1]/button/span/span[1]"));
            LanguageButton = driver.FindElement(By.XPath("//*[@id=\"b2indexPage\"]/header/nav[1]/div[2]/div[2]/button"));
            LanguageTextName = driver.FindElement(By.XPath("//*[@id=\"b2indexPage\"]/header/nav[1]/div[2]/div[2]/button/span/span"));
            FlightButton = driver.FindElement(By.XPath("//*[@id=\"b2indexPage\"]/header/nav[2]/ul/li[2]/a"));
            PersonalAccountButton = driver.FindElement(By.XPath("//*[@id=\"b2indexPage\"]/header/nav[1]/div[2]/div[6]/a"));

            CityFilerField = driver.FindElement(By.XPath("//*[@id=\"ss\"]"));
            DateFilterField = driver.FindElement(By.XPath("//*[@id=\"frm\"]/div[1]/div[2]/div[1]"));
            PeopleFilerField = driver.FindElement(By.XPath("//*[@id=\"frm\"]/div[1]/div[3]"));
            SearchButton = driver.FindElement(By.XPath("//*[@id=\"frm\"]/div[1]/div[4]/div[2]/button"));
        }
    }
}