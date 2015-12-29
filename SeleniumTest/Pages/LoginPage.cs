using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeleniumTest.Pages
{
    class LoginPage
    {
        private const string BASE_URL = "https://www.dropbox.com/login";
        private IWebDriver driver;
        private static readonly ILog log = LogManager.GetLogger(typeof(MainPage));

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this.driver, this);
        }

        internal void OpenPage()
        {
            log.Info("Go to login page: ("+BASE_URL+")");
            driver.Navigate().GoToUrl(BASE_URL);
        }

        internal bool OnPage()
        {
            return Regex.IsMatch(driver.Url, "^"+BASE_URL+".*");
        }
    }
}
