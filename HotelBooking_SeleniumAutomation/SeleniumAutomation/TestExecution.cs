using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAutomation;
using System;

namespace Automation
{
    [TestClass]
    public class TestExecution
    {
        LoginPage loginPage = new LoginPage();
        WebDriverWait wait;

        [TestInitialize]
        public void Setup()
        {
            CorePage.SeleniumInIt();
            wait = new WebDriverWait(CorePage.driver, TimeSpan.FromSeconds(30));
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (CorePage.driver != null)
                CorePage.driver.Quit();
        }

        // ---------------- HELPER METHODS ----------------
        public void ValidLogin()
        {
            loginPage.Login("https://adactinhotelapp.com/", "admin1777", "98765abcd$*98");
            // Wait for welcome message
            wait.Until(driver => driver.FindElement(By.XPath("//td[contains(text(),'Welcome')]")));
        }

        public void SearchHotel()
        {
            wait.Until(driver => driver.FindElement(By.Id("location"))).SendKeys("Sydney");
            CorePage.driver.FindElement(By.Id("hotels")).SendKeys("Hotel Creek");
            CorePage.driver.FindElement(By.Id("room_type")).SendKeys("Standard");
            CorePage.driver.FindElement(By.Id("room_nos")).SendKeys("1");
            CorePage.driver.FindElement(By.Id("Submit")).Click();

            wait.Until(driver => driver.Title.Contains("Select Hotel"));
        }

        public void SelectHotel()
        {
            wait.Until(driver => driver.FindElement(By.Id("radiobutton_0"))).Click();
            CorePage.driver.FindElement(By.Id("continue")).Click();
            wait.Until(driver => driver.Title.Contains("Book A Hotel"));
        }

        public void FillBookingForm(string fname, string lname, string address, string ccnum = "", string cctype = "", string expmonth = "", string expyear = "", string cvv = "")
        {
            CorePage.driver.FindElement(By.Id("first_name")).SendKeys(fname);
            CorePage.driver.FindElement(By.Id("last_name")).SendKeys(lname);
            CorePage.driver.FindElement(By.Id("address")).SendKeys(address);

            if (!string.IsNullOrEmpty(ccnum))
            {
                CorePage.driver.FindElement(By.Id("cc_num")).SendKeys(ccnum);
                CorePage.driver.FindElement(By.Id("cc_type")).SendKeys(cctype);
                CorePage.driver.FindElement(By.Id("cc_exp_month")).SendKeys(expmonth);
                CorePage.driver.FindElement(By.Id("cc_exp_year")).SendKeys(expyear);
                CorePage.driver.FindElement(By.Id("cc_cvv")).SendKeys(cvv);
            }
        }

        // ================= POSITIVE TESTS =================

        [TestMethod]
        public void TC01_Valid_Login()
        {
            ValidLogin();
            string welcomeText = CorePage.driver.FindElement(By.XPath("//td[contains(text(),'Welcome')]")).Text;
            Assert.IsTrue(welcomeText.Contains("Welcome"));
        }

        [TestMethod]
        public void TC02_Valid_Hotel_Search()
        {
            ValidLogin();
            SearchHotel();
            Assert.IsTrue(CorePage.driver.Title.Contains("Select Hotel"));
        }

        [TestMethod]
        public void TC03_Select_Hotel()
        {
            ValidLogin();
            SearchHotel();
            SelectHotel();
            Assert.IsTrue(CorePage.driver.Title.Contains("Book A Hotel"));
        }

        [TestMethod]
        public void TC04_Fill_Booking_Form_Partial()
        {
            ValidLogin();
            SearchHotel();
            SelectHotel();
            FillBookingForm("Areesha", "Mallick", "Pakistan");
            // Just check that "Book Now" button is displayed
            Assert.IsTrue(CorePage.driver.FindElement(By.Id("book_now")).Displayed);
        }

        [TestMethod]
        public void TC05_Logout_After_Login()
        {
            ValidLogin();
            CorePage.driver.FindElement(By.LinkText("Logout")).Click();
            Assert.IsTrue(CorePage.driver.Title.Contains("Logout"));
        }

        // ================= NEGATIVE TESTS =================

        [TestMethod]
        public void TC06_Invalid_Login()
        {
            loginPage.Login("https://adactinhotelapp.com/", "wronguser", "wrongpass");
            string error = wait.Until(driver => driver.FindElement(By.ClassName("auth_error"))).Text;
            Assert.IsTrue(error.Contains("Invalid Login"));
        }
        [TestMethod]
        public void TC07_Search_Without_Location()
        {
            ValidLogin();
            CorePage.driver.FindElement(By.Id("Submit")).Click();
            // Assert page title still contains "Search Hotel"
            Assert.IsTrue(CorePage.driver.Title.Contains("Search Hotel"));
        }
        [TestMethod]
        public void TC08_Booking_Without_CVV()
        {
            ValidLogin();
            SearchHotel();
            SelectHotel();
            FillBookingForm("Areesha", "Mallick", "Pakistan", "4111111111111111", "VISA", "12", "2028", ""); // CVV empty
            CorePage.driver.FindElement(By.Id("book_now")).Click();
            System.Threading.Thread.Sleep(2000);
            var orderElements = CorePage.driver.FindElements(By.Id("order_no"));
            Assert.IsTrue(orderElements.Count == 0, "Booking should fail without CVV");
        }

        [TestMethod]
        public void TC09_Booking_Without_Name()
        {
            ValidLogin();
            SearchHotel();
            SelectHotel();
            CorePage.driver.FindElement(By.Id("book_now")).Click();
            // Wait 3 seconds for page reaction
            System.Threading.Thread.Sleep(3000);
            // Order number should NOT exist
            var orderElements = CorePage.driver.FindElements(By.Id("order_no"));
            Assert.IsTrue(orderElements.Count == 0);
        }

        [TestMethod]
        public void TC10_Logout_Without_Login()
        {
            CorePage.driver.Navigate().GoToUrl("https://adactinhotelapp.com/Logout.php");
            Assert.IsTrue(CorePage.driver.Title.Contains("Logout"));
        }
    }
}
