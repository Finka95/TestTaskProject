using OpenQA.Selenium;

namespace TestTaskProject
{
    public class CurrencyPopUp
    {
        public IWebElement RussianRybLink { get; set; }

        public CurrencyPopUp(IWebDriver driver)
        {
            RussianRybLink = driver.FindElement(By.PartialLinkText("RUB"));
        }          
    }
}