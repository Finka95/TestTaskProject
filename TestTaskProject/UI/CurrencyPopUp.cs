using OpenQA.Selenium;

namespace TestTaskProject
{
    public class CurrencyPopUp
    {
        public IWebElement RussianRybLink { get; set; }

        public CurrencyPopUp(IWebDriver driver, string currency)
        {
            RussianRybLink = driver.FindElement(By.PartialLinkText("RUB"));
        }          
    }
}