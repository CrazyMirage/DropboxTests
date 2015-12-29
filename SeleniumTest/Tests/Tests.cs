using System;
using SeleniumTest.Steps;
using NUnit.Framework;
using log4net;
using log4net.Config;

namespace SeleniumTest.Tests
{

    [TestFixture]
    public class Tests
    {
        static private BasicSteps steps;

        private static readonly ILog log = LogManager.GetLogger(typeof(Tests));
        const string Email = "kilneri@mail.ru";
        const string Password = "trin1TRON";

        [SetUp]
        public void Initialize()
        {
            XmlConfigurator.Configure();
            steps = new BasicSteps();
            steps.InitBrowser();
        }        


        [TestCase(Description = "Successful loged in.")]
        public void LoginToSite()
        {
            log.Info("LoginToSite test started");
            steps.Login(Email, Password);
            Assert.IsTrue(steps.IsLoggedIn(Email));

        }

        [TestCase(Description = "Successful sign out.")]
        public void SignOut()
        {
            log.Info("SignOut test started");
            steps.Login(Email, Password);
            steps.SignOut();
            Assert.IsFalse(steps.IsAnonymousUser());
        }

        [TestCase(Description = "Folder created successfully.")]
        public void CreateFolder()
        {
            log.Info("CreateFolder test started");
            var folder = Guid.NewGuid().ToString();
            steps.Login(Email, Password);
            steps.CreateFolder(folder);
            Assert.IsTrue(steps.FolderExist(folder));
        }

        [TestCase(Description = "Folder shared successfully.")]
        public void ShareFolder()
        {
            log.Info("ShareFolder test started");
            var folder = Guid.NewGuid().ToString();
            steps.Login(Email, Password);
            steps.CreateFolder(folder);
            var url = steps.ShareFolder(folder);
            steps.SignOut();
            Assert.IsNotNull(url, "Incorrect URL");
            Assert.IsFalse(steps.IsAnonymousUser(), "User are not anonymous");
            Assert.IsTrue(steps.IsFolderShared(url,folder), "Folder is not shared");
        }

        [TestCase(Description = "Folder deleted successfully.")]
        public void DeleteFolder()
        {
            log.Info("DeleteFolder test started");
            var folder = Guid.NewGuid().ToString();
            steps.Login(Email, Password);
            steps.CreateFolder(folder);
            steps.DeleteFolder(folder);
            Assert.IsFalse(steps.FolderExist(folder));
        }

        [TearDown]
        public void Cleanup()
        {
            steps.CloseBrowser();
        }
    }
}
