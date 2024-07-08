using Assignment_FirstCry.BaseClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_FirstCry.PageObjects
{
    [Parallelizable(ParallelScope.All)]
    public class HomePage : BaseClass
    {
        [FindsBy(How = How.Id, Using = "search_box")]
        private IWebElement searchBox;
        
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Support')]")]
        private IWebElement supportLink;

        [FindsBy(How = How.XPath, Using = "//div[@id='searchlist']/ul/li")]
        private IList<IWebElement> searchSuggestionsList;

        [FindsBy(How = How.XPath, Using = "//span[normalize-space()='Select location']")]
        private IWebElement selectLocation;

        [FindsBy(How = How.XPath, Using = "//sapn[@class='R14_link']")]
        private IWebElement enterPincodeHome;

        [FindsBy(How = How.XPath, Using = "//input[@id='nonlpincode']")]
        private IWebElement enterPinHome;

        [FindsBy(How = How.XPath, Using = "//div[@id='epincode']//div[@class='appl-but']")]
        private IWebElement applyPinHome;

        [FindsBy(How = How.XPath, Using = "//span[@id='geopincode']")]
        private IWebElement pinInHome;

        [FindsBy(How = How.XPath, Using = "//div[@class='menu-container']//li[2]")]
        private IWebElement boysFashion;

        [FindsBy(How = How.XPath, Using = "//*[@class=\"B14_42 allcat\"]")]
        private IWebElement allCat;

        [FindsBy(How = How.XPath, Using = "//div[@class='option-container allsubmenu1-1']//a[normalize-space()='Sets & Suits']")]
        private IWebElement boysSuites;

        [FindsBy(How = How.XPath, Using = "//*[@class=\"list_wrapper_ftop\"]//span//a[1]")]
        private IWebElement homePageLink;

        [FindsBy(How = How.XPath, Using = "//*[@class=\"list_wrapper_ftop\"]//span//a[2]")]
        private IWebElement clothes;

        public HomePage()
        {
            PageFactory.InitElements(_driver, this);
        }

        //Enter text in search field
        public ResultPage EnterSearchText(string text)
        {
            searchBox.Clear();
            searchBox.SendKeys(text);
            searchBox.SendKeys(Keys.Enter);
            return new ResultPage();
        }

        // Click on Support link
        public SupportPage SupportClik()
        {
            supportLink.Click();
            return new SupportPage();
        }

        //Gives list of search suggestions
        public List<string> SearchSuggestions(string text)
        {
            searchBox.Clear();
            searchBox.SendKeys(text);
            return CommonPage.ConvertToDynamicList(searchSuggestionsList, "string");
        }

        //Selects the location
        public void SelectLocation(string pin)
        {
            selectLocation.Click();
            enterPincodeHome.Click();
            enterPinHome.Clear();
            enterPinHome.SendKeys(pin);
            applyPinHome.Click();

        }

        public string PincodeInHomepage()
        {
            return pinInHome.Text;
        }

        public void MovetoBoysFashion()
        {
            boysFashion.Click();
        }

        public void MovetoBoysSuites()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(allCat).Build().Perform();
            boysSuites.Click();
        }

        public string IsHomePageLinkPresent()
        {
            return homePageLink.Text;
        }
        public string IsClothesLinkPresent()
        {
            return clothes.Text;
        }


    }
}
