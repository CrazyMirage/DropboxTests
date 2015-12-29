using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTest.Driver;
using SeleniumTest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest.Steps
{
    class BasicSteps
    {
        IWebDriver driver;
        private static readonly ILog log = LogManager.GetLogger(typeof(BasicSteps));

        public void InitBrowser()
        {
            driver = DriverInstance.Instance();
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(60));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(60));
            log.Info("Browser is started.");
        }

        public void CloseBrowser()
        {
            DriverInstance.Close();
            log.Info("Browser is closed.");
        }

        public void WaitTillPageLoad()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.00));

            wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void Login(string email, string password)
        {
            MainPage page = new MainPage(driver);
            page.OpenPage();
            page.Login(email, password);
            log.Info("Login steps are performed.");
        }
        

        public string ShareFolder(string folderName)
        {
            var homePage = new HomePage(driver);
            var url = homePage.ShareFolder(folderName);
            homePage.HideOverlay();
            log.Info("ShareFolder steps are performed.");
            return url;
        }

        public bool IsFolderShared(string shareLink, string folderName)
        {
            var sharedPage = new SharedPage(driver, shareLink);
            sharedPage.OpenPage();
            WaitTillPageLoad();
            return (sharedPage.FolderName().Trim().ToLower().Equals(folderName));
        }

        public void CreateFolder(string folderName)
        {
            var homePage = new HomePage(driver);
            homePage.CreateNewFolder(folderName);
            log.Info("CreateFolder steps are performed.");
        }

        public bool IsLoggedIn(string email)
        {
            var homePage = new HomePage(driver);
            return (homePage.UserEmail().Trim().ToLower().Equals(email));
        }

        public bool IsAnonymousUser()
        {
            var homePage = new HomePage(driver);
            homePage.OpenPage();
            return homePage.OnPage();
        }

        public bool FolderExist(string folderName)
        {
            HomePage homePage = new HomePage(driver);
            return homePage.Folders().Any(x => x.Equals(folderName));
            log.Info("FolderExist steps are performed.");
        }

        public void DeleteFolder(string folderName)
        {
            HomePage homePage = new HomePage(driver);
            homePage.DeleteFolder(folderName);
            log.Info("DeleteFolder steps are performed.");
        }

        public void SignOut()
        {
            HomePage homePage = new HomePage(driver);
            homePage.SignOut();
        }
    }
}