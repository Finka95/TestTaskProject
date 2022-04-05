using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestTaskProject
{
    public class PeoplePopUp
    {
        private IWebElement AddChildrenButton { get; set; }
        public SelectElement SelectElement { get; set; }

        public PeoplePopUp(IWebDriver driver)
        {
            AddChildrenButton = driver.FindElement(By.XPath("//*[@id=\"xp__guests__inputs-container\"]/div/div/div[2]/div/div[2]/button[2]"));
        }

        public void ClickOnAddChilldrenButton(IWebDriver driver)
        {
            AddChildrenButton.Click();
            SelectElement = new SelectElement(driver.FindElement(By.XPath("//*[@id=\"xp__guests__inputs-container\"]/div/div/div[3]/select")));
        }
    }
}