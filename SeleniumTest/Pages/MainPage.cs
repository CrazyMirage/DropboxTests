using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest.Pages
{
    class MainPage
    {
        private const string BASE_URL = "https://www.dropbox.com";
        private IWebDriver driver;
        private static readonly ILog log = LogManager.GetLogger(typeof(MainPage));

        [FindsBy(How = How.CssSelector, Using = "input[name=login_email]")]
        private IWebElement inputLoginEmail;

        [FindsBy(How = How.CssSelector, Using = "input[name=login_password]")]
        private IWebElement inputLoginPassword;
        
        [FindsBy(How = How.CssSelector, Using = "#index-sign-in-modal #regular-login-forms [type=submit].login-button.button-primary")]
        private IWebElement buttonLoginSubmit;
        
        [FindsBy(How = How.Id, Using = "sign-in")]
        private IWebElement buttonSignIn;

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this.driver, this);
        }

        internal void Login(string username, string password)
        {
            buttonSignIn.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000));
            wait.Until(_ => inputLoginEmail);

            inputLoginEmail.SendKeys(username);
            inputLoginPassword.SendKeys(password);

            buttonLoginSubmit.Click();
            log.Info("Log in is permormed.");
        }

        internal void OpenPage()
        {
            log.Info("Go to main page: (" + BASE_URL + ")");
            driver.Navigate().GoToUrl(BASE_URL);
        }

    }
}
