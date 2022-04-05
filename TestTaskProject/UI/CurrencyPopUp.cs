using OpenQA.Selenium;

namespace TestTaskProject
{
    public class CurrencyPopUp
    {
        public IWebElement RussianRybLink { get; set; }

        public CurrencyPopUp(IWebDriver driver)
        {
            RussianRybLink = driver.FindElement(By.XPath("//*[@id=\"_7frvs4qvn\"]/div/div/div/div/div/div[1]/div/div[2]/div/div/div[1]/ul/li[4]/a"));
        }
    }
}