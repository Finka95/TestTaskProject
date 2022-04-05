using OpenQA.Selenium;

namespace TestTaskProject
{
    public class LanguagePopUp
    {
        public IWebElement NederlandsLanguage { get; set; }

        public LanguagePopUp(IWebDriver driver)
        {
            NederlandsLanguage = driver.FindElement(By.XPath("//*[@id=\"language-selection\"]/div/div/div/div/div/div[2]/div/div[2]/div/div/div[1]/ul/li[4]/a"));
        }
    }
}