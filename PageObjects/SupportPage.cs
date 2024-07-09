using Assignment_FirstCry.BaseClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment_FirstCry.PageObjects
{
    [Parallelizable(ParallelScope.All)]
    public class SupportPage : BaseClass
    {
       
        WebDriverWait wait;

        [FindsBy(How = How.XPath, Using = "//p[@class='M14_21']")]
        private IWebElement contactDetailsLink;

        [FindsBy(How = How.Id, Using = "customercare")]
        private IWebElement customerCare;

        [FindsBy(How = How.XPath, Using = "//span[@class='R16_link']")]
        private IWebElement email;

        [FindsBy(How = How.XPath, Using = "(//*[@class=\"M15_white\"])[1]")]
        private IWebElement supportinfo;
        

        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
        public SupportPage()
        {
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            PageFactory.InitElements(_driver, this);
        }

        //Get Contact Email Address
        public string getEmailForContact()
        {
            js.ExecuteScript("window.scrollBy(0,2000)");
         
            js.ExecuteScript("arguments[0].click();", contactDetailsLink);

            js.ExecuteScript("arguments[0].click();", customerCare);
        
            return email.GetAttribute("innerText");
             
        }

        public string SupportInfo()
        {
            return supportinfo.Text;
        }
    }
}
