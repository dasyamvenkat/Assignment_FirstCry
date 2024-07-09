using Assignment_FirstCry.Utitlities;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Logs = Assignment_FirstCry.Utitlities.Logs;
using System.IO;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;

namespace Assignment_FirstCry.BaseClasses
{
    [TestFixture]
    public class BaseClass
    {
        [ThreadStatic]
        public static IWebDriver _driver;
        public string browserName;
        public static string originalWindow;
        public Logs _log = new Logs();
        public static Dictionary<string, string> dataFromExcel =  ExcelDataReader.readXLS
            (Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\")) + "ExcelData.xlsx");

        [SetUp]
        public void InitScript()
        {

            browserName = ConfigurationHelper.Get<string>("Broswer");
            switch (browserName)
            {
                case "Firefox":

                    _driver = GetFirefoxDriver();
                    break;

                case "Chrome":

                    _driver = GetChromeDriver();
                    break;

              
            }
            _log.Info("Driver Opened");
          
            var timeouts = _driver.Manage().Timeouts();
            timeouts.PageLoad = TimeSpan.FromSeconds(ConfigurationHelper.Get<int>("PageLoadTimeout"));
            //Open First Cry Website
            NavigateToUrl(ConfigurationHelper.Get<string>("Url"));
            _log.Info(TestContext.CurrentContext.Test.Name + "TestCase Started");

           originalWindow = _driver.CurrentWindowHandle;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        private static FirefoxDriver GetFirefoxDriver()
        {
            FirefoxDriver driver = new FirefoxDriver();
            return driver;
        }

        private static ChromeDriver GetChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--headless");
            options.AddArgument("--start-maximized");

            // options.DebuggerAddress = "127.0.0.1";
            ChromeDriver driver = new ChromeDriver(options);
            return driver;
        }

       


        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
          //  _driver.Dispose();
        }

        [OneTimeTearDown]
        public void CleanupFinal()
        {
             _driver.Quit();
            //_driver.Dispose();
        }

        //[TearDown]
        //public void Cleanup()
        //{
        //    //Take the screenshot if Test Case failed
        //    if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
        //    {
        //        var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
        //        string fileName = String.Format(@"{0}\Screenshot" + TestContext.CurrentContext.Test.Name + DateTime.Now.ToShortDateString() + ".jpg", Environment.CurrentDirectory);
        //        screenshot.SaveAsFile(fileName);
        //    }
        //    _log.Info(TestContext.CurrentContext.Test.Name + "  -  " + TestContext.CurrentContext.Result.Outcome.Status);
        //    if (_driver == null)
        //    {
        //        _driver.Quit();
        //    }
        //    _driver.Dispose();
        //}


        //To handle 2 Windows
        public void WindowHandling()
        {
            string prodWindow = _driver.WindowHandles[1];
            _driver.SwitchTo().Window(prodWindow);
        }
        public void SwitchToFrame(string framdId)
        {
            _driver.SwitchTo().Frame(framdId);
        }

        // Close all tabs except first initiated Tab
        public void CloseTab()
        {
            try
            {
                foreach (String handle in _driver.WindowHandles)
                {
                    if (!handle.Equals(originalWindow))
                    {
                        _driver.SwitchTo().Window(handle);
                        _driver.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message);
                _log.Info(ex.StackTrace);
            }

            _driver.SwitchTo().Window(originalWindow);
        }

        //Move driver control to the specific window
        public void WindowHandlingDynamic(int index)
        {
            string prodWindow = _driver.WindowHandles[index];
            string Title = _driver.Title;
            _driver.SwitchTo().Window(prodWindow);

        }

        //Navigate to URL
        public void NavigateToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }
    }
}
