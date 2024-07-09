using Assignment_FirstCry.BaseClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_FirstCry.PageObjects
{
    [Parallelizable(ParallelScope.All)]
    public class ResultPage : BaseClass
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

        WebDriverWait wait;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'li_txt1')]//a")]
        private IList<IWebElement> listOfProducts;

        [FindsBy(How = How.XPath, Using = "//span[@class = 'r1 B14_42']//a")]
        private IList<IWebElement> priceListOfProducts;

        //   [FindsBy(How = How.XPath, Using = "//div[@id='fltbrnd']//span[contains(text(), '" + dataFromExcel["brandFilter"].ToString() + "')]")]
        [FindsBy(How=How.XPath, Using = "//div[@id='fltbrnd']//li")]
        private IList<IWebElement> brandFilter;

        [FindsBy(How = How.ClassName, Using = "sort-select")]
        private IWebElement sortByElement;

        [FindsBy(How = How.XPath, Using = "//ul[contains(@class,'sort-menu')]/li/a[contains(text(),'Price')]")]
        private IWebElement sortByPrice;

        [FindsBy(How = How.XPath, Using = "//div[@class ='list_img wifi']")]
        private IWebElement firstPrdoductLink;

        [FindsBy(How = How.Id, Using = "cart_TotalCount")]
        private IWebElement cart;

        [FindsBy(How = How.XPath, Using = "//p[contains(text(),'Please make sure the spelling is correct')]")]
        private IWebElement invalidSearch;


        [FindsBy(How = How.XPath, Using = "//*[@class='slider-container']//img")]
        private IList<IWebElement> images;



        public ResultPage()
        {
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            PageFactory.InitElements(_driver, this);
        }

        //Opens first product in results
        public ProductPage OpenFirstProductLink()
        {
            firstPrdoductLink.Click();
            return new ProductPage();
        }


        public string InvalidResult()
        {
            return invalidSearch.Text;
        }

        //Verifies if results contains searched products 
        public bool VerifySearchResults()
        {
            List<string> productNames = ConvertoList(listOfProducts);
             return VerifyResultsForSpecificValue("Stroller");
        }

        // Applies Brand filter
        public void ApplyBrandFilter(string brandName)
        {
            js.ExecuteScript("window.scrollBy(0,500)", "");
            Thread.Sleep(1000);
            brandFilter[0].Click();
        }

        //Select price as Sort option
        public void SelectSortPriceOption(string sortOption)
        {
            sortByElement.Click();
            sortByPrice.Click();
        }

        // Validates Results according to sorting order
        public bool VerifyResultsSortingPriceOrder(string sortOrder)
        {
            Thread.Sleep(1000);
             List<int> priceList= CommonPage.ConvertToDynamicList(priceListOfProducts, "int");
            if (sortOrder=="asc")
            {
                List<int> expectedList = new List<int>();
                expectedList = priceList.OrderBy(x => x).ToList();
                //_log.Info("expected  -" + expectedList[0].ToString() + " : real " + priceList[0].ToString());
                return expectedList.SequenceEqual(priceList);
            }
            else
            {
                List<int> expectedList = new List<int>();
                expectedList = priceList.OrderByDescending(x => x).ToList();
                return expectedList.SequenceEqual(priceList);
            }
        }

        // Verifies if results contains specific text like brand or type of product
        public bool VerifyResultsForSpecificValue(string text)
        {
            List<string> listOfProdNames = ConvertoList(listOfProducts );
            for (int i = 0; i < listOfProdNames.Count; i++)
            {
                if (listOfProdNames[i].Contains(text))
                { return true; }
            }
            return false;

        }

        //Opens given number of product from results page
        public List<string> OpenNumberofProducts(int num)
        {
            List<string> listOfProds = new List<string>();
            for (int i = 0; i < num; i++)
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(listOfProducts[i])).Click();
                //listOfProducts[i].Click();
                listOfProds.Add(listOfProducts[i].Text.Split(" -")[0]);
            }
            List<string> productNames = CommonPage.ConvertToDynamicList(listOfProducts, "string");

            return productNames.GetRange(0,num);
        }

        public List<string> ConvertoList(IList<IWebElement> results)
        {
            List<string> listOfProdNames = new List<string>();

            for (int i = 0; i < results.Count; i++)
            {
                string text = results[i].GetAttribute("title").ToString();
                listOfProdNames.Add(text);
            }
            return listOfProdNames;

        }

        // Gets the price of the listed products
        public List<double> GetPriceOfProducts(int num)
        {
            List<double> listOfProds = new List<double>();
            listOfProds = CommonPage.ConvertToDynamicList(priceListOfProducts, "double");
            return listOfProds.GetRange(0, num);
        }

        //opens cart page
        public CartPage OpenCart()
        {
            cart.Click();
           return new CartPage();
        }

        //
        public List<string> GetAllAltTags()
        {
            List<string> alttext = new List<string>();
            foreach (var alt in images)
            {
                alttext.Add(alt.GetAttribute("alt"));
            }

            return alttext;
        }
    }
}
