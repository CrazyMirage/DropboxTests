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
    class SharedPage
    {
        private readonly string URL;
        private IWebDriver driver;
        private static readonly ILog log = LogManager.GetLogger(typeof(MainPage));

        [FindsBy(How = How.CssSelector, Using = ".display-name")]
        private IWebElement folderName;

        public SharedPage(IWebDriver driver, string url)
        {
            this.driver = driver;
            URL = url;
            PageFactory.InitElements(this.driver, this);
        }

        internal void OpenPage()
        {
            log.Info("Go to share page: (" + URL + ")");
            driver.Navigate().GoToUrl(URL);
        }

        internal string FolderName()
        {
            log.Info(folderName.Text);
            log.Info(folderName.Displayed);
            log.Info(folderName.Enabled);
            log.Info(folderName.Location);
            log.Info(folderName.Size);
            return folderName.Text;
        }
    }
}
