using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumAutomation
{
    public class CorePage
    {
        public static IWebDriver driver;

        public static IWebDriver SeleniumInIt()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}