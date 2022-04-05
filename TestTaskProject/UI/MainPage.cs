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
        private const string MAIN_PAGE = @"https://www.booking.com/";

        public IWebElement CurrencyButton { get; set; }

        public IWebElement LanguageButton { get; set; }

        public IWebElement PersonalAccountButton { get; set; }

        public IWebElement FlightButton { get; set; }

        public IWebElement CityFilerField { get; set; }

        public IWebElement DateFilterField { get; set; }

        public IWebElement PeopleFilerField { get; set; }

        public IWebElement SearchButton { get; set; }

        public MainPage(IWebDriver driver)
        {
            GoToPage(driver);
            IniteElements(driver);
        }

        public void GoToPage(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(MAIN_PAGE);
        }

        public void IniteElements(IWebDriver driver)
        {
            CurrencyButton = driver.FindElement(By.XPath("//*[@id=\"b2indexPage\"]/header/nav[1]/div[2]/div[1]/button"));
            LanguageButton = driver.FindElement(By.XPath("//*[@id=\"b2indexPage\"]/header/nav[1]/div[2]/div[2]/button"));
            FlightButton = driver.FindElement(By.XPath("//*[@id=\"b2indexPage\"]/header/nav[2]/ul/li[2]/a"));
            PersonalAccountButton = driver.FindElement(By.XPath("//*[@id=\"b2indexPage\"]/header/nav[1]/div[2]/div[6]/a"));

            CityFilerField = driver.FindElement(By.XPath("//*[@id=\"ss\"]"));
            DateFilterField = driver.FindElement(By.XPath("//*[@id=\"frm\"]/div[1]/div[2]/div[1]"));
            PeopleFilerField = driver.FindElement(By.XPath("//*[@id=\"frm\"]/div[1]/div[3]"));
            SearchButton = driver.FindElement(By.XPath("//*[@id=\"frm\"]/div[1]/div[4]/div[2]/button"));
        }
    }
}