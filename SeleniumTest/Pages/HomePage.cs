using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeleniumTest.Pages
{
    class HomePage
    {
        private const string BASE_URL = "https://www.dropbox.com/home";
        private IWebDriver driver;
        private static readonly ILog log = LogManager.GetLogger(typeof(MainPage));

        [FindsBy(How = How.CssSelector, Using = ".email.force-no-break")]
        private IWebElement userLogin;

        [FindsBy(How = How.CssSelector, Using = ".header-nav-link.button-as-link.bubble-dropdown-target")]
        private IWebElement userInfoButton;

        [FindsBy(How = How.CssSelector, Using = "li.option-to-share-link")]
        private IWebElement shareLinkButton;

        [FindsBy(How = How.CssSelector, Using = ".copy-link-input-container input")]
        private IWebElement sharedLinkInput;

        [FindsBy(How = How.Id, Using = "new_folder_button")]
        private IWebElement newFolderButton;

        [FindsBy(How = How.CssSelector, Using = "#create-and-share-new-folder-name-input input")]
        private IWebElement folderNameInput;

        [FindsBy(How = How.CssSelector, Using = "[aria-label='Close']")]
        private IList<IWebElement> overlaysCloseButtons;

        [FindsBy(How = How.CssSelector, Using = "[dropzone = 'copy move']")]
        private IList<IWebElement> foldersFields;
        
        [FindsBy(How = How.CssSelector, Using = ".inline-share-button")]
        private IList<IWebElement> shareButtons;

        [FindsBy(How = How.CssSelector, Using = ".dbmodal-button.confirm-button.button-primary")]
        private IList<IWebElement> confirmButtons;

        [FindsBy(How = How.CssSelector, Using = ".account-dropdown a")]
        private IList<IWebElement> userMenuLinks;

        [FindsBy(How = How.Id, Using = "upload_button")]
        private IWebElement uploadButton;

        [FindsBy(How = How.Id, Using = "delete_action_button")]
        private IWebElement deleteButton;

        [FindsBy(How = How.CssSelector, Using = ".button-primary.dbmodal-button")]
        private IList<IWebElement> deleteButtons;

        
        

        private string linkToFileSelector = ".filename-link";

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this.driver, this);
        }

        internal void OpenPage()
        {
            log.Info("Go to home page: (" + BASE_URL + ")");
            driver.Navigate().GoToUrl(BASE_URL);
        }
        
        internal bool OnPage()
        {
            return Regex.IsMatch(driver.Url, "^" + BASE_URL + ".*");
        }

        internal string UserEmail()
        {
            userInfoButton.Click();
            return userLogin.Text;
        }

        internal void DeleteFolder(string folderName)
        {
            var foldersCount = foldersFields.Count;

            var nededFolder = FolderField(folderName);
            nededFolder.Click();
            deleteButton.Click();
            deleteButtons.Where(x => x.Displayed).First().Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(_ => foldersFields.Count < foldersCount);
            log.Info("Folder is deleted");
        }

        internal IWebElement FolderField(string folderName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.Until(_ => foldersFields.FirstOrDefault());
            IWebElement nededFolder = null;
            foreach (var folder in foldersFields)
            {
                if (folder.FindElement(By.CssSelector(linkToFileSelector)).Text.Trim().ToLower().Equals(folderName))
                {
                    nededFolder = folder;
                    break;
                }
            }
            return nededFolder;

        }

        internal string ShareFolder(string folderName)
        {
            var nededFolder = FolderField(folderName);
            nededFolder.Click();
            shareButtons.Where(x => x.Displayed).First().Click();
            shareLinkButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(_ => sharedLinkInput.Displayed);
            log.Info("Folder is shared");
            return sharedLinkInput.GetAttribute("value");
        }

        internal void HideOverlay()
        {
            overlaysCloseButtons.Where(x => x.Displayed).First().Click();
        }

        internal void CreateNewFolder(string folderName)
        {
            newFolderButton.Click();

            var foldersCount = foldersFields.Count;

            folderNameInput.SendKeys(folderName);
            confirmButtons.Where(x => x.Displayed).First().Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(_ => foldersFields.Count > foldersCount);
            log.Info("Folder is created");
        }

        internal void SignOut()
        {
            userInfoButton.Click();
            userMenuLinks.Where(x => x.Text.Trim().ToLower().Equals("sign out")).First().Click();
            log.Info("Sign out is performed");
        }
        
        internal IEnumerable<string> Folders()
        {
            return foldersFields.Select(x => x.FindElement(By.CssSelector(linkToFileSelector)).Text.Trim().ToLower());
        }
    }
}
