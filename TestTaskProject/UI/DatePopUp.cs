using OpenQA.Selenium;

namespace TestTaskProject
{
    public class DatePopUp
    {
        public IWebElement TargetArrivalDateTime { get; set; }
        public IWebElement TargetDepartureDateTime { get; set; }

        public DatePopUp(IWebDriver driver, string targetArrivalDateTime, string targetDepartureDateTime)
        {
            TargetArrivalDateTime = driver.FindElement(By.XPath($"//td[@data-date= '{targetArrivalDateTime}']"));
            TargetDepartureDateTime = driver.FindElement(By.XPath($"//td[@data-date= '{targetDepartureDateTime}']"));
        }
    }
}