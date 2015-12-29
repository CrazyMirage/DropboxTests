using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest.Driver
{
    class DriverInstance
    {
        private static IWebDriver driver;

        private DriverInstance() { }

        public static IWebDriver Instance()
        {
            if (driver == null)
            {
                //ChromeOptions options = new ChromeOptions();
                //options.AddArgument("--start-maximized");
                driver = new ChromeDriver();
                //driver.Manage().Window.Maximize();
                driver.Manage().Window.Size = new System.Drawing.Size() { Height = 1084, Width = 1920 };
            }
            return driver;
        }

        public static void Close()
        {
            driver.Quit();
            driver = null;
        }
    }
}
