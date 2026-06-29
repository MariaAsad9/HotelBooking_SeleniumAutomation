using OpenQA.Selenium;
using SeleniumAutomation;

namespace Automation
{
    public class LoginPage : CorePage
    {
        public static By usernameTXT = By.Id("username");
        public static By passwordTXT = By.Id("password");
        public static By loginBtn = By.Id("login");

        public void Login(string url, string username, string password)
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(usernameTXT).Clear();
            driver.FindElement(usernameTXT).SendKeys(username);
            driver.FindElement(passwordTXT).Clear();
            driver.FindElement(passwordTXT).SendKeys(password);
            driver.FindElement(loginBtn).Click();
        }
    }
}