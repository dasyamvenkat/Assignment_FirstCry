using Assignment_FirstCry.BaseClasses;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_FirstCry.PageObjects
{
    public class InvalidPage : BaseClass
    {
        [FindsBy(How = How.XPath, Using = "//*[@class='er_heading lft ew']")]
        private IWebElement invalid;

        public InvalidPage()
        {
            PageFactory.InitElements(_driver, this);
        }


        // Gives displayed Text message in the page
        public string GetPageText()
        {
            return invalid.Text;
        }
    }
}
